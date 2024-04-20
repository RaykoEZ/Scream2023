using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// handles screen animation and text boxes in a tutorial sequence
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
    public bool Next() 
    {
        bool ended = false;
        //end this tutorial sequence
        if(m_current >= m_steps.Count) 
        {
            ended = true;
        }
        // ignore spamming
        else if (m_inProgress) 
        {
            return ended;
        }
        // increment sequence
        else if (++m_current < m_steps.Count) 
        {
            m_steps[m_current]?.Display?.Hide();
            m_inProgress = true;
            StartCoroutine(Next_Internal());
        }
        return ended;
    }

    public void End() 
    {
        var step = m_steps[m_current - 1];
        step?.Display?.Hide();
        ScreenHighlight?.Hide();
        m_current = 0;
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
