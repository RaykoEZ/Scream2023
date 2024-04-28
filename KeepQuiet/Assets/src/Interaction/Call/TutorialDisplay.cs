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
        StartCoroutine(ShowCurrent());
    }
    public bool Next() 
    {
        //end this tutorial sequence if current index is at the end
        bool hasStepsLeft = m_current + 1 < m_steps.Count;
        // ignore spamming
        if (!hasStepsLeft || m_inProgress) 
        {
            return hasStepsLeft;
        }
        // increment sequence
        else
        {
            ++m_current;
            m_steps[m_current]?.Display?.Hide();
            m_inProgress = true;
            StartCoroutine(Next_Internal());
        }
        return hasStepsLeft;
    }

    public void End() 
    {
        var step = m_steps[m_current];
        step?.Display?.Hide();
        ScreenHighlight?.Hide();
        m_current = 0;
        m_inProgress = false;
    }
    IEnumerator ShowCurrent()
    {
        m_inProgress = true;
        var step = m_steps[m_current];
        step?.Display?.SetContent(step.Content);
        step?.Display?.Show();
        yield return new WaitForSeconds(0.5f);
        m_inProgress = false;
    }
    IEnumerator Next_Internal() 
    {
        yield return new WaitForSeconds(0.2f);
        if(m_current < m_steps.Count) 
        {
            yield return ShowCurrent();
        }
    }
}
