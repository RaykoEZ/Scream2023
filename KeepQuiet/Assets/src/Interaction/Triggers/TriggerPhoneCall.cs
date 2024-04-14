using UnityEngine;

public class TriggerPhoneCall : TriggerInteraction
{
    [SerializeField] protected PlayerCaller m_trigger = default;
    [SerializeField] protected DialResult m_callContent = default;
    public override void Trigger()
    {
        m_trigger?.Call(m_callContent.Sequence, m_callContent.EventToTrigger);
    }
}
