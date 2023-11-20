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
}
