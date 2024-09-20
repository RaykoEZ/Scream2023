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
    DialogueNode m_currentDialogueRef;
    public event OnOptionPrompt OnPrompt;
    protected override IReadOnlyList<IStepDisplayContent> Steps => m_currentDialogueRef.Dialogues;
    public DialogueNode CurrentDialogueRef { get => m_currentDialogueRef; set => m_currentDialogueRef = value; }
    public override void Begin()
    {
        if (m_currentDialogueRef == null || m_displaying != null) return;
        m_current = 0;
        m_closeupHandle?.EnterScene();
        m_displaying = StartCoroutine(ShowCurrent());
    }
    public void ResetConversation() 
    {
        m_current = 0;
        m_currentDialogueRef = null;
        StopAllCoroutines();
        m_displaying = null;
    }
    public override void End()
    {
        ResetConversation();
        m_closeupHandle?.ExitScene();
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
    protected override IEnumerator Next_Internal()
    {
        yield return new WaitForSeconds(0.1f);
        if (m_current < Steps.Count)
        {
            yield return ShowCurrent();
        }
        // if we reached the end and have options
        else if (CurrentDialogueRef.Options.Count > 0) 
        {
            OnPrompt?.Invoke(CurrentDialogueRef.Options as List<ChatOption>);
        }
        m_displaying = null;
    }
}