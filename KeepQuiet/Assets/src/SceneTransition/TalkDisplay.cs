using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public interface IStepDisplayContent
{
    string DisplayContent { get; }
}
public class TalkDisplay : StepDisplayHandler
{
    [SerializeField] CloseupHandler m_closeupHandle = default;
    [SerializeField] TextMeshProUGUI m_nextButtonLabel = default;
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
        // If current step is the last step
        m_nextButtonLabel.text = m_current < Steps.Count - 1 || 
            CurrentDialogueRef.Options.Count > 0? ">>" : "END";
        m_closeupHandle?.StartTalk(step.DisplayContent, step.Emotion);
    }
    public override bool Next()
    {
        int next = ++m_current;
        //end this tutorial sequence if current index is at the end
        bool hasStepsLeft = next < Steps.Count;
        if (m_displaying != null) return hasStepsLeft;
        // ignore spamming/at the end of the conversation
        if (!hasStepsLeft && CurrentDialogueRef.Options.Count == 0)
        {
            return hasStepsLeft;
        }        // if we reached the end and have options
        else if (!hasStepsLeft && CurrentDialogueRef.Options.Count > 0)
        {
            // Display options prompt and continue current node
            OnPrompt?.Invoke(CurrentDialogueRef.Options as List<ChatOption>);
            return true;
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