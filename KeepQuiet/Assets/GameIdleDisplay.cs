using System.Collections;
using UnityEngine;
// Turns paused display on/off whenb game is minimized/returns to focus
public class GameIdleDisplay : MonoBehaviour 
{
    [SerializeField] float m_secondsBeforeIdle = default;
    [SerializeField] CanvasGroup m_idlePanel = default;
    [SerializeField] InputSequenceMatch m_hiddenInputSequence = default;
    bool m_focus = true;
    Coroutine m_showIdleInProgress;
    void Start()
    {
        IdleOff();
    }
    void OnApplicationFocus(bool focus)
    {
        // enable idle display when game is off focus
        if (!focus) 
        {
            m_showIdleInProgress = StartCoroutine(IdleTimer());
        }
        // cancel idle timer
        else if(focus && m_showIdleInProgress != null) 
        {
            StopCoroutine(m_showIdleInProgress);
            m_showIdleInProgress = null;
            m_focus = focus; 
        }
    }
    IEnumerator IdleTimer() 
    {
        yield return new WaitForSeconds(m_secondsBeforeIdle);
        m_hiddenInputSequence.Enable();
        m_focus = false;
        SetDisplay();
        m_showIdleInProgress = null;
    }
    void SetDisplay() 
    {
        m_idlePanel.alpha = m_focus ? 0f : 1f;
        m_idlePanel.interactable = !m_focus;
        m_idlePanel.blocksRaycasts = !m_focus;
    }
    // Unhides idle UI
    public void IdleOff()
    {
        m_focus = true;
        m_hiddenInputSequence.Disable();
        SetDisplay();
    }
}
