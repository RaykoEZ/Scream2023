using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

[Serializable]
public struct DialEvent 
{
    [SerializeField] AudioClip m_audio;
    [SerializeField] string m_subtitle;
    public AudioClip Audio => m_audio;
    public string Subtitle => m_subtitle;
}
[CreateAssetMenu(fileName = "Dial_", menuName = "Dial/Result", order = 0)]
public class DialResult : ScriptableObject 
{
    [SerializeField] string m_sequence = default;
    [SerializeField] DialEvent m_eventToTrigger = default;
    public string Sequence => m_sequence; 
    public DialEvent EventToTrigger => m_eventToTrigger;
}

public class DialHandler : MonoBehaviour 
{
    [SerializeField] int m_characterLimit = default;
    [SerializeField] TextMeshProUGUI m_display = default;
    [SerializeField] List<DialResult> m_results = default;
    Dictionary<string, DialEvent> m_eventSet = new Dictionary<string, DialEvent>();
    string m_confirmedInput = "";
    private void Start()
    {
        foreach(var result in m_results) 
        {
            m_eventSet.Add(result.Sequence, result.EventToTrigger);
        }
    }
    public void Dial(int number) 
    { 
        // check for characterLimit and input validation
        if (number > 9 
            || number < 0 
            || m_display.text.Length >= m_characterLimit) 
        { 
            return; 
        }
        m_display.text += number.ToString();
    }
    
    public void DialHash() 
    {
        m_display.text += "#";

    }
    public void DialStar() 
    {
        m_display.text += "*";
    }
    // Start Call, check puzzle phone book for results 
    public void ConfirmInput()
    {
        // TODO: Need a short dial waiting/ringtone sfx + animation
        // Find valid result and output event wioth audio + subtitle (maybe?)
        m_confirmedInput = m_display.text;
        if(m_eventSet.TryGetValue(m_confirmedInput, out var result)) 
        { 
            //TODO: trigger audio response here + subtitle is needed
        }
    }
    public void ClearDial() 
    {
        m_display.text = "";
        m_confirmedInput = "";
    }
}
