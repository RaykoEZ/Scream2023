﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChatHistory
{
    [SerializeField] DialogueNode m_lastDialogue;
    [SerializeField] List<Dialogue> m_log;
    public DialogueNode LastDialogue => m_lastDialogue;
    public List<Dialogue> Log => m_log;


    public ChatHistory() 
    {
        m_lastDialogue = null;
        m_log = new List<Dialogue>();
    }
    public ChatHistory(ChatHistory copy) 
    {
        m_lastDialogue = copy.m_lastDialogue;
        m_log = new List<Dialogue>(copy.Log);
    }
    public ChatHistory(DialogueNode lastDialogue, List<Dialogue> log) 
    {
        m_lastDialogue = lastDialogue;
        m_log = log;
    }
    public void OverwriteLog(DialogueNode lastDialogue, List<Dialogue> overwrite) 
    {
        m_lastDialogue = lastDialogue;
        m_log = overwrite;
    }
    public void Append(List<DialogueNode> append) 
    {
        if (append.Count == 0) return;

        foreach(var node in append) 
        {
            m_log.AddRange(node.Dialogues);
        }
        m_lastDialogue = append[append.Count - 1];
    }
    public void Append(DialogueNode append) 
    {
        m_log.AddRange(append.Dialogues);
        m_lastDialogue = append;
    }
}
