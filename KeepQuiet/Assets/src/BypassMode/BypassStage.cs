using System.Collections.Generic;
using UnityEngine;
// class for one stage of mini-game
public class BypassStage : MonoBehaviour 
{
    [SerializeField] List<MissNode> m_misses = default;
    [SerializeField] SafeNode m_safeNode = default;
    public void LoadStage() 
    {
        foreach (var item in m_misses)
        {
            item?.Init();
        }
        m_safeNode?.Init();
    }
}
