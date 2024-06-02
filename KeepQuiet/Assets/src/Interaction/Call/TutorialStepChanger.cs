using System.Collections.Generic;
using UnityEngine;
// To add/replace tutorial dialogues on condition
public class TutorialStepChanger : MonoBehaviour
{
    // the dialogues to add/replace the orginal tutorial steps
    [SerializeField] protected List<TutorialStep> m_extraSteps = default;
    [SerializeField] TutorialDisplay m_display = default;
    public void Replace() 
    {
        m_display.ReplaceStep(m_extraSteps);
    }
    public void Append() 
    {
        m_display.AppendStep(m_extraSteps);
    }
}
