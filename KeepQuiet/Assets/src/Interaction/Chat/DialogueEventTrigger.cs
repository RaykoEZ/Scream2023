using Curry.Events;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Trigger_", menuName = "Chat/Trigger Dialogue")]
public class DialogueEventTrigger : ScriptableObject 
{
    [SerializeField] List<Dialogue> m_monologue = default;
    [SerializeField] CurryGameEventTrigger m_displayToTrigger = default;
    [SerializeField] CurryGameEventTrigger m_stateToTrigger = default;
    public void Trigger()
    {
        m_stateToTrigger?.TriggerEvent();
        m_displayToTrigger?.TriggerEvent(
            new DialogueInfo(m_monologue));
    }
}
public class DialogueInfo : EventInfo
{
    public List<Dialogue> Content { get; private set; }
    public DialogueInfo(List<Dialogue> content)
    {
        Content = new List<Dialogue>(content);
    }
}