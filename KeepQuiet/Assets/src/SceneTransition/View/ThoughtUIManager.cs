using Curry.Events;
using Curry.Explore;
using UnityEngine;
public class ThoughtUIManager : MonoBehaviour 
{
    [SerializeField] HideableUI m_thoughtInventory = default;
    [SerializeField] TemporaryInputAction m_dragCancel = default;
    HideableUI m_currentDropzone;
    DraggableObject m_dragging;
    public void EnableDropzone(ThoughtDropZone dropzone)
    {
        if (dropzone == null) return;
        m_currentDropzone = dropzone.DisplayUI;
    }
    public void OnThoughtDrag(EventInfo info)
    {
        if (m_currentDropzone == null || 
            info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("thought", out object result) &&
            result is DraggableObject drag)
        {
            m_dragging = drag;
            m_dragCancel?.Enable();
            m_currentDropzone?.Show();
            m_thoughtInventory?.Hide();
        }
    }
    public void OnThoughtDrop() 
    {
        if(m_currentDropzone != null) 
        {
            m_currentDropzone?.Hide();
            m_thoughtInventory?.Hide();
        }
        m_dragCancel.Disable();
        m_dragging = null;
        m_currentDropzone = null;
    }
    public void OnCacnelDrag()
    {
        m_dragging?.ReturnToBeforeDrag();
        m_dragging = null;
    }
}
