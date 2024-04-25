using UnityEngine;

public class TutorialManager : MonoBehaviour 
{
    [SerializeField] GameStateManager m_gameState = default;
    [SerializeField] TutorialCollection m_uiNavigation = default;
    [SerializeField] TutorialCollection m_tools = default;
    [SerializeField] TutorialCollection m_newGame = default;
    TutorialCollection m_current;
    public void Navigation() 
    {
        m_uiNavigation?.Begin();
        m_current = m_uiNavigation;
    }
    public void Tools() 
    {
        m_tools?.Begin();
        m_current = m_tools;

    }
    public void NewGame() 
    {
        m_newGame?.Begin();
        m_current = m_newGame;
    }
    public void NextStep() 
    {
        m_current?.NextTutorialStep();
    }

}
