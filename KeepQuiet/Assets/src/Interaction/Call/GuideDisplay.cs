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
    bool m_inProgress = false;
    protected bool isActive = true;
    protected bool m_hasTriggeredOnce = false;
    public bool IsActive { get => isActive; private set => isActive = value; }
    public bool HasTriggeredOnce { get => m_hasTriggeredOnce; }
    public bool BlockBackground { get => m_blockBackground; }
    public GuideDisplay NextDisplay { get => m_nextDisplay; }

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
        int next = ++m_current;
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
            // transition not needed if we show next step instantly
            if (!nextStep.ShowInstantly)
            {
                m_display?.Hide();
            }
            m_inProgress = true;
            StartCoroutine(Next_Internal());
        }
        return hasStepsLeft;
    }
    public void End() 
    {
        m_display?.Hide();
        ScreenHighlight?.Hide();
        m_current = 0;
        m_hasTriggeredOnce = true;
        m_inProgress = false;
        IsActive = false;
    }
    IEnumerator ShowCurrent()
    {
        m_inProgress = true;
        var step = m_steps[m_current];
        if (step.PlaySound != null)
        {
            m_display?.Audio?.PlayOneShot(step.PlaySound);
        }
        m_display?.SetContent(step.Content);
        m_display?.Show(step.ShowInstantly, step.Angry);
        step?.OnShow?.TriggerEvent();
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
    public virtual void Refresh(SaveData save)
    {
        IsActive = true;
    }
}
