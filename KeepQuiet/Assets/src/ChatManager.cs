using System;
using System.Collections.Generic;
using UnityEngine;
public class ChatManager : MonoBehaviour 
{
    [SerializeField] Transform m_messageParent = default;
    Dictionary<string, ChatHistory> m_histories = new Dictionary<string, ChatHistory>();
    string m_chattingWith;
    ChatHistory m_current;
    // load chat of the person in question
    public void BeginChat(string name) 
    {
        if (string.IsNullOrEmpty(name)) 
        {
            return;
        }
        m_chattingWith = name;
    }
    public void EndChat() 
    { 
    
    }
    public void OnNpcReply() { }
    public void PromptChoice() { }
    public void OnPlayerChosen() { }
}
// A class to traverse and to keep the state of Dialogue nodes
[Serializable]
public class DialogueTree 
{
    [SerializeField] DialogueNode m_startNode = default;
    // If no replies, this dialogue tree ends
    public List<Reply> Next() 
    {
        List<Reply> ret = new List<Reply>();
        return ret;
    }
    public void LoadNewTree(DialogueNode newStart) 
    {
        m_startNode = newStart;
    }
}
// Need a node-network for dialogue states & decisions
public class DialogueTreeManager : MonoBehaviour
{
    [SerializeField] DialogueTree m_dialogueTree = default;

}
