using UnityEngine;
using Curry.Explore;

public class PhoneScreenTransitionHandler : MonoBehaviour
{
    [SerializeField] HideableUITrigger m_home = default;
    [SerializeField] HideableUITrigger m_contact = default;
    [SerializeField] HideableUITrigger m_call = default;
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
    public void ToContact()
    {
        TransitionTo(m_contact);
    }
    public void ToDial() 
    {
        TransitionTo(m_call);
    }
    void TransitionTo(HideableUITrigger goTo) 
    {
        m_currentlyShowing?.Hide();
        m_currentlyShowing = goTo;
        m_currentlyShowing?.Show();
    }
}
