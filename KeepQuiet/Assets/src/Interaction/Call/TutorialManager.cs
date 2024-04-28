using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour 
{
    [SerializeField] Image m_background = default;
    [SerializeField] GameStateManager m_gameState = default;
    [SerializeField] TutorialCollection m_uiNavigation = default;
    [SerializeField] TutorialCollection m_tools = default;
    [SerializeField] TutorialCollection m_newGame = default;
    TutorialCollection m_current;
    Coroutine m_transition;
    public void Navigation()
    {
        m_background.enabled = true;
        EndCurrent();
        m_uiNavigation?.Begin();
        m_current = m_uiNavigation;
    }
    public void Tools()
    {
        m_background.enabled = true;
        EndCurrent();
        m_tools?.Begin();
        m_current = m_tools;
    }
    public void NewGame()
    {
        m_background.enabled = true;
        EndCurrent();
        m_newGame?.Begin();
        m_current = m_newGame;
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
