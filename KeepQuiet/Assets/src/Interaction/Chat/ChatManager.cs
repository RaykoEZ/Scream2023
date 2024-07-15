using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
// Notifies player when message comes
public class ChatManager : HideableUI 
{
    [SerializeField] ChatRoom m_chatRoom = default;
    [SerializeField] ChatHistoryContainer m_historySource = default;
    public event OnChatUpdate OnEnd;
    private void Start()
    {
        // instantiate history logs and store them here for record keeping if needed
        m_chatRoom.Init(m_historySource.History);
    }
    private void OnDestroy()
    {
        Shutdown();
    }
    public void Shutdown()
    {
        // shutdown chat room
        m_chatRoom.Shutdown();
    }
    // load chat of the person in question
    public void BeginChat() 
    {
        if (string.IsNullOrEmpty(name)) return;
        m_chatRoom.Hide();
        StartCurrentChat();
        Show();
    }
    void StartCurrentChat()
    {
        // listen to dialogue update events
        m_chatRoom.OnEnd += EndDialogue;
        // Display the preloaded ui for chatting with this NPC
        m_chatRoom.Show();
        m_chatRoom.StartChat();
    }
    // Redirect to ContactList
    public void OnNewMessage(DialogueNode newDialogue) 
    {
        m_chatRoom.Hide();
        // Set current dialogue to the incoming dialogue
        m_chatRoom.UpdateCurrentDialogue(newDialogue);
    }
    void EndDialogue()
    {
        // unlisten dialogue events
        m_chatRoom.OnEnd -= EndDialogue;
        OnEnd?.Invoke();
    }
}
