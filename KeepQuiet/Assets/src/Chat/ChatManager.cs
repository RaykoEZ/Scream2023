using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
public class IncomingMessage : HideableUI
{
    // Redirect to ContactList screen
    public void Trigger(string callerNumber, string alias = "")
    {
        throw new System.NotImplementedException();
    }
}
// Notifies player when message comes
public class ChatManager : MonoBehaviour 
{
    [SerializeField] IncomingMessage m_incoming = default;
    [SerializeField] DialogueHandler m_dialogueTree = default;
    [SerializeField] List<ChatHistoryContainer> m_historyCollection = default;
    Dictionary<string, ChatHistory> m_histories = new Dictionary<string, ChatHistory>();
    private void Start()
    {
        m_dialogueTree.OnEnd += OnChatEnd;
        // instantiate history logs and store them here for record keeping if needed
        foreach(var history in m_historyCollection) 
        {
            m_histories.Add(history.Username, history.History);
        }
        m_dialogueTree.PrepareDialogueBoxes(m_histories);
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
    // Redirect to ContactList
    public void OnNewMessage(string incomingNumber) 
    { 
    
    }
    // TODO: End chat, hide
    void OnChatEnd() 
    { 

    }
}
