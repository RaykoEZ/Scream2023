using Curry.Explore;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DialogueBox : HideableUI 
{
    [SerializeField] TextMeshProUGUI m_content = default;
    public void SetContent(string toSet) 
    {
        m_content.text = toSet;
    }
}
[Serializable]
public class TutorialStep 
{
    public DialogueBox Display;
    [TextArea]
    public string Content;
}
