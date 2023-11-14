using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using TMPro;
public delegate void OnCallCanceled(string dialed, string extension);
public delegate void OnCallFinish(string dialed, string extension);
public delegate void OnDialed(string dialed);
public class DialHandler : MonoBehaviour 
{
    [SerializeField] int m_characterLimit = default;
    [SerializeField] Animator m_anim = default;
    [SerializeField] TextMeshProUGUI m_display = default;
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] List<DialResult> m_results = default;
    public event OnCallCanceled OnCallCancel;
    public event OnCallFinish OnCallEnd;
    public event OnDialed OnDial;

    Dictionary<string, DialEvent> m_eventSet = new Dictionary<string, DialEvent>();
    string m_confirmedInput = "";
    string m_extensionInput = "";
    bool m_calling = false;
    public bool Calling => m_calling;
    public string ExtensionInput => m_extensionInput;

    private void Start()
    {
        foreach(var result in m_results) 
        {
            m_eventSet.Add(result.Sequence, result.EventToTrigger);
        }
    }
    public void Dial(string val) 
    { 
        // check for characterLimit and input validation
        if (string.IsNullOrWhiteSpace(val)
            || m_display.text.Length >= m_characterLimit) 
        { 
            return; 
        }
        m_display.text += val;
        DialExtension(val);
    }
    public void CancelCall()
    {
        StopResultPlayback();
        m_calling = false;
        m_anim.SetBool("Calling", false);
        OnCallCancel?.Invoke(m_confirmedInput, m_extensionInput);
    }
    // Start Call, check puzzle phone book for results 
    public void ConfirmInput()
    {
        // TODO: Need a short dial waiting/ringtone sfx + animation
        // Find valid result and output event wioth audio + subtitle (maybe?)
        m_confirmedInput = m_display.text;
        if(m_eventSet.TryGetValue(m_confirmedInput, out var result)) 
        {
            OnDial?.Invoke(m_confirmedInput);
            m_calling = true;
            //TODO: trigger audio response here + subtitle is needed
            m_anim.SetBool("Calling", true);
            StartCoroutine(PlayResultSequence(result.PlayThis));
        }
    }
    public void ClearDial() 
    {
        m_display.text = "";
        m_confirmedInput = "";
        m_extensionInput = "";
    }
    void DialExtension(string val) 
    {
        if (m_calling) 
        {
            m_extensionInput += val;
        }
    }
    void StopResultPlayback() 
    { 
        if (!m_calling || !m_director.playableGraph.IsPlaying()) 
        {
            return;
        }
        m_director.Stop();
        m_calling = false;
        StopAllCoroutines();
    }
    IEnumerator PlayResultSequence(PlayableAsset sequence)
    {
        if (sequence == null) 
        {
            yield break;
        }
        m_director.playableAsset = sequence;
        yield return new WaitForSeconds(Random.Range(0.8f, 3f));
        m_director.Play();
        yield return new WaitForSeconds((float)sequence.duration + 0.5f);
        OnCallEnd?.Invoke(m_confirmedInput, m_extensionInput);
        m_calling = false;
        m_anim.SetBool("Calling", false);
    }
}
