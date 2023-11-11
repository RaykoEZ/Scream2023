using System.Collections.Generic;
using Unity.VisualScripting;

public class ChatHistory
{
    public DialogueNode LastDialogue => m_lastDialogue;
    public List<Dialogue> Log => m_log;
    DialogueNode m_lastDialogue;
    List<Dialogue> m_log;

    public ChatHistory() 
    {
        m_lastDialogue = null;
        m_log = new List<Dialogue>();
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
