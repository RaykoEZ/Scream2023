using System.Collections.Generic;
using UnityEngine;
public interface IStepDisplayContent
{
    string DisplayContent { get; }
}
public class TalkDisplay : StepDisplayHandler
{
    [SerializeField] CloseupHandler m_closeupHandle = default;
    protected override IReadOnlyList<IStepDisplayContent> Steps => m_currentDialogueRef.Dialogues;
    DialogueNode m_currentDialogueRef;
    public void Init(DialogueNode toTalk)
    {
        if (m_displaying != null) return;
        m_currentDialogueRef = toTalk;
        Begin();
    }
    public override void Begin()
    {
        if (m_currentDialogueRef == null || m_displaying != null) return;
        m_current = 0;
        m_closeupHandle?.EnterScene();
        m_displaying = StartCoroutine(ShowCurrent());
    }
    public override void End()
    {
        m_current = 0;
        m_closeupHandle?.ExitScene();
        m_currentDialogueRef = null;
    }
    protected override void Display()
    {
        Dialogue step = m_currentDialogueRef.Dialogues[m_current];
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