using Curry.Events;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewTriggerDialogue", menuName = "Chat/Trigger Dialogue")]
public class DialogueEventTrigger : ScriptableObject 
{
    [SerializeField] List<GuideStep> m_monologue = default;
    [SerializeField] CurryGameEventTrigger m_displayToTrigger = default;
    [SerializeField] CurryGameEventTrigger m_stateToTrigger = default;
    public IReadOnlyList<GuideStep> Monologue { get => m_monologue; }
    public void Trigger()
    {
        m_stateToTrigger?.TriggerEvent(new EventInfo());
        m_displayToTrigger?.TriggerEvent(
            new DialogueInfo(m_monologue));
    }
}
public class DialogueInfo : EventInfo
{
    public List<GuideStep> Content { get; private set; }
    public DialogueInfo(List<GuideStep> content)
    {
        Content = new List<GuideStep>(content);
    }
}