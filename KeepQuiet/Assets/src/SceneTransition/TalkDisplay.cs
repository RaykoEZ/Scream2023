using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IStepDisplayContent
{
    string DisplayContent { get; }
}
public class TalkDisplay : StepDisplayHandler
{
    [SerializeField] CloseupHandler m_closeupHandle = default;
    [SerializeField] TemporaryInputAction m_nextLine = default;
    protected override IReadOnlyList<IStepDisplayContent> Steps => m_currentStepRef;
    List<DialogueStep> m_currentStepRef;
    public void SetContent(List<DialogueStep> toTalk, bool startTalkngNow = true)
    {
        if (m_displaying != null) return;
        m_currentStepRef = toTalk;
        if (startTalkngNow) 
        {
            Begin();
        }
    }
    public override void Begin()
    {
        if (m_currentStepRef == null || m_displaying != null) return;
        m_current = 0;
        m_closeupHandle?.EnterScene();
        m_displaying = StartCoroutine(ShowCurrent());
        m_nextLine?.Enable();
    }
    public override void End()
    {
        m_current = 0;
        m_closeupHandle?.ExitScene();
        m_nextLine?.Disable();
        m_currentStepRef = null;
    }
    protected override void Display()
    {
        DialogueStep step = m_currentStepRef[m_current];
        // default
        if (step == null)
        {
            StopAllCoroutines();
            Next();
            return;
        }
        m_closeupHandle?.StartTalk(step.DisplayContent, step.Emotion);
    }
    public override bool Next()
    {
        int next = ++m_current;
        //end this tutorial sequence if current index is at the end
        bool hasStepsLeft = next < Steps.Count;
        if (m_displaying != null) return hasStepsLeft;
        // ignore spamming
        if (!hasStepsLeft)
        {
            End();
            return hasStepsLeft;
        }
        // increment sequence
        else
        {
            // transition not needed if we show next step instantly
            m_displaying = StartCoroutine(Next_Internal());
        }
        return hasStepsLeft;
    }
}