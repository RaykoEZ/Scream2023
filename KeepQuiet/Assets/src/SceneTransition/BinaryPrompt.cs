using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class BinaryPrompt : MonoBehaviour 
{
    [SerializeField] TemporaryInputAction m_binaryInput = default;
    [SerializeField] UnityEvent m_yes = default;
    [SerializeField] UnityEvent m_no = default;
    void Start()
    {
        m_binaryInput?.Disable();
    }
    public void ActivatePrompt()
    {
        m_binaryInput?.Enable();
    }
    public void OnPromptTrigger(InputAction.CallbackContext c) 
    {
        // did player press n/y?
        float result = c.action.ReadValue<float>();
        if (result > 0f) 
        {
            m_yes?.Invoke();
        }
        else 
        {
            m_no?.Invoke();
        }
    }
}
