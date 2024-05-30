using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
// Notifies player when message comes
public class ChatManager : HideableUI 
{
    [SerializeField] DialogueHandler m_dialogueTree = default;
    [SerializeField] List<ChatHistoryContainer> m_historyCollection = default;
    Dictionary<string, ChatHistory> m_histories = new Dictionary<string, ChatHistory>();
    private void Start()
    {
        // instantiate history logs and store them here for record keeping if needed
        foreach(var history in m_historyCollection) 
        {
            m_histories.Add(history.Username, history.History);
        }
        m_dialogueTree.PrepareDialogueBoxes(m_histories);
    }
    // load chat of the person in question
    public void BeginChat(string name) 
    {
        if (string.IsNullOrEmpty(name)) return;
        m_dialogueTree?.StartDialogue(name);
        Show();
    }
    // Redirect to ContactList
    public void OnNewMessage(DialogueNode newDialogue) 
    {
        m_dialogueTree.IncomingDialogue(newDialogue);
    }
}
