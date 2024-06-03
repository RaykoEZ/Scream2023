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
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] List<BypassStage> m_stages = default;
    [SerializeField] TextMeshProUGUI m_stageCount = default;
    [SerializeField] Button m_nextButtonDisplay = default;
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
        SetNextPuzzleButton(currentStage.HasClearedOnce);
        currentStage?.InitStage(reset: true);
    }
    void SetNextPuzzleButton(bool newValue) 
    {
        m_nextButtonDisplay.interactable = newValue;
        if (newValue) 
        {
            m_nextButtonDisplay.animator.SetTrigger("Normal");
        }
        else 
        {
            m_nextButtonDisplay.animator.SetTrigger("Disabled");
        }
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
    void OnMiss(BypassStage stage, bool playFeedback) 
    {

    }
    void OnStageClear(BypassStage stage, bool playFeedback) 
    {
        BypassStage currentStage = m_stages[m_currentStage];
        currentStage.OnClear -= OnStageClear;
        currentStage.OnNodeMiss -= OnMiss;
        if (playFeedback) 
        {
            m_director.Play(m_onCorrect);
        }
        SetNextPuzzleButton(true);
    }
}
