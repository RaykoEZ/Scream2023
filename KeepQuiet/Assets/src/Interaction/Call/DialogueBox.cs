using Curry.Events;
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
    [SerializeField] CurryGameEventTrigger m_onShow = default;
    [TextArea(5, 10)]
    public string Content;
    public CurryGameEventTrigger OnShow { get => m_onShow; }
    public virtual void SetContent(GuideStep content) 
    {
        ShowInstantly = content.ShowInstantly;
        Angry = content.Angry;
        PlaySound = content.PlaySound;
        Content = content.Content;
        m_onShow = content.m_onShow;
    }
}
