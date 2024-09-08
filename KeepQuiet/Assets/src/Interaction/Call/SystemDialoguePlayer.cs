using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class SystemDialoguePlayer : MonoBehaviour 
{
    [SerializeField] Image m_background = default;
    [SerializeField] TemporaryInputAction m_next = default;
    GuideDisplay m_current;
    Coroutine m_displayCall;
    public virtual void TriggerTutorial(GuideDisplay col)
    {
        if (m_displayCall != null) return;
        StartTutorial(col);
    }
    public virtual void TriggerTutorial(GuideDisplay col, bool forceRepeat = false) 
    {
        if (m_displayCall != null) return;
        StartTutorial(col, forceRepeat);
    }
    protected virtual void StartTutorial(GuideDisplay col, bool forceRepeat = false) 
    {
        // Don't repeat the same tutorial in the same session if we don't need to
        if (m_displayCall != null) return;
        if ((!col.IsActive || col.HasTriggeredOnce) && !forceRepeat) return;
        EndCurrent();
        m_background.enabled = col.BlockBackground;
        m_current = col;
        m_current?.Begin();
        m_next?.Enable();
    }
    void EndCurrent()
    {
        m_next?.Disable();
        m_current?.End();
        StopAllCoroutines();
        m_background.enabled = false;
    }
    public void NextStep()
    {
        if (m_displayCall != null) return;
        m_displayCall = StartCoroutine(Next_Internal());
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
            m_displayCall = null;
            yield break;
        }
        EndCurrent();
        // End current tutorial if we finished all dialogues
        if (!stepsLeft && m_current.NextDisplay != null)
        {
            m_displayCall = null;
            StartTutorial(m_current.NextDisplay);
        }
    }
}
