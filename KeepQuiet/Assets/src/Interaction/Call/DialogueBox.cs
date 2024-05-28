using Curry.Explore;
using System;
using TMPro;
using UnityEngine;

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
}
[Serializable]
public class TutorialStep 
{
    public DialogueBox Display;
    public AudioClip PlaySound;
    [TextArea]
    public string Content;
    public virtual void Show()
    {
        Display?.SetContent(Content);
        Display?.Audio?.PlayOneShot(PlaySound);
        Display?.Show();
    }
}
