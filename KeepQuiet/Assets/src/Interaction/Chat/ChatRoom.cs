using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void OnChatUpdate();
// Contains and displays text message boxes for a NPC chat
public class ChatRoom : HideableUI
{
    [SerializeField] UnityEvent m_onThoughtDialogue = default;
    [SerializeField] Transform m_messageParent = default;
    [SerializeField] ReplyPrompter m_optionPrompt = default;
    [SerializeField] MessageBox m_npcBoxPrefab = default;
    [SerializeField] MessageBox m_playerBoxPrefab = default;
    List<MessageBox> m_spawnedMessages = new List<MessageBox>();
    ChatHistory m_history;
    DialogueNode m_currentNode;
    Coroutine m_chatting;
    public event OnChatUpdate OnEnd;
    bool m_isDirty = false;
    bool m_paused = false;
    public ChatHistory History => m_history;
    void OnEnable()
    {
        m_optionPrompt.OnChosen += OnReplyChosen;
    }
    void OnDisable()
    {
        Shutdown();
    }
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
    public void OnThoughtDrop(ThoughtBubble thought) 
    {
        if (thought.DetailRef == null) return;
        // Find a dialogue outcome from dropping the thought
        DialogueNode outcome = m_currentNode.FindThoughtOutcome(thought.DetailRef);
        if (outcome != null) 
        {
            // Stop current Dialogue and move to the new dialogue line
            StartCoroutine(InterruptChat(outcome));
            thought.ConsumeBubble();
            m_onThoughtDialogue?.Invoke();
        }
    }
    public void Shutdown() 
    {
        m_optionPrompt.OnChosen -= OnReplyChosen;
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
        var options = m_currentNode.Options;
        if (options.Count > 0)
        {
            m_optionPrompt.PromptOption(options);
        }
        else 
        {
            // end display loop
            OnEnd?.Invoke();
        }
    }
    // A new dialogue is chosen for the current display
    void OnReplyChosen(DialogueNode chosen)
    {
        if (chosen == null) return;
        UpdateCurrentDialogue(chosen);
        StartChat();
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
            m_chatting = StartCoroutine(ContinueChat(m_currentNode.Dialogues));
        }
        else 
        {
            // Called when reopening a static chatroom,
            // check for new reply override conditions
            CheckForReplyOptions();
        }
    }
    IEnumerator InterruptChat(DialogueNode outcome) 
    {
        m_optionPrompt?.HideAll();
        // Stop current Dialogue and move to the new dialogue line
        UpdateCurrentDialogue(outcome);
        // Wat until previous chat finish resolving last line
        yield return new WaitUntil(() => m_chatting == null);
        yield return new WaitForSeconds(0.5f);
        StartChat();
    }
    IEnumerator ContinueChat(IReadOnlyList<Dialogue> dialogues)
    {
        bool isNpc;
        MessageBox msg;
        foreach (var line in dialogues)
        {
            //skip empty content
            if (line.Content == null || string.IsNullOrEmpty(line.Content)) continue;
            isNpc = line.WhoSpoke != DialogueNode.s_playerName;
            yield return new WaitUntil(() => !m_paused);
            yield return new WaitForSeconds(line.DelayBeforeTyping);
            msg = PrepareMessage(line, isNpc);
            msg.Typing();
            yield return new WaitForSeconds(line.TypingDelay);
            msg.Show();
            // Break subroutine if we have interrupted the chat (thought bubble)
            if (m_isDirty == true)
            {
                m_chatting = null;
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
            // Trigger any events after a dialogue is displayed
            TryTriggerAfterCurrentLine(line);
        }
        yield return new WaitForSeconds(0.5f);
        m_chatting = null;
        // prompt option at the end if there is any
        CheckForReplyOptions();
    }
    public static void TryTriggerAfterCurrentLine(Dialogue current)
    {
        current.TriggerAfterThisLine?.Trigger();
    }
}