using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour 
{
    [SerializeField] List<Npc> m_npcInScene = default;
    public Npc Find(string phoneNumber) 
    {
        return m_npcInScene.Find(
            (npc) => 
            { 
                return npc.PhoneNumber == phoneNumber; 
            });
    }
    public void OnCallFinished(string whoCalled) 
    {
        foreach (var npc in m_npcInScene) 
        {
            npc.OnCallFinished();
        }
    }
    public void OnChatFinished(DialogueNode lastDialogue) 
    {
        foreach (var npc in m_npcInScene)
        {
            npc.OnChatFinished();
        }
    }
}
