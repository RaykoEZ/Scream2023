using Curry.Explore;
using UnityEngine;
using UnityEngine.UI;
public class PhoneNotificationHandler : MonoBehaviour
{
    [SerializeField] Animator m_toggleAnim = default;
    [SerializeField] HideableUI m_phoneUI = default;
    [SerializeField] AudioSource m_ring = default;
    [SerializeField] Image m_toggleIcon = default;
    [SerializeField] Sprite m_callAlert = default;
    [SerializeField] Sprite m_messageAlert = default;
    [SerializeField] CallHandler m_call = default;
    [SerializeField] ChatManager m_chat = default;
    Sprite m_defaultSprite;
    DialogueNode m_newMessage;
    private void Start()
    {
        m_defaultSprite = m_toggleIcon.sprite;
    }
    // If phone rings when phone isn't toggled on, alert player with animated toggle icon
    public void Call(string incomingNumber, DialEvent onAccept) 
    {
        ShowToggle();
        m_toggleIcon.sprite = m_callAlert;
        // Start glowing / animating
        AnimateAlertIcon();
        m_call.CallPhone(incomingNumber, onAccept);
    }
    public void MessagePlayer(DialogueNode newDialogue) 
    {
        ShowToggle();
        m_newMessage = newDialogue;
        m_toggleIcon.sprite = m_messageAlert;
        AnimateAlertIcon();
        m_chat.OnNewMessage(newDialogue);
    }
    public void MessageNpc(DialogueNode dialogue) 
    {
        m_chat.OnNewMessage(dialogue);
        m_chat.BeginChat();
    }
    public void HideToggle() 
    {
        if (!m_toggleAnim.GetBool("Alert")) 
        {
            m_toggleAnim.SetBool("Show", false);
        }
    }
    public void ShowToggle() 
    {
        m_toggleAnim.SetBool("Show", true);
    }
    void AnimateAlertIcon() 
    {
        m_ring.Play();
        // Animate Toggle Icon here
        m_toggleAnim.SetBool("Alert", true);
    }
    public void PickupPhone() 
    {
        m_toggleIcon.sprite = m_defaultSprite;
        m_toggleAnim.SetBool("Alert", false);
        m_ring.Stop();
        if (m_newMessage != null) 
        {
            m_chat.BeginChat();
            m_newMessage = null;
        }
        else 
        {
            m_phoneUI.Toggle();
        }
    }
}
