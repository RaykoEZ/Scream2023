using Curry.Events;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewTriggerDialogue", menuName = "Chat/Trigger Dialogue")]
public class DialogueEventTrigger : ScriptableObject 
{
    [SerializeField] List<DialogueContent> m_monologue = default;
    [SerializeField] CurryGameEventTrigger m_displayToTrigger = default;
    [SerializeField] CurryGameEventTrigger m_triggerOnShow = default;
    [SerializeField] CurryGameEventTrigger m_triggerOnFinish = default;
    public IReadOnlyList<DialogueContent> Monologue { get => m_monologue; }
    public void TriggerDisplay()
    {
        m_displayToTrigger?.TriggerEvent(
            new DialogueInfo(m_monologue));
    }
}
public class DialogueInfo : EventInfo
{
    public List<DialogueContent> Content { get; private set; }
    public DialogueInfo(List<DialogueContent> content)
    {
        Content = content;
    }
}