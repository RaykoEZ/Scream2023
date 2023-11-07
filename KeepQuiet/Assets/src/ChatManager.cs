using System.Collections.Generic;
using UnityEngine;
public class ChatManager : MonoBehaviour 
{
    [SerializeField] Transform m_messageParent = default;
    Dictionary<string, ChatHistory> m_histories = new Dictionary<string, ChatHistory>();
    string m_chattingWith;
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
