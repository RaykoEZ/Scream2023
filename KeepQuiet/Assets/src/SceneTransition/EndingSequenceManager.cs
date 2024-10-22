using UnityEngine;
// Listens to saved game states and affect game behaviour
public class EndingSequenceManager : MonoBehaviour 
{
    // sequences to trigger
    [SerializeField] SkippableSequencePlayer m_credits = default;
    // post credit
    [SerializeField] RouteSequencePlayer m_ending = default;
    Ending m_currentlyPlaying = Ending.None;
    bool m_playingEnding = false;
    void OnEnable()
    {
        m_credits.OnFinish += OnCreditFinish;
    }
    void OnDisable()
    {
        m_credits.OnFinish -= OnCreditFinish;
    }
    public void PlayRouteSequence(Ending flag) 
    {
        if (m_playingEnding) return;
        m_playingEnding = true;
        m_ending.OnFinish += OnEndingFinish;
        m_currentlyPlaying = flag;
        m_ending.RouteSequence(flag);
    }
    public void PlayCredit()
    {
        m_credits?.PlaySequence();
    }
    void OnEndingFinish()
    {
        m_ending.OnFinish -= OnEndingFinish;
        // Play credit after ending sequence
    }
    void OnCreditFinish() 
    {
        // Post Credit
        m_ending.OnFinish += PostCreditFinish;
        m_ending.RouteSequence(m_currentlyPlaying);
    }
    void PostCreditFinish()
    {
        m_ending.OnFinish -= PostCreditFinish;
        m_playingEnding = false;
    }
}
