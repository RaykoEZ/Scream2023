using UnityEngine;

[CreateAssetMenu(fileName = "History_", menuName ="Dialogue/History", order = 1)]
public class ChatHistoryContainer : ScriptableObject 
{
    [SerializeField] string m_username = default;
    [SerializeField] ChatHistory m_history = default;
    public ChatHistory History => m_history;

    public string Username => m_username; 

    public void Overwrite(ChatHistory overwrite) 
    {
        if (overwrite == null) return;

        m_history = overwrite;
    }
}
