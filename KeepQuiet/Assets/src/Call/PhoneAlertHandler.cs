using UnityEngine;
using UnityEngine.UI;

public class PhoneAlertHandler : MonoBehaviour 
{
    [SerializeField] Animator m_toggleAnim = default;
    [SerializeField] AudioSource m_ring = default;
    [SerializeField] PhoneSettings m_setting = default;
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
        m_call.CallPhone(incomingNumber);
    }
    public void Message(DialogueNode newDialogue) 
    {
        m_toggleIcon.sprite = m_messageAlert;
        AnimateAlertIcon();
        m_chat.OnNewMessage(newDialogue);
    }
    void AnimateAlertIcon() 
    {
        m_ring.clip = m_setting.GetRingtone();
        m_ring.volume = m_setting.GetVolume();
        m_ring.Play();
        // Animate Toggle Icon here
        m_toggleAnim.SetBool("Alert", true);
    }
    public void EndAlert() 
    {
        m_toggleIcon.sprite = m_defaultSprite;
        m_toggleAnim.SetBool("Alert", false);
    }
}
