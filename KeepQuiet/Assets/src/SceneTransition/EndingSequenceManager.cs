using Curry.Events;
using System.Collections;
using UnityEngine;
// Listens to saved game states and affect game behaviour
public class EndingSequenceManager : MonoBehaviour 
{
    [SerializeField] LevelEventHandler m_level = default;
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
    public void CloseCaseSequence() 
    {
        m_ending.SetEnding(Ending.Normal_CaseClosed);
        m_ending.PlaySequence();
    }
    public void BadEndSequence()
    {
        m_ending.SetEnding(Ending.Bad_Delusion);
        m_ending.PlaySequence();
    }
    public void FreedomEndSequence()
    {
        m_ending.SetEnding(Ending.Secret_Freedom);
        m_ending.PlaySequence();
    }
    void OnCreditFinish() 
    {
        // Determine a post credit sequence for ending
        m_credits.OnFinish -= OnCreditFinish;
        m_level?.ReturnToTitle();
    }
    public void PlayCredit() 
    {
        m_credits.OnFinish += OnCreditFinish;
        m_credits?.PlaySequence();
    }
}
