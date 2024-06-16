using System.Collections.Generic;
using UnityEngine;
// To add/replace tutorial dialogues on condition
public class GuideStepChanger : MonoBehaviour
{
    // the dialogues to add/replace the orginal tutorial steps
    [SerializeField] List<GuideStep> m_extraSteps = default;
    [SerializeField] protected GuideDisplay m_display = default;
    DialogueInfo m_currentDialogue;

    public void Replace() 
    {
        m_display.ReplaceStep(m_extraSteps);
    }
    public void Append() 
    {
        m_display.AppendStep(m_extraSteps);
    }
}
