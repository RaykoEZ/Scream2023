using Curry.Events;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Trigger_", menuName = "Chat/Trigger Dialogue")]
public class DialogueEventTrigger : ScriptableObject 
{
    [SerializeField] List<DialogueStep> m_monologue = default;
    [SerializeField] CurryGameEventTrigger m_displayToTrigger = default;
    [SerializeField] CurryGameEventTrigger m_stateToTrigger = default;
    public IReadOnlyList<DialogueStep> Monologue { get => m_monologue; }
    public void Trigger()
    {
        m_stateToTrigger?.TriggerEvent();
        m_displayToTrigger?.TriggerEvent(
            new DialogueInfo(m_monologue));
    }
}
public class DialogueInfo : EventInfo
{
    public List<DialogueStep> Content { get; private set; }
    public DialogueInfo(List<DialogueStep> content)
    {
        Content = new List<DialogueStep>(content);
    }
}