using Curry.Events;
using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// handles screen animation and text boxes in a tutorial sequence
public class GuideDisplay : MonoBehaviour
{
    [SerializeField] protected bool m_blockBackground = default;
    [SerializeField] protected HideableUI ScreenHighlight;
    [SerializeField] protected List<GuideStep> m_steps = default;
    [SerializeField] GuideDisplay m_nextDisplay = default;
    [SerializeField] protected DialogueBox m_display = default;
    int m_current = 0;
    protected bool isActive = true;
    protected bool m_hasTriggeredOnce = false;
    public bool IsActive { get => isActive; private set => isActive = value; }
    public bool HasTriggeredOnce { get => m_hasTriggeredOnce; }
    public bool BlockBackground { get => m_blockBackground; }
    public GuideDisplay NextDisplay { get => m_nextDisplay; }
    Coroutine m_displayCall;
    public void Begin()
    {
        if (m_displayCall != null) return;
        m_current = 0;
        ScreenHighlight?.Show();
        m_displayCall = StartCoroutine(ShowCurrent());
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
        int next = ++m_current;
        //end this tutorial sequence if current index is at the end
        bool hasStepsLeft = next < m_steps.Count;
        // ignore spamming
        if (!hasStepsLeft || m_displayCall != null) 
        {
            return hasStepsLeft;
        }
        // increment sequence
        else
        {
            var nextStep = m_steps[next];
            // transition not needed if we show next step instantly
            if (!nextStep.ShowInstantly)
            {
                m_display?.Hide();
            }
            m_displayCall = StartCoroutine(Next_Internal());
        }
        return hasStepsLeft;
    }
    public void End() 
    {
        m_display?.Hide();
        ScreenHighlight?.Hide();
        m_current = 0;
        m_hasTriggeredOnce = true;
        IsActive = false;
    }
    IEnumerator ShowCurrent()
    {
        var step = m_steps[m_current];
        if (step.PlaySound != null)
        {
            m_display?.Audio?.PlayOneShot(step.PlaySound);
        }
        m_display?.SetContent(step.Content);
        m_display?.Show(step.ShowInstantly, step.Angry);
        step?.OnShow?.TriggerEvent();
        yield return new WaitForSeconds(step.Content.Length * 0.05f);     
        m_displayCall = null;
    }
    IEnumerator Next_Internal() 
    {
        yield return new WaitForSeconds(0.1f);
        if (m_current < m_steps.Count) 
        {
            yield return ShowCurrent();
        }
        m_displayCall = null;
    }
}
