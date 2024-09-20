using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SystemDialoguePlayer : MonoBehaviour 
{
    [SerializeField] protected bool m_defaultRepeat = default;
    [SerializeField] protected Image m_background = default;
    [SerializeField] protected TemporaryInputAction m_next = default;
    protected StepDisplayHandler m_current;
    protected Coroutine m_displayCall;
    public virtual void TriggerDialogue(StepDisplayHandler col)
    {
        if (m_displayCall != null) return;
        StartDialogue(col, m_defaultRepeat);
    }
    public virtual void TriggerDialogue(StepDisplayHandler col, bool forceRepeat = false) 
    {
        if (m_displayCall != null) return;
        StartDialogue(col, forceRepeat);
    }
    protected virtual void StartDialogue(StepDisplayHandler col, bool forceRepeat = false) 
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
    protected virtual void EndCurrent()
    {
        m_next?.Disable();
        m_current?.End();
        StopAllCoroutines();
        m_background.enabled = false;
    }
    public virtual void NextStep()
    {
        if (m_displayCall != null) return;
        m_displayCall = StartCoroutine(Next_Internal());
    }
    protected virtual IEnumerator Next_Internal() 
    {
        yield return new WaitForEndOfFrame();
        // Display next step
        bool stepsLeft = m_current.Next();
        // Finish here if we still have next step 
        if (stepsLeft) 
        {
            m_displayCall = null;
            yield break;
        }
        // Finish up
        EndCurrent();
        // End current tutorial if we finished all dialogues
        if (!stepsLeft && m_current.NextDisplay != null)
        {
            m_displayCall = null;
            StartDialogue(m_current.NextDisplay, true);
        }
    }
}
