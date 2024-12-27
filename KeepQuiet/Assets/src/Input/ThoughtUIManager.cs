using Curry.Events;
using Curry.Explore;
using UnityEngine;
public class ThoughtUIManager : MonoBehaviour 
{
    [SerializeField] HideableUI m_thoughtInventory = default;
    [SerializeField] TemporaryInputAction m_dragCancel = default;
    HideableUI m_currentDropzoneRef;
    DraggableObject m_dragging;
    public void EnableDropzone(ThoughtDropZone dropzone)
    {
        if (dropzone == null) return;
        m_currentDropzoneRef = dropzone.DisplayUI;
    }
    public void DisableDropzone()
    {
        m_currentDropzoneRef = null;
    }
    public void OnThoughtDrag(EventInfo info)
    {
        if (m_currentDropzoneRef == null || 
            info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("thought", out object result) &&
            result is DraggableObject drag)
        {
            m_dragging = drag;
            m_dragCancel?.Enable();
            m_currentDropzoneRef?.Show();
            m_thoughtInventory?.Hide();
        }
    }
    public void OnThoughtDrop() 
    {
        if(m_currentDropzoneRef != null) 
        {
            m_currentDropzoneRef?.Hide();
            m_thoughtInventory?.Hide();
        }
        m_dragCancel.Disable();
        m_dragging = null;
    }
    public void OnCacnelDrag()
    {
        m_dragging?.ReturnToBeforeDrag();
        m_dragging = null;
    }
}
