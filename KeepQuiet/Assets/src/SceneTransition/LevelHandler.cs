using Curry.Events;
using UnityEngine;
// Persistent event listener to handle scene changes
[RequireComponent(typeof(LevelLoader))]
public class LevelHandler : MonoBehaviour 
{
    [SerializeField] CurryGameEventListener m_newGame = default;
    [SerializeField] CurryGameEventListener m_continueGame = default;
    [SerializeField] CurryGameEventListener m_returnToTitle = default;
    LevelLoader Loader => GetComponent<LevelLoader>();
    private void Start()
    {
        m_newGame?.Init();
        m_continueGame?.Init();
        m_returnToTitle?.Init();
    }
    public void ContinueGame(EventInfo info) 
    {
        // Load persistent and start game
        GoToGameScene();
    }
    public void NewGame(EventInfo info) 
    {
        // Start game from the start, may keep some persistent
        GoToGameScene();
    }
    public void ReturnToTitle(EventInfo info) 
    {
        // Save persistent and go to title
        GoToTitle();
    }
    void GoToGameScene() 
    {
        Loader?.LoadScene(1);
    }
    void GoToTitle() 
    {
        Loader?.LoadScene(0);
    }
}
