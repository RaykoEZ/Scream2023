using Curry.Events;
using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
// Notifies player when message comes
public class ChatManager : HideableUI 
{
    [SerializeField] ChatRoom m_chatRoom = default;
    [SerializeField] ChatHistoryCollection m_histories = default;
    [SerializeField] CurryGameEventListener m_onGameSetup = default;
    public event OnChatUpdate OnEnd;
    void OnEnable()
    {
        m_onGameSetup?.Init();
    }
    void OnDisable()
    {
        m_onGameSetup?.Shutdown();
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
    
    public void SetupChatHistory(EventInfo info) 
    { 
    
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
