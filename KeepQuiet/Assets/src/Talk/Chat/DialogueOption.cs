using Curry.Explore;
using TMPro;
using UnityEngine;
public delegate void OnReplyChosen(DialogueOption chosen);
public class DialogueOption : HideableUI
{
    [SerializeField] TextMeshProUGUI m_optionText = default;
    ChatOption m_optionValue;
    public event OnReplyChosen OnChosen;
    public ChatOption OptionValue { get => m_optionValue;}
    public void Init(ChatOption optVal)
    {
        m_optionValue = optVal;
        m_optionText.text = m_optionValue.Description;
    }
    public void Choose() 
    {
        OnChosen?.Invoke(this);
    }
}
