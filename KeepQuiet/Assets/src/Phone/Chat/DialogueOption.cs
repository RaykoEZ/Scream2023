using Curry.Explore;
using TMPro;
using UnityEngine;
public delegate void OnReplyChosen(DialogueOption chosen);
public class DialogueOption : HideableUI
{
    [SerializeField] TextMeshProUGUI m_optionText = default;
    DialogueNode m_optionValue;
    public event OnReplyChosen OnChosen;
    public DialogueNode OptionValue { get => m_optionValue;}
    public void Init(DialogueNode optVal)
    {
        m_optionValue = optVal;
        m_optionText.text = m_optionValue.Dialogues[0].Content;
    }
    public void Choose() 
    {
        OnChosen?.Invoke(this);
    }
}
