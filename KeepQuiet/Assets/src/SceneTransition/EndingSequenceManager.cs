using Curry.Events;
using System.Collections;
using UnityEngine;
// Listens to saved game states and affect game behaviour
public class EndingSequenceManager : MonoBehaviour 
{
    // sequences to trigger
    [SerializeField] SkippableSequencePlayer m_credits = default;
    // post credit
    [SerializeField] EndingPlayer m_ending = default;
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
        // Determine a post credit sequence for ending
    }
    public void PlayCredit() 
    {
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
