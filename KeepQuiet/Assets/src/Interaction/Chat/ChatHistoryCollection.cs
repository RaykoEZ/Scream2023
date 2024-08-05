using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HisCol_", menuName = "Chat/Collection of Chat Histories", order = 1)]
public class ChatHistoryCollection : ScriptableObject 
{
    [SerializeField] List<ChatHistoryContainer> m_histories = default;
    public ChatHistory Find(string username) 
    {
        var result = m_histories.Find((i) => i.Username == username);
        return result.History;
    }
}
