using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour 
{
    [SerializeField] HideableUI ScreenHighlight;
    [SerializeField] List<TutorialStep> m_steps = default;
    int m_current = 0;
    bool m_inProgress = false;
    public void Begin() 
    {
        m_current = 0;
        ScreenHighlight?.Show();
        ShowCurrent();
    }
    public void Next() 
    {
        if (m_inProgress) 
        {
            return;
        }
        m_steps[m_current]?.Display?.Hide();
        ++m_current;
        if (m_current >= m_steps.Count) 
        {
            End();
        }
        m_inProgress = true;
        StartCoroutine(Next_Internal());
    }

    public void End() 
    {
        var step = m_steps[m_current - 1];
        step?.Display?.Hide();
        ScreenHighlight?.Hide();
        m_inProgress = false;
    }
    void ShowCurrent() 
    {
        var step = m_steps[m_current];
        step?.Display?.SetContent(step.Content);
        step?.Display?.Show();
        m_inProgress = false;
    }
    IEnumerator Next_Internal() 
    {

        yield return new WaitForSeconds(0.2f);
        if(m_current < m_steps.Count) 
        {
            ShowCurrent();
        } 
    }
}
