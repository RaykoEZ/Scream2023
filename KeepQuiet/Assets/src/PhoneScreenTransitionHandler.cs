using UnityEngine;
using Curry.Explore;

public class PhoneScreenTransitionHandler : MonoBehaviour
{
    [SerializeField] HideableUITrigger m_home = default;
    [SerializeField] HideableUITrigger m_settings = default;
    [SerializeField] HideableUITrigger m_contact = default;
    [SerializeField] HideableUITrigger m_chat = default;
    [SerializeField] HideableUITrigger m_call = default;
    [SerializeField] HideableUITrigger m_lock = default;
    HideableUITrigger m_currentlyShowing;
    void Start() 
    {
        ToHome();
    }
    //TODO: secret app for later
    public void ToHome() 
    {
        TransitionTo(m_home);
    }
    public void ToSettings() 
    {
        TransitionTo(m_settings);
    }
    public void ToContact()
    {
        TransitionTo(m_contact);
    }
    public void ToChat() 
    {
        TransitionTo(m_chat);
    }
    public void ToDial() 
    {
        TransitionTo(m_call);
    }
    public void LockScreen() 
    {
        m_currentlyShowing?.Hide();
        m_lock?.Show();
    }
    public void UnlockScreen() 
    {
        m_currentlyShowing?.Show();
        m_lock?.Hide();
    }
    void TransitionTo(HideableUITrigger goTo) 
    {
        m_currentlyShowing?.Hide();
        m_currentlyShowing = goTo;
        m_currentlyShowing?.Show();
    }
}
