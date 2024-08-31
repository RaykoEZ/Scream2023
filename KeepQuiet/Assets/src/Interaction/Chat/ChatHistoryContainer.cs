using System;
using UnityEngine;

[CreateAssetMenu(fileName = "History_", menuName ="Chat/History for one NPC", order = 1)]
public class ChatHistoryContainer : ScriptableObject 
{
    [SerializeField] ChatHistoryItem m_history = default;
    public ChatHistory History => m_history.History;
    public string Username => m_history.Username; 
    public void Overwrite(ChatHistory overwrite) 
    {
        if (overwrite == null) return;
        m_history.History = overwrite;
    }
}
[Serializable]
public struct ChatHistoryItem 
{
    public string Username;
    public ChatHistory History;
}
