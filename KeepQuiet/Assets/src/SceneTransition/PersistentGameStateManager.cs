using Curry.Events;
using UnityEngine;
// Reads saved game states and affect game behaviour
public class PersistentGameStateManager : MonoBehaviour 
{
    [SerializeField] GameStateFileHandler m_file = default;
    [SerializeField] EndingHandler m_ending = default;

    [SerializeField] CurryGameEventListener m_onTitleLoad = default;
    [SerializeField] CurryGameEventListener m_onNewGame = default;
    [SerializeField] CurryGameEventListener m_onContinue = default;
    [SerializeField] CurryGameEventListener m_onDialSecret = default;
    [SerializeField] CurryGameEventListener m_onJamOut = default;
    [SerializeField] CurryGameEventListener m_onToolIn = default;

    public void OnTitleLoaded() 
    { 
        
    }

    public void OnNewGame() 
    { 
    
    }
    public void OnContinue() 
    { 
    
    }

    public void OnDialSecretReveal() 
    { 
    
    }
    public void OnJamDragOut() 
    { 
    
    }

    public void OnToolDragIn() 
    { 
    
    }
}
