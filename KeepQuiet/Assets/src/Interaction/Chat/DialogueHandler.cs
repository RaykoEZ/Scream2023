using System.Collections.Generic;
using TMPro;
using UnityEngine;
public delegate void OnPromptDialogueOption(IReadOnlyList<ChatOption> options);
public delegate void OnDialogueEnd();
// Need a node-network for dialogue states & decisions
public class DialogueHandler : MonoBehaviour
{
    [SerializeField] Transform m_dialogueBoxParent = default;
    [SerializeField] DialogueOptionPrompter m_optionPrompt = default;
    [SerializeField] ChatRoom m_chatRoomToSpawn = default;
    [SerializeField] NpcManager m_npc = default;
    Dictionary<string, ChatRoom> m_spawnedDialogueBoxes = new Dictionary<string, ChatRoom>();
    // all dialogues displayed before ending dialogue
    ChatRoom m_currentDisplay;
    public event OnDialogueEnd OnEnd;
    Npc m_chattingWith;
    private void Start()
    {
        m_optionPrompt.OnChosen += OnNewDialogue;
    }
    private void OnDestroy()
    {
        Shutdown();
    }
    // Instantiate all chat UIs, call at start of chat
    public void PrepareDialogueBoxes(Dictionary<string, ChatHistory> histories) 
    { 
        foreach (var kvp in histories) 
        {
            ChatRoom instance = Instantiate(m_chatRoomToSpawn, m_dialogueBoxParent);
            instance.Init(kvp.Value);
            m_spawnedDialogueBoxes.Add(kvp.Key, instance);
        }
    }
    public void Shutdown() 
    {
        m_optionPrompt.OnChosen -= OnNewDialogue;
        // shutdown all message boxes
        foreach (var kvp in m_spawnedDialogueBoxes) 
        {
            Destroy(kvp.Value);
        }
        m_spawnedDialogueBoxes.Clear();
    }
    // Start dialogue with npc
    public void StartDialogue(string chattingWith) 
    {
        if (!m_spawnedDialogueBoxes.TryGetValue(chattingWith, out ChatRoom result)) 
        {
            OnEnd?.Invoke();
            return; 
        }
        HideAll();
        m_currentDisplay = result;
        StartCurrentChat();
    }

    // Start a new dialogue tree from a sender
    public void IncomingDialogue(DialogueNode newDialogue) 
    {
        string chatting = newDialogue.Title;
        if (!m_spawnedDialogueBoxes.TryGetValue(chatting, out ChatRoom result))
        {
            OnEnd?.Invoke();
            return;
        }
        HideAll();
        m_currentDisplay = result;
        // Set current dialogue to the incoming dialogue
        m_currentDisplay.UpdateCurrentDialogue(newDialogue);
    }
    void HideAll() 
    {
        foreach (var kvp in m_spawnedDialogueBoxes)
        {
            kvp.Value.Hide();
        }
    }
    void StartCurrentChat() 
    {
        m_chattingWith = m_npc.Get(m_currentDisplay.History.LastDialogue.Title);
        // listen to dialogue update events
        m_currentDisplay.OnPrompt += OnPromptChoice;
        m_currentDisplay.OnEnd += EndDialogue;
        // Display the preloaded ui for chatting with this NPC
        m_currentDisplay.Show();
        m_currentDisplay.StartChat();
    }
    void EndDialogue() 
    {
        // unlisten dialogue events
        m_currentDisplay.OnPrompt -= OnPromptChoice;
        m_currentDisplay.OnEnd -= EndDialogue;
        OnEnd?.Invoke();
    }
    void OnPromptChoice(IReadOnlyList<ChatOption> options)
    {
        m_optionPrompt.PromptOption(options);
    }
    // A new dialogue is chosen for the current display
    void OnNewDialogue(DialogueNode chosen, int choiceIndex)
    {
        m_chattingWith?.OnPlayerDecided(chosen, choiceIndex);
        m_currentDisplay.UpdateCurrentDialogue(chosen);
        m_currentDisplay.StartChat();
    }
}
