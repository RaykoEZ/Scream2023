using Curry.Explore;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class DialogueBox : HideableUI 
{
    [SerializeField] TextMeshProUGUI m_content = default;
    [SerializeField] AudioSource m_audio = default;
    public AudioSource Audio { get => m_audio; }
    public void SetContent(string toSet) 
    {
        m_content.text = toSet;
    }
    public void Show(bool instant, bool angry) 
    {
        GetAnim?.SetBool("Angry", angry);
        GetAnim?.SetBool("Instant", instant);
        base.Show();
    }
    public override void Hide()
    {
        // reset dialogue state
        GetAnim?.SetBool("Angry", false);
        GetAnim?.SetBool("Instant", false);
        base.Hide();
    }
}
[Serializable]
public class GuideStep 
{
    public bool ShowInstantly;
    public bool Angry;
    public AudioClip PlaySound;
    [TextArea(5, 10)]
    public string Content;
    [SerializeField] protected DialogueBox m_display = default;
    [SerializeField] UnityEvent m_triggerOnShow = default;
    public virtual void Show()
    {
        if (PlaySound != null) 
        {
            m_display?.Audio?.PlayOneShot(PlaySound);
        }
        m_triggerOnShow?.Invoke();
        m_display?.SetContent(Content);
        m_display?.Show(ShowInstantly, Angry);
    }
    public virtual void Transition(GuideStep nextStep) 
    {
        bool sameDialogueBox = nextStep.m_display == m_display;
        // Don't hide if transitioning the same dialogue box
        if (sameDialogueBox && nextStep.ShowInstantly) 
        {
            return;
        }
        m_display?.Hide();
    }
    public void End() 
    {
        m_display?.Hide();
    }
}
