using UnityEngine;

public class TriggerMessage : TriggerInteraction
{
    [SerializeField] protected PhoneNotificationHandler m_trigger = default;
    [SerializeField] protected DialogueNode m_message = default;
    public override void Trigger()
    {
        m_trigger?.MessagePlayer(m_message);
    }
}
