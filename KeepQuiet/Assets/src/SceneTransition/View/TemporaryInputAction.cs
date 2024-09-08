using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
// Listens to an input action and trigger an event,
// unlistens after triggering once auto/ manual cancel
[System.Serializable]
public class TemporaryInputAction 
{
    [SerializeField] bool m_autoDisable = default;
    [SerializeField] InputActionReference m_inputTarget = default;
    [SerializeField] UnityEvent<InputAction.CallbackContext> m_triggerOnAction;
    public bool AutoDisable { get => m_autoDisable; set => m_autoDisable = value; }
    public virtual void Enable()
    {
        m_inputTarget.action.performed += Trigger;
    }
    public virtual void Disable()
    {
        m_inputTarget.action.performed -= Trigger;
    }
    protected virtual void Trigger(InputAction.CallbackContext c) 
    {
        if (m_autoDisable) 
        {
            m_inputTarget.action.performed -= Trigger;
        }
        m_triggerOnAction?.Invoke(c);
    }
}
