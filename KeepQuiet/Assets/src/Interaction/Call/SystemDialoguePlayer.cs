using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SystemDialoguePlayer : MonoBehaviour 
{
    [SerializeField] Image m_background = default;
    [SerializeField] InputActionReference m_nextStep = default;
    GuideDisplay m_current;
    Coroutine m_transition;
    public virtual void TriggerTutorial(GuideDisplay col)
    {
        StartTutorial(col);
    }
    public virtual void TriggerTutorial(GuideDisplay col, bool forceRepeat = false) 
    {
        StartTutorial(col, forceRepeat);
    }
    protected virtual void StartTutorial(GuideDisplay col, bool forceRepeat = false) 
    {
        // Don't repeat the same tutorial in the same session if we don't need to
        if ((!col.IsActive || col.HasTriggeredOnce) && !forceRepeat) return;
        m_background.enabled = col.BlockBackground;
        EndCurrent();
        m_current = col;
        m_current?.Begin();
        m_nextStep.action.performed += NextStep;
    }
    void EndCurrent()
    {
        m_nextStep.action.performed -= NextStep;
        m_current?.End();
        StopAllCoroutines();
        m_transition = null;
        m_background.enabled = false;
    }
    public void NextStep()
    {
        if (m_transition != null) return;
        m_transition = StartCoroutine(Next_Internal());
    }
    public void NextStep(InputAction.CallbackContext c) 
    {
        NextStep();
    }
    IEnumerator Next_Internal() 
    {
        yield return new WaitForEndOfFrame();
        // End current tutorial if we finished all dialogues
        if (m_current.Next())
        {
            m_transition = null;
        }
        else if(m_current.NextDisplay != null)
        {
            StartTutorial(m_current.NextDisplay);
        }
        else 
        {
            EndCurrent();
        }
    }
}
