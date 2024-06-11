using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Contains and displays text message boxes for a NPC chat
public class ChatRoom : HideableUI
{
    [SerializeField] Transform m_messageParent = default;
    [SerializeField] MessageBox m_npcBoxPrefab = default;
    [SerializeField] MessageBox m_playerBoxPrefab = default;
    [SerializeField] SaveDataSource m_saveState = default;
    List<MessageBox> m_spawnedMessages = new List<MessageBox>();
    ChatHistory m_history;
    DialogueNode m_currentNode;
    public event OnDialogueEnd OnEnd;
    public event OnPromptDialogueOption OnPrompt;
    bool m_isDirty = false;
    bool m_paused = false;
    public ChatHistory History => m_history;
    public void Init(ChatHistory history)
    {
        m_history = new ChatHistory(history);
        m_currentNode = m_history.LastDialogue;
        MessageBox msg;
        foreach (var logEntry in m_history.Log)
        {
            // Display all previous messages
            msg = PrepareMessage(logEntry);
            msg?.Show();
        }
        m_isDirty = false;
    }
    public void SetPaused(bool paused)
    {
        m_paused = paused;
    }
    // Overwrite history and reload chat
    public void Overwrite(DialogueNode newChat) 
    {
        Shutdown();
        m_history.OverwriteLog(newChat);
        Init(m_history);
        CheckForReplyOptions();
    }
    public void Shutdown() 
    {
        List<MessageBox> toDelete = new List<MessageBox>(m_spawnedMessages);
        foreach (var item in toDelete)
        {
            // Clear all previous messages
            m_spawnedMessages.Remove(item);
            item.Cleanup();
            Destroy(item.gameObject);
        }
    }
    void CheckForReplyOptions()
    {
        var options = GetChatOptions();
        if (options.Count > 0)
        {
            OnPrompt?.Invoke(m_currentNode.Options);
        }
        else 
        {
            // end display loop
            OnEnd?.Invoke();
        }
    }
    // Get options for the current dialogue node, check for hidden option conditions
    IReadOnlyList<ChatOption> GetChatOptions() 
    {
        SaveData save = m_saveState.CurrentGameState;
        if (m_currentNode.HiddenOptions != null &&
            !m_currentNode.HiddenOptions.CheckForOptions(save, out var result)
            && result != null) 
        {
            return result;
        }
        else 
        {
            return m_currentNode.Options;        
        }
    } 
    // Display a new message
    MessageBox PrepareMessage(Dialogue toDisplay, bool isNpc = true) 
    {
        // Instntiate a message box for the message
        MessageBox instance = isNpc? 
            Instantiate(m_npcBoxPrefab, m_messageParent) : 
            Instantiate(m_playerBoxPrefab, m_messageParent);
        instance.Init(toDisplay);
        m_spawnedMessages.Add(instance);
        return instance;
    }
    // Append a dialogue to history
    public void UpdateCurrentDialogue(DialogueNode result) 
    {
        m_currentNode = result;
        m_history.Append(m_currentNode);
        m_isDirty = true;
    }
    // start displaying dialogues of current node
    // and continue until the end of the dialogue tree
    public void StartChat() 
    {
        if (m_isDirty) 
        {
            m_isDirty = false;
            StartCoroutine(ContinueChat(m_currentNode.Dialogues));
        }
        else 
        {
            // Called when reopening a static chatroom,
            // check for new reply override conditions
            CheckForReplyOptions();
        }
    }
    IEnumerator ContinueChat(IReadOnlyList<Dialogue> dialogues)
    {
        bool isNpc;
        MessageBox msg;
        foreach (var line in dialogues)
        {
            isNpc = line.WhoSpoke != DialogueNode.s_playerName;
            yield return new WaitUntil(() => !m_paused);
            yield return new WaitForSeconds(line.DelayBeforeTyping);
            msg = PrepareMessage(line, isNpc);
            msg.Typing();
            yield return new WaitForSeconds(line.TypingDelay);
            msg.Show();
            yield return new WaitForSeconds(0.1f);
            // Trigger any events after a dialogue is displayed
            TryTriggerAfterCurrentLine(line);
        }
        yield return new WaitForSeconds(0.5f);
        // prompt option at the end if there is any
        CheckForReplyOptions();
    }
    public static void TryTriggerAfterCurrentLine(Dialogue current)
    {
        current.TriggerAfterThisLine?.Trigger();
    }
}
