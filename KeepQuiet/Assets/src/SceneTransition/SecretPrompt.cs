using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class SecretPrompt : MonoBehaviour 
{
    [SerializeField] TemporaryInputAction m_secretInput = default;
    [SerializeField] UnityEvent m_reconnect = default;
    [SerializeField] UnityEvent m_disconnect = default;
    public void OnPromptTrigger(InputAction.CallbackContext c) 
    {
        // did player press n/y?
        bool reconnect = c.action.ReadValue<bool>();
        if (reconnect) 
        {
            m_reconnect?.Invoke();
        }
        else 
        {
            m_disconnect?.Invoke();
        }
    }
    public void ActivatePrompt() 
    {
        m_secretInput?.Enable();
    }
    void Start()
    {
        m_secretInput?.Disable();
    }
}
