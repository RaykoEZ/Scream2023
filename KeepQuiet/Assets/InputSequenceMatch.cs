using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSequenceMatch : MonoBehaviour
{
    [SerializeField] string m_sequenceToMatch = default;
    [SerializeField] float m_secondsBeforeTimeout = default;
    [SerializeField] UnityEvent m_onSequenceMatched = default;
    bool m_sequenceMatched = false;
    bool m_checkInProgress = false;
    float m_timer = 0f;
    string m_currentSequence = "";
    public bool SequenceMatched { get => m_sequenceMatched; set => m_sequenceMatched = value; }
    bool TimeOut => m_timer > m_secondsBeforeTimeout;
    void Update()
    {
        if (m_checkInProgress) 
        {
            m_timer += Time.deltaTime;
        }
        if (TimeOut && m_checkInProgress)
        {
            ResetCheckState();
        }
    }
    public void Enable()
    {
        m_checkInProgress = true;
        ResetCheckState();
        Keyboard.current.onTextInput += OnTextInput;
    }
    public void Disable()
    {
        Keyboard.current.onTextInput -= OnTextInput;
        ResetCheckState();
        m_checkInProgress = false;
    }
    void ResetCheckState()
    {
        m_timer = 0f;
        m_sequenceMatched = false;
        m_currentSequence = "";
    }
    void OnTextInput(char newChar)
    {
        if (!m_checkInProgress)
        {
            return;
        }
        // reset input timer
        m_timer = 0f;
        // append input
        m_currentSequence += newChar;
        m_sequenceMatched = Match();
        if (m_sequenceMatched) 
        {
            StartCoroutine(OnInputMatched());
        }
    }
    IEnumerator OnInputMatched() 
    {
        yield return new WaitForSeconds(0.5f);
        // Wait for some time before triggering the event
        m_sequenceMatched = Match();
        if (m_sequenceMatched) 
        {
            m_onSequenceMatched?.Invoke();
        }
        else 
        {
            // reset if we added something incorrect
            ResetCheckState();
        }
    }
    bool Match() 
    {
        return string.Equals(m_currentSequence, m_sequenceToMatch);
    }
}
