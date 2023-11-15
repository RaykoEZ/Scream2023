using UnityEngine;
using Curry.Explore;

public abstract class IncomingNotificationHandler: HideableUI 
{
    [SerializeField] protected AudioSource m_ring = default;
    [SerializeField] protected PhoneSettings m_setting = default;
    public abstract void Trigger(string callerNumber, bool loopRingTone = false, string alias = "");
}
