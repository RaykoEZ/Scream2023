using System.Collections.Generic;
public struct TextMessage
{
    public static readonly string s_playerName = "You";
    public string SenderName;
    public string Content;
}
public class ChatHistory
{
    public IReadOnlyList<TextMessage> Log => m_log;
    List<TextMessage> m_log;
    public ChatHistory() 
    {
        m_log = new List<TextMessage>();
    }
    public ChatHistory(List<TextMessage> log) 
    {
        m_log = log;
    }
    public void OverwriteLog(List<TextMessage> overwrite) 
    {
        m_log = overwrite;
    }
    public void Remove(TextMessage remove) 
    {
        m_log?.Remove(remove);
    }
    public void Append(List<TextMessage> append) 
    {
        m_log.AddRange(append);
    }
    public void Append (TextMessage append) 
    {
        m_log.Add(append);
    }
}
