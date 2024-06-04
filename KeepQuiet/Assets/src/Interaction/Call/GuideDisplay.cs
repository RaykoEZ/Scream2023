using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// handles screen animation and text boxes in a tutorial sequence
public class GuideDisplay : MonoBehaviour 
{
    [SerializeField] protected HideableUI ScreenHighlight;
    [SerializeField] protected List<GuideStep> m_steps = default;
    int m_current = 0;
    bool m_inProgress = false;
    public void Begin()
    {
        m_current = 0;
        ScreenHighlight?.Show();
        StartCoroutine(ShowCurrent());
    }
    public void AppendStep(List<GuideStep> toAdd) 
    {
        m_steps.AddRange(toAdd);
    }
    // overwrite all steps starting from the current step index
    public void ReplaceStep(List<GuideStep> toReplace) 
    {
        // replace all
        if (m_current == 0) 
        {
            m_steps = toReplace;
        }
        else 
        {
            // remove and replace all steps starting from current step
            m_steps.RemoveRange(m_current, m_steps.Count - m_current);
            m_steps.AddRange(toReplace);
        }
    }
    public bool Next() 
    {
        int next = m_current + 1;
        //end this tutorial sequence if current index is at the end
        bool hasStepsLeft = next < m_steps.Count;
        // ignore spamming
        if (!hasStepsLeft || m_inProgress) 
        {
            return hasStepsLeft;
        }
        // increment sequence
        else
        {
            var nextStep = m_steps[next];
            m_steps[m_current]?.Transition(nextStep);
            ++m_current;
            m_inProgress = true;
            StartCoroutine(Next_Internal());
        }
        return hasStepsLeft;
    }
    public void End() 
    {
        var step = m_steps[m_current];
        step?.End();
        ScreenHighlight?.Hide();
        m_current = 0;
        m_inProgress = false;
    }
    IEnumerator ShowCurrent()
    {
        m_inProgress = true;
        var step = m_steps[m_current];
        step?.Show();
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
