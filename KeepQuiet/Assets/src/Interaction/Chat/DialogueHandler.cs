using System.Collections.Generic;
using TMPro;
using UnityEngine;
public delegate void OnPromptDialogueOption(IReadOnlyList<ChatOption> options);
public delegate void OnDialogueEnd();
// Need a node-network for dialogue states & decisions
public class DialogueHandler : MonoBehaviour
{
    [SerializeField] DialogueOptionPrompter m_optionPrompt = default;
    [SerializeField] ChatRoom m_chatRoom = default;
    [SerializeField] NpcManager m_npc = default;
    // all dialogues displayed before ending dialogue
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
            m_chatRoom.Init(kvp.Value);
        }
    }
    public void Shutdown() 
    {
        m_optionPrompt.OnChosen -= OnNewDialogue;
        // shutdown chat room
        m_chatRoom.Shutdown();
    }
    // Start dialogue with npc
    public void StartDialogue() 
    {
        HideAll();
        StartCurrentChat();
    }

    // Start a new dialogue tree from a sender
    public void IncomingDialogue(DialogueNode newDialogue) 
    {
        string chatting = newDialogue.Title;
        HideAll();
        // Set current dialogue to the incoming dialogue
        m_chatRoom.UpdateCurrentDialogue(newDialogue);
    }
    void HideAll() 
    {
        m_chatRoom.Hide();
    }
    void StartCurrentChat() 
    {
        m_chattingWith = m_npc.Get(m_chatRoom.History.LastDialogue.Title);
        // listen to dialogue update events
        m_chatRoom.OnPrompt += OnPromptChoice;
        m_chatRoom.OnEnd += EndDialogue;
        // Display the preloaded ui for chatting with this NPC
        m_chatRoom.Show();
        m_chatRoom.StartChat();
    }
    void EndDialogue() 
    {
        // unlisten dialogue events
        m_chatRoom.OnPrompt -= OnPromptChoice;
        m_chatRoom.OnEnd -= EndDialogue;
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
        m_chatRoom.UpdateCurrentDialogue(chosen);
        m_chatRoom.StartChat();
    }
}
