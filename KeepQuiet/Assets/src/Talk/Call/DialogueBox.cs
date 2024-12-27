using Curry.Events;
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
    // Only do angry for dialogue box animation
    public void Show(bool instant, NpcEmotion emote) 
    {
        GetAnim?.SetBool("Angry", emote == NpcEmotion.Angry);
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