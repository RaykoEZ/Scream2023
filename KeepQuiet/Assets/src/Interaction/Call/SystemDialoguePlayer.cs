using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class SystemDialoguePlayer : MonoBehaviour 
{
    [SerializeField] Image m_background = default;
    [SerializeField] InputActionReference m_nextStep = default;
    bool m_inProgress = false;
    GuideDisplay m_current;
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
        if (m_inProgress) return;
        if ((!col.IsActive || col.HasTriggeredOnce) && !forceRepeat) return;
        EndCurrent();
        m_inProgress = true;
        m_background.enabled = col.BlockBackground;
        m_current = col;
        m_current?.Begin();
        m_nextStep.action.performed += NextStep;
    }
    void EndCurrent()
    {
        m_inProgress = false;
        m_nextStep.action.performed -= NextStep;
        m_current?.End();
        StopAllCoroutines();
        m_background.enabled = false;
    }
    public void NextStep()
    {
        StartCoroutine(Next_Internal());
    }
    public void NextStep(InputAction.CallbackContext c) 
    {
        NextStep();
    }
    IEnumerator Next_Internal() 
    {
        yield return new WaitForEndOfFrame();
        bool stepsLeft = m_current.Next();
        if (stepsLeft) 
        {
            yield break;
        }
        EndCurrent();
        // End current tutorial if we finished all dialogues
        if (!stepsLeft && m_current.NextDisplay != null)
        {
            StartTutorial(m_current.NextDisplay);
        }
    }
}
