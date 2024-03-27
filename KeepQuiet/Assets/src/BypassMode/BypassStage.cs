﻿using Curry.Explore;
using System;
using System.Collections.Generic;
using UnityEngine;
public delegate void OnStageUpdate(BypassStage toUpdate);
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
    public virtual void InitStage(bool reset = false) 
    {
        if(m_currentSubStage >= m_subStages.SafeNodes.Count) 
        {
            return; 
        }
        if (reset) 
        {
            m_currentSubStage = 0;
        }
        BypassNode currentSafeNode = m_subStages.SafeNodes[m_currentSubStage];
        foreach (var item in m_subStages.AllNodes)
        {
            currentSafeNode.SetSafe(false);
            item?.Init();
            item.OnFail += OnMiss;
        }
        currentSafeNode.SetSafe(true);
        currentSafeNode.OnFail -= OnMiss;
        currentSafeNode.OnSuccess += OnSafe;
        currentSafeNode?.Init();
        Show();
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
            OnClear?.Invoke(this);
        }
    }
    public virtual void EndCurrentStage() 
    {
        BypassNode currentSafeNode = m_subStages.SafeNodes[m_currentSubStage];
        foreach (var item in m_subStages.AllNodes)
        {
            item.OnFail -= OnMiss;
        }
        currentSafeNode.SetSafe(false);
        currentSafeNode.OnSuccess -= OnSafe;
    }
    // callbacks for player interaction with nodes
    void OnMiss() 
    {
        OnNodeMiss?.Invoke(this);
    }
    void OnSafe() 
    {
        NextSubstage();
        Debug.Log("Correct Button!");
    }
}
