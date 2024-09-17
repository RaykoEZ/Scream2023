using Curry.Events;
using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// handles screen animation and text boxes in a tutorial sequence
public class GuideDisplay : StepDisplayHandler
{
    [SerializeField] protected HideableUI ScreenHighlight;
    [SerializeField] protected List<Dialogue> m_toDisplay = default;
    [SerializeField] protected DialogueBox m_display = default;
    protected override IReadOnlyList<IStepDisplayContent> Steps => m_toDisplay;
    public override void Begin()
    {
        if (m_displaying != null) return;
        m_current = 0;
        ScreenHighlight?.Show();
        m_displaying = StartCoroutine(ShowCurrent());
    }
    public override void End()
    {
        m_display?.Hide();
        ScreenHighlight?.Hide();
        m_current = 0;
        m_hasTriggeredOnce = true;
        m_isActive = false;
    }
    public void AppendStep(List<Dialogue> toAdd) 
    {
        m_toDisplay.AddRange(toAdd);
    }
    // overwrite all steps starting from the current step index
    public void ReplaceStep(List<Dialogue> toReplace) 
    {
        // replace all
        if (m_current == 0) 
        {
            m_toDisplay = toReplace;
        }
        else 
        {
            // remove and replace all steps starting from current step
            m_toDisplay.RemoveRange(m_current, m_toDisplay.Count - m_current);
            m_toDisplay.AddRange(toReplace);
        }
    }
    protected override void Display()
    {
        var step = m_toDisplay[m_current];
        // default
        if(step == null) 
        {
            StopAllCoroutines();
            Next();
            return;
        }
        if (step.PlaySound != null)
        {
            m_display?.Audio?.PlayOneShot(step.PlaySound);
        }
        m_display?.SetContent(step.DisplayContent);
        m_display?.Show(step.ShowInstantly, step.Emotion);
    }
    protected override void OnStepFinish()
    {
        m_toDisplay[m_current]?.TriggerAfterThisLine?.Trigger();
    }
    public override bool Next() 
    {
        int next = ++m_current;
        //end this tutorial sequence if current index is at the end
        bool hasStepsLeft = next < m_toDisplay.Count;
        // ignore spamming
        if (!hasStepsLeft || m_displaying != null) 
        {
            return hasStepsLeft;
        }
        // increment sequence
        var nextStep = m_toDisplay[next];
        // transition not needed if we show next step instantly
        if (!nextStep.ShowInstantly)
        {
            m_display?.Hide();
        }
        m_displaying = StartCoroutine(Next_Internal());
        
        return hasStepsLeft;
    }
}
