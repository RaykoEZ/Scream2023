using Curry.Events;
using System;
using UnityEngine;
// Persistent event listener to handle scene changes
[RequireComponent(typeof(LevelLoader))]
public class LevelEventHandler : MonoBehaviour 
{
    [SerializeField] GameStateFileHandler m_files = default;
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
        m_files?.SetNewGame(false);
        // Load persistent and start game
        GoToGameScene();
    }
    public void NewGame(EventInfo info) 
    {
        m_files?.SetNewGame(true);
        // Start game from the start, may keep some persistent
        GoToGameScene();
    }
    public void ReturnToTitle(EventInfo info) 
    {
        GoToTitle();
    }
    void GoToGameScene() 
    {
        Loader?.LoadScene(2);
    }
    void GoToTitle() 
    {
        Loader?.LoadScene(1);
    }
}
