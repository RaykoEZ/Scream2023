using UnityEngine;
using UnityEngine.EventSystems;

public class ExternalDraggableObject : DraggableObject
{
    [SerializeField] FileWriteDetail m_toWrite = default;
    protected FileWriter m_writer = new FileWriter();
    protected virtual FileWriteDetail WriteFile { 
        get { return m_toWrite; } set { m_toWrite = value; }
    }
    // stops file from writing duplicates
    bool m_draggedOut = false;
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (!m_draggedOut && !screenRect.Contains(eventData.position)) 
        {
            // If we are dragging an object out of app window, trigger event
            OnDragOutOfApplication();
            m_draggedOut = true;
        }
    }
    protected virtual void OnDragOutOfApplication() 
    {
        m_writer.WriteToDesktop(
        WriteFile.Filename,
        "",
        WriteFile.RawContent);
    }
}
