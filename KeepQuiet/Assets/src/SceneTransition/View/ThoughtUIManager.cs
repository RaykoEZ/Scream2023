using Curry.Events;
using Curry.Explore;
using UnityEngine;
public class ThoughtUIManager : MonoBehaviour 
{
    [SerializeField] HideableUI m_thoughtInventory = default;
    [SerializeField] TemporaryInputAction m_dragCancel = default;
    HideableUI m_currentDisplayRef;
    DraggableObject m_dragging;
    public void EnableDropzone(ThoughtDropZone dropzone)
    {
        if (dropzone == null) return;
        m_currentDisplayRef = dropzone.DisplayUI;
    }
    public void DisableDropzone() 
    {
        m_currentDisplayRef = null;
    }
    public void OnThoughtDrag(EventInfo info)
    {
        if (m_currentDisplayRef == null || 
            info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("thought", out object result) &&
            result is DraggableObject drag)
        {
            m_dragging = drag;
            m_dragCancel?.Enable();
            m_currentDisplayRef?.Show();
            m_thoughtInventory?.Hide();
        }
    }
    public void OnThoughtDrop() 
    {
        m_dragCancel.Disable();
        m_currentDisplayRef?.Hide();
        m_thoughtInventory?.Hide();
        m_dragging = null;
    }
    public void OnCacnelDrag()
    {
        m_dragging?.ReturnToBeforeDrag();
        m_dragging = null;
    }
}
