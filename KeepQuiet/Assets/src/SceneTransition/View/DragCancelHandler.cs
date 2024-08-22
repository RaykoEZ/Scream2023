using Curry.Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragCancelHandler : MonoBehaviour
{
    [SerializeField] InputActionReference m_cancelDragAction = default;
    DraggableObject m_dragging;
    public void EnableCancel(EventInfo info)
    {
        if (info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("thought", out object result) &&
            result is DraggableObject drag)
        {
            m_dragging = drag;
            m_cancelDragAction.action.performed += OnCacnelDrag;
        }
    }
    public void DisableCancel() 
    {
        m_cancelDragAction.action.performed -= OnCacnelDrag;
        m_dragging = null;
    }
    void OnCacnelDrag(InputAction.CallbackContext c)
    {
        m_cancelDragAction.action.performed -= OnCacnelDrag;
        m_dragging?.ReturnToBeforeDrag();
        m_dragging = null;
    }
}
