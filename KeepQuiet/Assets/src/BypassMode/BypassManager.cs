using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
// A class to handle multiple stages of spot-the-odd-one mini-game 
public class BypassManager : MonoBehaviour 
{
    [SerializeField] PlayableAsset m_onClear = default;
    [SerializeField] PlayableAsset m_onMiss = default;
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] List<BypassStage> m_stages = default;
    int m_currentStage = 0;
    public void Start()
    {
        StartCurrentStage();
    }
    public void Init(int currentStage)
    {
        m_currentStage = currentStage;
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
        currentStage?.InitStage();
    }
    public void StartStage(int stageIndex) 
    {
        m_currentStage = stageIndex;
        StartCurrentStage();
    }
    public void NextStage(bool showStageNow = false) 
    {
        m_currentStage++;
        if (m_currentStage >= m_stages.Count) 
        {
            m_director.Play(m_onClear);
        }
        else if (showStageNow) 
        {
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
        m_director.Play(m_onClear);
        NextStage(true);
    }
}
