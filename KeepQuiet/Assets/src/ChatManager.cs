using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class ChatManager : MonoBehaviour 
{
    [SerializeField] DialogueTreeManager m_dialogueTree = default;
    private void Start()
    {
        m_dialogueTree.OnEnd += OnChatEnd;
    }
    private void OnDestroy()
    {
        m_dialogueTree.OnEnd -= OnChatEnd;
    }
    // load chat of the person in question
    public void BeginChat(string name) 
    {
        if (string.IsNullOrEmpty(name)) return;
        m_dialogueTree?.StartDialogue(name);
    }
    void OnChatEnd() 
    { 
    }
}
// A class to display a text message
public class MessageBox : MonoBehaviour 
{ 
    
}
public delegate void OnPromptDialogueOption(List<DialogueNode> options);
// Contains and displays text message boxes for a NPC chat
public class DialogueDisplay : HideableUI
{
    [SerializeField] Transform m_messageParent = default;
    [SerializeField] MessageBox m_messageBoxPrefab = default;
    [SerializeField] TextMeshProUGUI m_title = default;
    bool m_inProgress = false;
    List<MessageBox> m_spawnedMessages = new List<MessageBox>();
    ChatHistory m_history;
    DialogueNode m_currentNode;
    public event OnDialogueEnd OnEnd;
    public event OnPromptDialogueOption OnPrompt;
    public ChatHistory History => m_history; 
    public void Init(string title, ChatHistory history)
    {
        m_title.text = title;
        m_history = history;
        foreach (var logEntry in m_history.Log)
        {
            // Display all previous messages
            DisplayMessage(logEntry, false);
        }
    }
    public void NextDialogue()
    {
        if (m_currentNode.Options.Count > 1)
        {
            m_inProgress = false;
            OnPrompt?.Invoke(m_currentNode.Options);
        }
        else if (m_currentNode.Options.Count == 1)
        {
            m_history.Append(m_currentNode.Dialogue);
            m_currentNode = m_currentNode.Options[0];
            DisplayMessage(m_currentNode.Dialogue);
        }
    }
    // Display a new message, append to chat history
    public void DisplayMessage(Dialogue toDisplay, bool appendToHistory = true) 
    {
        // TODO: display in box

        //append to history
        if (appendToHistory) 
        {
            m_history.Append(toDisplay);
        }

    }
    public void EndDialogue()
    {
        OnEnd?.Invoke();
    }
    IEnumerator ContinueChat()
    {
        while (m_inProgress)
        {
            NextDialogue();
            // TODO: Simulate typing effect, will animate in future
            yield return new WaitForSeconds(1f);

        }
    }
}

public delegate void OnDialogueEnd();
// Need a node-network for dialogue states & decisions
public class DialogueTreeManager : MonoBehaviour
{
    [SerializeField] Transform m_dialogueBoxParent = default;
    [SerializeField] DialogueCollection m_dialogueTree = default;
    [SerializeField] DialogueOptionPrompter m_optionPrompt = default;
    [SerializeField] DialogueDisplay m_dialogueBoxToSpawn = default;
    Dictionary<string, DialogueDisplay> m_spawnedDialogueBoxes = new Dictionary<string, DialogueDisplay>();
    // all dialogues displayed before ending dialogue
    DialogueDisplay m_currentDisplay;
    public event OnDialogueEnd OnEnd;
    private void Start()
    {
        m_optionPrompt.OnChosen += OnPlayerChosen;
    }
    private void OnDestroy()
    {
        Shutdown();
    }
    public void PrepareDialogueBoxes(Dictionary<string, ChatHistory> histories) 
    { 
        foreach (var kvp in histories) 
        {
            DialogueDisplay instance = Instantiate(m_dialogueBoxToSpawn, m_dialogueBoxParent);
            instance.Init(kvp.Key, kvp.Value);
            instance.OnPrompt += OnPromptChoice;
            instance.OnEnd += EndDialogue;
            m_spawnedDialogueBoxes.Add(kvp.Key, instance);
        }
    }
    public void Shutdown() 
    {
        m_optionPrompt.OnChosen -= OnPlayerChosen;
        // shutdown all message boxes
        foreach (var kvp in m_spawnedDialogueBoxes) 
        {
            kvp.Value.OnPrompt -= OnPromptChoice;
            kvp.Value.OnEnd -= EndDialogue;
            Destroy(kvp.Value);
            m_spawnedDialogueBoxes.Remove(kvp.Key);
        }
    }
    public void StartDialogue(string chattingWith) 
    {
        if (!m_spawnedDialogueBoxes.TryGetValue(chattingWith, out DialogueDisplay result)) 
        {
            OnEnd?.Invoke();
            return; 
        }
        m_currentDisplay = result;
        // Display the preloaded ui for chatting with this NPC
        m_currentDisplay.Show();
    }
    void EndDialogue() 
    {
        OnEnd?.Invoke();
    }
    void OnPromptChoice(List<DialogueNode> options)
    { 
        
    }
    void OnPlayerChosen(DialogueNode chosen) 
    { 
        
    }
}
