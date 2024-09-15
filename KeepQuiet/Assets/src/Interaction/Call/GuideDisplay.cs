using Curry.Events;
using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// handles screen animation and text boxes in a tutorial sequence
public class GuideDisplay : StepDisplayHandler
{
    [SerializeField] protected bool m_blockBackground = default;
    [SerializeField] protected HideableUI ScreenHighlight;
    [SerializeField] protected List<DialogueStep> m_steps = default;
    [SerializeField] GuideDisplay m_nextDisplay = default;
    [SerializeField] protected DialogueBox m_display = default;
    protected bool isActive = true;
    protected bool m_hasTriggeredOnce = false;
    public bool IsActive { get => isActive; private set => isActive = value; }
    public bool HasTriggeredOnce { get => m_hasTriggeredOnce; }
    public bool BlockBackground { get => m_blockBackground; }
    public GuideDisplay NextDisplay { get => m_nextDisplay; }
    protected override IReadOnlyList<IStepDisplayContent> Steps => m_steps;
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
        IsActive = false;
    }
    public void AppendStep(List<DialogueStep> toAdd) 
    {
        m_steps.AddRange(toAdd);
    }
    // overwrite all steps starting from the current step index
    public void ReplaceStep(List<DialogueStep> toReplace) 
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
    protected override void Display()
    {
        var step = m_steps[m_current];
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
        m_steps[m_current]?.OnShowTrigger?.TriggerEvent();
    }
    public override bool Next() 
    {
        int next = ++m_current;
        //end this tutorial sequence if current index is at the end
        bool hasStepsLeft = next < m_steps.Count;
        // ignore spamming
        if (!hasStepsLeft || m_displaying != null) 
        {
            return hasStepsLeft;
        }
        // increment sequence
        var nextStep = m_steps[next];
        // transition not needed if we show next step instantly
        if (!nextStep.ShowInstantly)
        {
            m_display?.Hide();
        }
        m_displaying = StartCoroutine(Next_Internal());
        
        return hasStepsLeft;
    }
}
