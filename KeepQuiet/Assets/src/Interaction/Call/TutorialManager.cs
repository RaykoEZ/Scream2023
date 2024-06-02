using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour 
{
    [SerializeField] Image m_background = default;
    [SerializeField] GameStateManager m_gameState = default;
    TutorialCollection m_current;
    Coroutine m_transition;
    public void TriggerTutorial(TutorialCollection col) 
    {
        StartTutorial(col);
    }
    void StartTutorial(TutorialCollection col, bool forceRepeat = false) 
    {
        // Don't repeat the same tutorial in the same session if we don't need to
        if (col.HasTriggeredOnce && !forceRepeat) return;
        m_background.enabled = col.BlockBackground;
        EndCurrent();
        m_current = col;
        m_current?.Begin();
    }
    void EndCurrent()
    {
        m_current?.EndTutorial();
        StopAllCoroutines();
        m_transition = null;
        m_current = null;
    }
    public void NextStep() 
    {
        if (m_current == null || m_transition != null) return;
        m_transition = StartCoroutine(Next_Internal());       
    }
    IEnumerator Next_Internal() 
    {
        yield return m_current.NextTutorialStep();
        yield return new WaitForEndOfFrame();
        m_transition = null;
        // End current tutorial if we finished all dialogues
        if (!m_current.IsActive) 
        {
            m_current = null;
            m_background.enabled = false;
        }
    }
}
