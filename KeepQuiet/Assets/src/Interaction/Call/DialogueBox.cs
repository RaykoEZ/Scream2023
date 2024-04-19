using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DialogueBox : MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI m_content = default;
    public void SetContent(string toSet) 
    {
        m_content.text = toSet;
    }
    public void Show() 
    {
        GetComponent<Animator>()?.SetBool("BoxOn", true);
    }
    public void Hide() 
    {
        GetComponent<Animator>()?.SetBool("BoxOn", false);
    }
}
[Serializable]
public class TutorialStep 
{
    public DialogueBox Display;
    [TextArea]
    public string Content;
}
