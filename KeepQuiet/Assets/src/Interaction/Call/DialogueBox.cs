using Curry.Explore;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DialogueBox : HideableUI 
{
    [SerializeField] TextMeshProUGUI m_content = default;
    [SerializeField] AudioSource m_audio = default;
    public void SetContent(string toSet) 
    {
        m_content.text = toSet;
    }
    public override void Show()
    {
        m_audio?.Play();
        base.Show();
    }
}
[Serializable]
public class TutorialStep 
{
    public DialogueBox Display;
    [TextArea]
    public string Content;
}
