using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Curry.Explore;

public class PhoneAlertHandler : MonoBehaviour 
{
    [SerializeField] Animator m_toggleAnim = default;
    [SerializeField] AudioSource m_ring = default;
    [SerializeField] PhoneSettings m_setting = default;
    [SerializeField] Toggle m_phoneToggle = default;
    [SerializeField] Image m_toggleIcon = default;
    [SerializeField] Sprite m_callAlert = default;
    [SerializeField] Sprite m_messageAlert = default;
    [SerializeField] CallHandler m_call = default;
    [SerializeField] ChatManager m_chat = default;

    Sprite m_defaultSprite;
    private void Start()
    {
        m_defaultSprite = m_toggleIcon.sprite;
    }
    // If phone rings when phone isn't toggled on, alert player with animated toggle icon
    public void Call(string incomingNumber) 
    {
        m_toggleIcon.sprite = m_callAlert;
        // Start glowing / animating
        AnimateAlertIcon();
    }
    public void Message(string incomingNumber) 
    {
        m_toggleIcon.sprite = m_messageAlert;
        AnimateAlertIcon();
    }
    void AnimateAlertIcon() 
    {
        m_ring.clip = m_setting.GetRingtone();
        m_ring.volume = m_setting.GetVolume();
        m_ring.Play();
        // Animate Toggle Icon here
    }
    public void EndAlert() 
    {
        m_toggleIcon.sprite = m_defaultSprite;
    }
}

public class IncomingCall : HideableUI
{
    [SerializeField] TextMeshProUGUI m_incomingNumber = default;
    string m_currentCaller = "";
    public event OnIncomingNotify OnCallAccept;
    public event OnIncomingNotify OnCallDeny;
    public void Trigger(string callerNumber, string alias = "")
    {
        m_currentCaller = string.IsNullOrEmpty(alias) ? callerNumber : alias;
        m_incomingNumber.text = m_currentCaller;
        Show();
    }
    public void Accept()
    {
        OnCallAccept?.Invoke(m_currentCaller);
        Hide();
    }
    public void Deny()
    {
        OnCallDeny?.Invoke(m_currentCaller);
        Hide();
    }
}
