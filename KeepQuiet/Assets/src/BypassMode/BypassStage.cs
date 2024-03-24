using System.Collections.Generic;
using UnityEngine;
public delegate void OnStageUpdate(BypassStage toUpdate);
// class for one stage of mini-game
public class BypassStage : MonoBehaviour 
{
    [SerializeField] List<MissNode> m_misses = default;
    [SerializeField] SafeNode m_safeNode = default;
    public event OnStageUpdate OnClear;
    public virtual void InitStage() 
    {
        foreach (var item in m_misses)
        {
            item?.Init();
            item.OnFail += OnMiss;
        }
        m_safeNode?.Init();
        m_safeNode.OnSuccess += OnSafe;
    }
    protected virtual void EndStage() 
    {
        foreach (var item in m_misses)
        {
            item.OnFail -= OnMiss;
        }
        m_safeNode.OnSuccess -= OnSafe;
    }
    // callbacks for player interaction with nodes
    void OnMiss() 
    { 
    
    }
    void OnSafe() 
    {
        EndStage();
        OnClear?.Invoke(this);
    }
}
