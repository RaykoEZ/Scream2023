using System.Collections.Generic;

public class ChatHistory
{
    public int LastDialogueIndex => m_lastDialogueIndex;
    public List<Dialogue> Log => m_log;
    int m_lastDialogueIndex;

    List<Dialogue> m_log;

    public ChatHistory() 
    {
        m_lastDialogueIndex = 0;
        m_log = new List<Dialogue>();
    }
    public ChatHistory(int lastDialogue, List<Dialogue> log) 
    {
        m_lastDialogueIndex = lastDialogue;
        m_log = log;
    }
    public void OverwriteLog(int lastDialogue, List<Dialogue> overwrite) 
    {
        m_lastDialogueIndex = lastDialogue;
        m_log = overwrite;
    }
    public void Remove(Dialogue remove) 
    {
        m_log?.Remove(remove);
    }
    public void Append(List<Dialogue> append) 
    {
        m_log.AddRange(append);
    }
    public void Append(Dialogue append) 
    {
        m_log.Add(append);
    }
}
