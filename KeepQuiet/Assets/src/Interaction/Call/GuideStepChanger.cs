using Curry.Events;
using System.Collections.Generic;
using UnityEngine;
// To add/replace tutorial dialogues on condition
public class GuideStepChanger : MonoBehaviour
{
    [SerializeField] TutorialManager m_manager = default;
    // the dialogues to add/replace the orginal tutorial steps
    [SerializeField] List<GuideStep> m_extraSteps = default;
    [SerializeField] protected GuideDisplay m_display = default;
    [SerializeField] CurryGameEventListener m_triggerListener = default;
    DialogueInfo m_currentDialogue;
    public void DialogueTrigger(EventInfo info)
    {
        if (info == null || m_currentDialogue != null) return;
        if (info is DialogueInfo dialogue)
        {
            m_currentDialogue = dialogue;
        }
    }
    void SetupDialogue()
    {

    }
    public void Replace() 
    {
        m_display.ReplaceStep(m_extraSteps);
    }
    public void Append() 
    {
        m_display.AppendStep(m_extraSteps);
    }
}
