using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// Contains and displays text message boxes for a NPC chat
public class DialogueDisplay : HideableUI
{
    [SerializeField] Transform m_messageParent = default;
    [SerializeField] MessageBox m_npcBoxPrefab = default;
    [SerializeField] MessageBox m_playerBoxPrefab = default;
    List<MessageBox> m_spawnedMessages = new List<MessageBox>();
    ChatHistory m_history;
    DialogueNode m_currentNode;
    Coroutine m_chatting;
    public event OnDialogueEnd OnEnd;
    public event OnPromptDialogueOption OnPrompt;
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
    }
    void NextDialogue()
    {
        if (m_currentNode.Options.Count > 1)
        {
            m_chatting = null;
            OnPrompt?.Invoke(m_currentNode.Options);
        }
        else if (m_currentNode.Options.Count == 1)
        {
            UpdateCurrentDialogue(m_currentNode.Options[0]);
            StartChat();
        }
        else 
        {
            EndDialogue();
        }
    }
    // Display a new message
    void DisplayMessage(Dialogue toDisplay, bool isNpc = true) 
    {
        // Instntiate a message box for the message
        MessageBox instance = isNpc? 
            Instantiate(m_npcBoxPrefab, m_messageParent) : 
            Instantiate(m_playerBoxPrefab, m_messageParent);
        instance.Init(toDisplay.WhoSpoke, toDisplay.Content);
        m_spawnedMessages.Add(instance);
        instance.Show();
    }
    // Append a dialogue to history
    public void UpdateCurrentDialogue(DialogueNode result) 
    {
        m_currentNode = result;
        m_history.Append(m_currentNode);
    }
    // start displaying dialogues of current node
    // and continue until the end of the dialogue tree
    public void StartChat() 
    {
        m_chatting = StartCoroutine(ContinueChat(m_currentNode.Dialogues, m_currentNode.WhoSpoke != DialogueNode.s_playerName));
    }
    public void EndDialogue()
    {
        OnEnd?.Invoke();
    }
    IEnumerator ContinueChat(IReadOnlyList<Dialogue> dialogues, bool isNpc = true)
    {
        foreach (var line in dialogues)
        {
            DisplayMessage(line, isNpc);
            // TODO: Simulate typing effect, will animate in future
            yield return new WaitForSeconds(1f);
        }
        // prompt option at the end if there is any
        NextDialogue();
    }
}
