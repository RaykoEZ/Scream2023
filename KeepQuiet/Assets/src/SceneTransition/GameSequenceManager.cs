using Curry.Events;
using System.Collections;
using UnityEngine;
// Listens to saved game states and affect game behaviour
public class GameSequenceManager : MonoBehaviour 
{
    [SerializeField] GameStateFileHandler m_file = default;
    [SerializeField] LevelLoader m_level = default;
    // sequences to trigger
    [SerializeField] SequencePlayer m_intro = default;
    [SerializeField] SkippableSequencePlayer m_credits = default;
    // post credit
    [SerializeField] EndingPlayer m_ending = default;

    [SerializeField] CurryGameEventListener m_onNewGame = default;
    [SerializeField] CurryGameEventListener m_onContinue = default;
    [SerializeField] CurryGameEventListener m_onDialSecret = default;
    void OnEnable()
    {
        m_credits.OnFinish += OnCreditFinish;
        m_onNewGame?.Init();
        m_onContinue?.Init();
    }
    void OnDisable()
    {
        m_credits.OnFinish -= OnCreditFinish;
        m_onNewGame?.Shutdown();
        m_onContinue?.Shutdown();
    }
    public void PlayIntro()
    {
        m_intro?.PlaySequence();
    }
    public void OnIntroFinish() 
    {
        // playa special sequence if save data has flag
        m_level?.LoadScene(1);
    }
    void OnCreditFinish() 
    {
        // Determine a post credit sequence for ending

    }
    void OnEndingFinish() 
    { 
    
    }
    void PlayCredit() 
    {
        //play credit
        m_credits?.PlaySequence();
    }

    public void OnNewGame() 
    { 
        // if player killed Aria on the previous load
        // (leading to a game crash), new game soft locks into bloody scene
        // need to clear cache reset

        // aria possessed, disable new game
        // glitch + scary when choosing & spamming new game 

    }
    public void OnContinue() 
    { 
        // if player killed Aria on the previous load, soft lock as well

        // Continue increment
    }
}
