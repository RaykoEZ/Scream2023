using UnityEngine;
using UnityEngine.EventSystems;
// Extract to player's desktop from game app when dragged out of game window
public class ExternalDraggableObject : DraggableObject
{
    [SerializeField] bool m_extractEnabled = default;
    [SerializeField] FileWriteDetail m_toWrite = default;
    protected FileWriter m_writer = new FileWriter();
    protected virtual FileWriteDetail WriteFile { 
        get { return m_toWrite; } set { m_toWrite = value; }
    }
    public bool ExtractEnabled {
        get => m_extractEnabled; 
        set => m_extractEnabled = value; }
    // stops file from writing duplicates
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (m_extractEnabled && !screenRect.Contains(eventData.position)) 
        {
            // If we are dragging an object out of app window, trigger event
            OnDragOutOfApplication();
            m_extractEnabled = false;
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
