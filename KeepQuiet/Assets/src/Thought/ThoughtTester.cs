using Curry.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThoughtTester : MonoBehaviour 
{
    [SerializeField] InputAction m_obtainThought = default;
    [SerializeField] InputAction m_glowThought = default;
    [SerializeField] CurryGameEventTrigger m_obtain = default;
    [SerializeField] CurryGameEventTrigger m_glow = default;

    [SerializeField] ThoughtDetail m_dummy0 = default;
    [SerializeField] ThoughtDetail m_dummy1 = default;
    [SerializeField] ThoughtDetail m_dummy2 = default;
    public void Test_ObtainThought() 
    {
        var payload = new Dictionary<string, object>
        {
            { "thought", m_dummy0}
        };
        EventInfo info = new EventInfo(payload);
        m_obtain?.TriggerEvent(info);
    }
    public void Test_GlowThought() 
    {
        var list = new List<ThoughtDetail> 
        { m_dummy0, m_dummy1, m_dummy2 };
        var payload = new Dictionary<string, object>
        {
            { "toDrop", list}
        };
        EventInfo info = new EventInfo(payload);
        m_glow?.TriggerEvent(info);
    }
}
