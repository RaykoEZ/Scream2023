using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ChatHistory
{
    [SerializeField] List<DialogueNode> m_chatLog = default;
    public DialogueNode LastDialogue => m_chatLog.Last();
    public List<DialogueNode> ChatLog { get => m_chatLog; }

    public ChatHistory() 
    {
        m_chatLog = null;
    }
    public ChatHistory(ChatHistory copy) 
    {
        m_chatLog = copy.m_chatLog;
    }
    // Clear log and overwrite all content
    public void OverwriteLog(DialogueNode lastDialogue) 
    {
        m_chatLog.Clear();
        Append(lastDialogue);
    }
    public void Append(DialogueNode append) 
    {
        m_chatLog.Add(append);
    }
}
