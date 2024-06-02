using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InspectDocument : InspectionDisplay
{
    [SerializeField] Image m_noteZoom = default;
    [SerializeField] Image m_noteZoomMore = default;
    public override void Init(SaveData save) 
    {
        StopZoom();
    }
    public void ZoomNote() 
    {
        m_noteZoom.gameObject.SetActive(true);
    }
    public void ZoomNoteMore() 
    {
        m_noteZoomMore.gameObject.SetActive(true);
    }
    public void StopZoom()
    {
        m_noteZoom.gameObject.SetActive(false);
        m_noteZoomMore.gameObject.SetActive(false);
    }
    public override IEnumerator OnExit()
    {
        yield return null;
    }
}