using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
public delegate void OnStageUpdate(BypassStage toUpdate);
// class for one stage of mini-game
public class BypassStage : HideableUI 
{
    [SerializeField] List<MissNode> m_misses = default;
    [SerializeField] SafeNode m_safeNode = default;
    public event OnStageUpdate OnClear;
    public event OnStageUpdate OnNodeMiss;
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
        Hide();
    }
    // callbacks for player interaction with nodes
    void OnMiss() 
    {
        OnNodeMiss?.Invoke(this);
    }
    void OnSafe() 
    {
        EndStage();
        Debug.Log("Correct Button!");
        OnClear?.Invoke(this);
    }
}
