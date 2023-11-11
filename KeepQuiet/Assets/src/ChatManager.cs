using Curry.Explore;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class ChatManager : MonoBehaviour 
{
    [SerializeField] HideableUI m_ui = default;
    [SerializeField] DialogueHandler m_dialogueTree = default;
    private void Start()
    {
        m_dialogueTree.OnEnd += OnChatEnd;
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
    void OnChatEnd() 
    { 
    }
}
