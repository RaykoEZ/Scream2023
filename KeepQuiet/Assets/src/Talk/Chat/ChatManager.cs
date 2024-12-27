using Curry.Explore;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
// Notifies player when message comes
public class ChatManager : HideableUI 
{
    [SerializeField] GameSaveSource m_save = default;
    [SerializeField] DialogueContainer m_dialogueContainer = default;
    [SerializeField] ChatRoom m_chatRoom = default;
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
        ChatHistory result = FindHistory(username, m_save.Current.ChatHistories);
        if (result == null) return;
        //instantiate history logs and store them here for record keeping if needed
        if(m_dialogueContainer.CurrentChatLogs.TryGetValue(result.Username, out var log))
        {
            m_chatRoom.SetChatLog(log);
            m_chatRoom.Hide();
            StartCurrentChat();
            Show();
        }
    }
    // Redirect to ContactList
    public void OnNewMessage(DialogueNode newDialogue, string username)
    {
        ChatHistory result = FindHistory(username, m_save.Current.ChatHistories);
        if (result == null) return;
        if (m_dialogueContainer.CurrentChatLogs.TryGetValue(result.Username, out var log))
        {
            // instantiate history logs and store them here for record keeping if needed
            m_chatRoom.SetChatLog(log);
            m_chatRoom.Hide();
            m_chatRoom.NewDialogue(newDialogue);
        }
    }
    void StartCurrentChat()
    {
        // listen to dialogue update events
        m_chatRoom.OnEnd += EndDialogue;
        // Display the preloaded ui for chatting with this NPC
        m_chatRoom.Show();
        m_chatRoom.StartChat();
    }
    void EndDialogue()
    {
        // unlisten dialogue events
        m_chatRoom.OnEnd -= EndDialogue;
        OnEnd?.Invoke();
    }
    public static ChatHistory FindHistory(string name, List<ChatHistory> list)
    {
        if (list == null || string.IsNullOrWhiteSpace(name))
        {
            return null;
        }
        var result = list.Find(x => x.Username == name);
        return result;
    }
}
