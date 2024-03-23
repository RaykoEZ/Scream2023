using System.Collections.Generic;
using UnityEngine;
// A class to handle multiple stages of spot-the-odd-one mini-game 
public class BypassManager : MonoBehaviour 
{
    [SerializeField] List<BypassStage> m_stages = default;
    int m_currentStage = 0;
    public void Init(int currentStage)
    {
        m_currentStage = currentStage;
    }
    public void StartCurrentStage() 
    { 
        if (m_currentStage < 0 && m_currentStage >= m_stages.Count) 
        {
            m_currentStage = 0;
        }
        m_stages[m_currentStage]?.InitStage();
    }
    public void StartStage(int stageIndex) 
    {
        m_currentStage = stageIndex;
        StartCurrentStage();
    }
    public void NextStage(bool showStageNow = false) 
    {
        m_currentStage++;
        if (showStageNow) 
        {
            StartCurrentStage();
        }
    }
}
