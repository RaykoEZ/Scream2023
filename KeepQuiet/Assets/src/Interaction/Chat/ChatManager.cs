using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
// Notifies player when message comes
public class ChatManager : HideableUI 
{
    [SerializeField] ChatRoomManager m_chatroomHandler = default;
    [SerializeField] ChatHistoryContainer m_historySource = default;
    private void Start()
    {
        // instantiate history logs and store them here for record keeping if needed
        m_chatroomHandler.PrepareDialogueBoxes(m_historySource.History);
    }
    // load chat of the person in question
    public void BeginChat() 
    {
        if (string.IsNullOrEmpty(name)) return;
        m_chatroomHandler?.StartDialogue();
        Show();
    }
    // Redirect to ContactList
    public void OnNewMessage(DialogueNode newDialogue) 
    {
        m_chatroomHandler.IncomingDialogue(newDialogue);
    }
}
