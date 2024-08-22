using Curry.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class ThoughtTester : MonoBehaviour 
{
    [SerializeField] InputAction m_obtainThought = default;
    [SerializeField] InputAction m_glowThought = default;
    [SerializeField] CurryGameEventTrigger m_obtain = default;
    [SerializeField] CurryGameEventTrigger m_glow = default;

    [SerializeField] ThoughtDetail m_toObtain = default;
    [SerializeField] List<ThoughtDetail> m_toGlow = default;
    void OnEnable()
    {
        m_obtainThought.performed += Test_ObtainThought;
        m_glowThought.performed += Test_GlowThought;
        m_obtainThought.Enable();
        m_glowThought.Enable();
    }
    void OnDisable()
    {
        m_obtainThought.Disable();
        m_glowThought.Disable();
        m_obtainThought.performed -= Test_ObtainThought;
        m_glowThought.performed -= Test_GlowThought;
    }
    public void Test_ObtainThought(CallbackContext c) 
    {
        var payload = new Dictionary<string, object>
        {
            { "thought", m_toObtain}
        };
        EventInfo info = new EventInfo(payload);
        m_obtain?.TriggerEvent(info);
    }
    public void Test_GlowThought(CallbackContext c) 
    {
        var payload = new Dictionary<string, object>
        {
            { "toDrop", m_toGlow}
        };
        EventInfo info = new EventInfo(payload);
        m_glow?.TriggerEvent(info);
    }
}
