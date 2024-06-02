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
    List<MessageBox> m_spawnedMessages = new List<MessageBox>();
    ChatHistory m_history;
    DialogueNode m_currentNode;
    public event OnDialogueEnd OnEnd;
    public event OnPromptDialogueOption OnPrompt;
    bool m_isDirty = false;
    public ChatHistory History => m_history; 
    public void Init(ChatHistory history)
    {
        m_history = new ChatHistory(history);
        m_currentNode = m_history.LastDialogue;
        foreach (var logEntry in m_history.Log)
        {
            // Display all previous messages
            DisplayMessage(logEntry);
        }
        m_isDirty = false;
    }
    public void Shutdown() 
    {
        foreach (var message in m_spawnedMessages)
        {
            // Display all previous messages
            message.Cleanup();
        }
    }
    void NextDialogue()
    {
        if (m_currentNode.Options.Count > 0)
        {
            OnPrompt?.Invoke(m_currentNode.Options);
        }
        else 
        {
            // end display loop
            m_isDirty = false;
            OnEnd?.Invoke();
        }
    }
    // Display a new message
    void DisplayMessage(Dialogue toDisplay, bool isNpc = true) 
    {
        // Instntiate a message box for the message
        MessageBox instance = isNpc? 
            Instantiate(m_npcBoxPrefab, m_messageParent) : 
            Instantiate(m_playerBoxPrefab, m_messageParent);
        instance.Init(toDisplay);
        m_spawnedMessages.Add(instance);
        instance.Show();
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
            StartCoroutine(ContinueChat(m_currentNode.Dialogues));
        }
    }
    IEnumerator ContinueChat(IReadOnlyList<Dialogue> dialogues)
    {
        bool isNpc;
        foreach (var line in dialogues)
        {
            isNpc = line.WhoSpoke != DialogueNode.s_playerName;
            // TODO: Simulate typing effect, will animate in future
            yield return new WaitForSeconds(line.TypingDelay);
            DisplayMessage(line, isNpc);
        }
        yield return new WaitForSeconds(0.5f);
        // prompt option at the end if there is any
        NextDialogue();
    }
}
