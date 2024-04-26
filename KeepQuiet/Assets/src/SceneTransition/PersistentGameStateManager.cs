using Curry.Events;
using UnityEngine;
// Reads saved game states and affect game behaviour
public class PersistentGameStateManager : MonoBehaviour 
{
    [SerializeField] GameStateFileHandler m_file = default;
    [SerializeField] EndingHandler m_ending = default;
    [SerializeField] CreditsSequencePlayer m_credits = default;
    [SerializeField] CurryGameEventListener m_onTitleLoad = default;
    [SerializeField] CurryGameEventListener m_onNewGame = default;
    [SerializeField] CurryGameEventListener m_onContinue = default;
    [SerializeField] CurryGameEventListener m_onDialSecret = default;
    [SerializeField] CurryGameEventListener m_onJamOut = default;
    [SerializeField] CurryGameEventListener m_onToolIn = default;
    void OnEnable()
    {
        m_credits.OnFinish += OnCreditFinish;
    }
    void OnDisable()
    {
        m_credits.OnFinish -= OnCreditFinish;
    }
    void OnCreditFinish() 
    {
        // Determine a game ending through game states

    }
    void OnGameEnd() 
    {
        //play credit

    }
    // Change title screen behaviour depending on game state
    public void OnTitleLoaded() 
    { 
        // If play killed Aria, show creepier title atmosphere, no more Aria
        
        // If Aria is possessed, glitch + disable clear cache & new game,
        // add random scares

        // if true end reached, No Aria, no raining, slight rain drip, calmer music
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

    public void OnDialSecretReveal() 
    { 
    
    }
    public void OnJamRemove() 
    { 
    
    }
    public void OnMalwareIsolated() 
    { 
    
    }
    public void OnKillAria() 
    { 
    
    }
    public void HandshakeComplete() 
    { 
    
    }
    public void SecretKeyInput() 
    { 
    
    }
    public void OnClockSecretReveal() 
    { 
    
    }
    public void OnToolDragIn() 
    { 
    
    }
}
