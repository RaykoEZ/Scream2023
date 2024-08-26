using Curry.Events;
using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
// Notifies player when message comes
public class ChatManager : HideableUI 
{
    [SerializeField] ChatRoom m_chatRoom = default;
    [SerializeField] ChatHistoryCollection m_histories = default;
    public event OnChatUpdate OnEnd;
    private void OnDestroy()
    {
        Shutdown();
    }
    public void Shutdown()
    {
        // shutdown chat room
        m_chatRoom?.Shutdown();
    }
    // load chat of the person in question
    public void BeginChat(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) return;
        ChatHistory result = m_histories.Find(username);
        // instantiate history logs and store them here for record keeping if needed
        m_chatRoom.SetChatHistory(result);
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
    public void OnNewMessage(DialogueNode newDialogue, string username) 
    {
        ChatHistory result = m_histories.Find(username);
        // instantiate history logs and store them here for record keeping if needed
        m_chatRoom.SetChatHistory(result);
        m_chatRoom.Hide();
        m_chatRoom.NewCurrentDialogue(newDialogue);
    }
    void EndDialogue()
    {
        // unlisten dialogue events
        m_chatRoom.OnEnd -= EndDialogue;
        OnEnd?.Invoke();
    }
}
