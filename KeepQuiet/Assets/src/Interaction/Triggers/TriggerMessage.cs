using UnityEngine;

public class TriggerMessage : TriggerInteraction
{
    [SerializeField] protected PlayerCaller m_trigger = default;
    [SerializeField] protected DialogueNode m_message = default;
    public override void Trigger()
    {
        m_trigger?.Message(m_message);
    }
}
