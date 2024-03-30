using Curry.Explore;
using System;
using System.Collections.Generic;
using UnityEngine;
public delegate void OnStageUpdate(BypassStage toUpdate, bool playFeedback);
[Serializable]
public class BypassSubStage 
{
    [SerializeField] List<BypassNode> m_nodes = default;
    [SerializeField] List<BypassNode> m_safeNodes = default;

    public List<BypassNode> AllNodes { get => m_nodes; }
    public List<BypassNode> SafeNodes { get => m_safeNodes; }
}
// class for one stage of mini-game
public class BypassStage : HideableUI 
{
    [SerializeField] BypassSubStage m_subStages = default;
    public event OnStageUpdate OnClear;
    public event OnStageUpdate OnNodeMiss;
    int m_currentSubStage = 0;
    protected bool m_hasClearedOnce = false;
    protected virtual bool PlayFeedbackSequence => true;
    public bool HasClearedOnce => m_hasClearedOnce;
    public virtual void InitStage(bool reset = false) 
    {
        if(m_subStages.SafeNodes.Count > 0 &&
            (m_currentSubStage < 0 || 
            m_currentSubStage >= m_subStages.SafeNodes.Count)) 
        {
            return; 
        }
        if (reset) 
        {
            ResetClearedStage();

        }
        foreach (var item in m_subStages.AllNodes)
        {
            item.SetSafe(false);
            item?.Init();
            item.OnFail += OnMiss;
        }
        if (m_subStages.SafeNodes.Count > 0) 
        {
            BypassNode currentSafeNode = m_subStages.SafeNodes[m_currentSubStage];
            currentSafeNode.SetSafe(true);
            currentSafeNode.OnFail -= OnMiss;
            currentSafeNode.OnSuccess += OnSafe;
            currentSafeNode?.Init();
        }
        Show();
    }
    public void ResetClearedStage() 
    {
        m_currentSubStage = 0;
        GetAnim.SetBool("Cleared", false);
    }
    protected virtual void NextSubstage() 
    {
        // Stop listeners of cleared stage, start next stage listeners or end stage
        EndCurrentStage();
        if (m_currentSubStage + 1 < m_subStages.SafeNodes.Count) 
        {
            m_currentSubStage++;
            InitStage();
        }
        else 
        {
            m_hasClearedOnce = true;
            OnClear?.Invoke(this, PlayFeedbackSequence);
            GetAnim.SetBool("Cleared", true);
        }
    }
    public virtual void EndCurrentStage() 
    {
        foreach (var item in m_subStages.AllNodes)
        {
            item.OnFail -= OnMiss;
        }
        if (m_subStages.SafeNodes.Count > 0)
        {
            BypassNode currentSafeNode = m_subStages.SafeNodes[m_currentSubStage];
            currentSafeNode.SetSafe(false);
            currentSafeNode.OnSuccess -= OnSafe;
        }
    }
    // callbacks for player interaction with nodes
    protected virtual void OnMiss() 
    {
        OnNodeMiss?.Invoke(this, PlayFeedbackSequence);
    }
    protected virtual void OnSafe() 
    {
        NextSubstage();
    }
}
