using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using TMPro;

public delegate void OnIncomingNotify(string callerNumber);
public delegate void OnCallCanceled(string dialed);
public delegate void OnCallFinish(string dialed);
public delegate void OnDialed(string dialed);
public class CallHandler : MonoBehaviour, ISettingUpdateListener<PhoneSettings>
{
    [SerializeField] int m_characterLimit = default;
    [SerializeField] PhoneSettings m_setting = default;
    [SerializeField] Animator m_anim = default;
    [SerializeField] AudioSource m_callAudio = default;
    [SerializeField] IncomingCall m_incoming = default;
    [SerializeField] TextMeshProUGUI m_display = default;
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] NpcManager m_npc = default;
    [SerializeField] List<DialResult> m_results = default;
    public event OnCallCanceled OnCallCancel;
    public event OnCallFinish OnCallEnd;
    public event OnDialed OnDial;
    Dictionary<string, DialEvent> m_eventSet = new Dictionary<string, DialEvent>();
    string m_confirmedInput = "";
    bool m_calling = false;
    Npc m_callingWith;
    DialEvent m_latestIncomingCall;
    public bool Calling => m_calling;
    private void Start()
    {
        m_setting.Listen(this);
        m_callAudio.volume = m_setting.GetVolume();
        m_incoming.OnCallAccept += OnIncomingCallAccept;
        m_incoming.OnCallDeny += CallDenied;
        foreach (var result in m_results) 
        {
            m_eventSet.Add(result.Sequence, result.EventToTrigger);
        }
    }
    void CallDenied(string incoming) 
    {
        // triggernpc deny event
        Npc denied = m_npc.Get(incoming);
        denied?.OnCallDenied();
    }
    void OnIncomingCallAccept(string incoming) 
    {
        // maybe choose to interrupt current call?
        if (m_calling) return;
        Npc accepted = m_npc.Get(incoming);
        accepted?.OnCallAccepted();
        BeginCall(incoming, m_latestIncomingCall);
    }
    public void CallPhone(string incomingNumber, DialEvent onAccept) 
    {
        if (m_calling) return;
        m_latestIncomingCall = onAccept;
        m_incoming.Trigger(incomingNumber);
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
    }
    public void CancelCall()
    {
        m_callingWith?.OnPlayerCallCanceled();
        StopResultPlayback();
        m_calling = false;
        m_anim.SetBool("Calling", false);
        OnCallCancel?.Invoke(m_confirmedInput);
    }
    // Start Call, check puzzle phone book for results 
    public void ConfirmInput()
    {
        // TODO: Need a short dial waiting/ringtone sfx + animation
        // Find valid result and output event wioth audio + subtitle (maybe?)
        if(m_eventSet.TryGetValue(m_confirmedInput, out var result)) 
        {
            BeginCall(m_display.text, result);
        }
    }
    void BeginCall(string callDisplay, DialEvent result) 
    {
        m_confirmedInput = callDisplay;
        m_callingWith = m_npc.Get(m_confirmedInput);
        m_callingWith?.OnPlayerDialed();
        OnDial?.Invoke(m_confirmedInput);
        m_calling = true;
        //TODO: trigger audio response here + subtitle is needed
        m_anim.SetBool("Calling", true);
        StartCoroutine(PlayResultSequence(result.PlayThis));
    }
    public void ClearDial()
    {
        m_display.text = "";
        m_confirmedInput = "";
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
        OnCallEnd?.Invoke(m_confirmedInput);
        m_calling = false;
        m_anim.SetBool("Calling", false);
        m_npc.OnCallFinished(m_confirmedInput);
    }
    public void OnUpdate(PhoneSettings updated)
    {
        m_callAudio.volume = updated.GetVolume();
    }
}
