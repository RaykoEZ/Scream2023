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
        m_ending.OnFinish += OnEndingFinish;
    }
    void OnDisable()
    {
        m_credits.OnFinish -= OnCreditFinish;
        m_ending.OnFinish -= OnEndingFinish;
    }
    public void PlayEnding(Ending flag) 
    {
        m_ending.PlayEnding(flag);
    }
    void OnEndingFinish()
    {
        // Play credit after ending sequence
        PlayCredit();
    }
    void OnCreditFinish() 
    {
        // Return to title after credit
        m_level?.ReturnToTitle();
    }
    public void PlayCredit() 
    {
        m_credits?.PlaySequence();
    }
}
