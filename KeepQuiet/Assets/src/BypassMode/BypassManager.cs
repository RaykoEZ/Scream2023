using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Curry.Explore;
using UnityEngine.UI;
using TMPro;
// A class to handle multiple stages of spot-the-odd-one mini-game 
public class BypassManager : HideableUI 
{
    [SerializeField] PlayableAsset m_onCorrect = default;
    [SerializeField] PlayableAsset m_onMiss = default;
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] List<BypassStage> m_stages = default;
    [SerializeField] TextMeshProUGUI m_stageCount = default;
    int m_currentStage = 0;
    public void Start()
    {
        Init(0);
        StartCurrentStage();
    }
    public override void Hide()
    {
        StopCurrentStage();
        Init(0);
        base.Hide();
    }
    public void Init(int currentStage)
    {
        m_currentStage = currentStage;
        UpdateStageNumber();
    }
    private void UpdateStageNumber()
    {
        m_stageCount.text = $"{m_currentStage + 1}/{m_stages.Count}";
    }
    public void StartCurrentStage() 
    { 
        if (m_currentStage < 0 && m_currentStage >= m_stages.Count) 
        {
            Debug.LogWarning("Bypass stage index out of range");
            return;
        }
        BypassStage currentStage = m_stages[m_currentStage];
        currentStage.OnClear += OnStageClear;
        currentStage.OnNodeMiss += OnMiss;
        currentStage?.InitStage(reset: true);
    }
    void StopCurrentStage() 
    {
        if (m_currentStage < 0 && m_currentStage >= m_stages.Count)
        {
            Debug.LogWarning("Bypass stage index out of range");
            return;
        }
        BypassStage currentStage = m_stages[m_currentStage];
        currentStage.OnClear -= OnStageClear;
        currentStage.OnNodeMiss -= OnMiss;
        currentStage?.EndCurrentStage();
        currentStage?.Hide();
    }
    public void StartStage(int stageIndex) 
    {
        m_currentStage = stageIndex;
        StartCurrentStage();
    }
    public void PreviousStage() 
    {
        if (m_currentStage - 1 >= 0)
        {
            StopCurrentStage();
            m_currentStage--;
            UpdateStageNumber();
            StartCurrentStage();
        }
    }
    public void NextStage() 
    {
        if (m_currentStage + 1 < m_stages.Count)
        {
            StopCurrentStage();
            m_currentStage++;
            UpdateStageNumber();
            StartCurrentStage();
        }

    }
    void OnMiss(BypassStage stage) 
    {
        m_director.Play(m_onMiss);
    }
    void OnStageClear(BypassStage stage) 
    {
        BypassStage currentStage = m_stages[m_currentStage];
        currentStage.OnClear -= OnStageClear;
        currentStage.OnNodeMiss -= OnMiss;
        m_director.Play(m_onCorrect);
       

    }
}
