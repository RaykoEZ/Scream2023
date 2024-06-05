using System.Collections.Generic;
using TMPro;
using UnityEngine;
public delegate void OnPromptDialogueOption(IReadOnlyList<ChatOption> options);
public delegate void OnDialogueEnd();
// Need a node-network for dialogue states & decisions
public class ChatRoomManager : MonoBehaviour
{
    [SerializeField] ReplyPrompter m_optionPrompt = default;
    [SerializeField] ChatRoom m_chatRoom = default;
    // all dialogues displayed before ending dialogue
    public event OnDialogueEnd OnEnd;
    private void Start()
    {
        m_optionPrompt.OnChosen += OnReplyChosen;
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
        m_optionPrompt.OnChosen -= OnReplyChosen;
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
    void OnReplyChosen(DialogueNode chosen, int choiceIndex)
    {
        if (chosen == null) return;
        m_chatRoom.UpdateCurrentDialogue(chosen);
        m_chatRoom.StartChat();
    }
}
