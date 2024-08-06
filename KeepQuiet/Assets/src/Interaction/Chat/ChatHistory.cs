using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChatHistory
{
    [SerializeField] DialogueNode m_lastDialogue;
    public DialogueNode LastDialogue => m_lastDialogue;
    public ChatHistory() 
    {
        m_lastDialogue = null;
    }
    public ChatHistory(ChatHistory copy) 
    {
        m_lastDialogue = copy.m_lastDialogue;
    }
    // Clear log and overwrite all content
    public void OverwriteLog(DialogueNode lastDialogue) 
    {
        m_lastDialogue = lastDialogue;
    }
    public void Append(DialogueNode append) 
    {
        m_lastDialogue = append;
    }
}
