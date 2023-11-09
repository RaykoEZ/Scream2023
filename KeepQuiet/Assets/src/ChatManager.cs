using System;
using System.Collections.Generic;
using UnityEngine;
public class ChatManager : MonoBehaviour 
{
    [SerializeField] Transform m_messageParent = default;
    [SerializeField] DialogueTreeManager m_dialogueTree = default;
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

}
public delegate void OnDialogueEnd(int lastIndex);
// Need a node-network for dialogue states & decisions
public class DialogueTreeManager : MonoBehaviour
{
    [SerializeField] DialogueCollection m_dialogueTree = default;
    DialogueNode m_current;
    public event OnDialogueEnd OnEnd;
    public void StartDialogue(string chattingWith, int lastMessageIndex = 0) 
    {
        m_current = m_dialogueTree.GetNode(chattingWith, lastMessageIndex);
        if(m_current != null) 
        { 
            // display dialogue and prompt option if available
        }
        else 
        {
            OnEnd?.Invoke(lastMessageIndex);
            return;
        }
    }
    public void OnNextDialogue() 
    { 
    
    }
    public void PromptChoice()
    { 
    
    }
    public void OnPlayerChosen() 
    { 
    
    }
}
