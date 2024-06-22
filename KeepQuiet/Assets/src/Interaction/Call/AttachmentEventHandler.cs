using Curry.Events;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AttachmentEventHandler : MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI m_title = default;
    [SerializeField] CurryGameEventTrigger m_onClick = default;
    GameObject m_toDisplay;
    public void Init(string title, GameObject toDisplay) 
    {
        m_title.text = title;
        m_toDisplay = toDisplay;
    }
    public void Shutdown() 
    {
        m_title.text = "";
        m_toDisplay = null;
    }
    public void ShowAttachment() 
    {
        if (m_toDisplay == null) return;
        var payload = new Dictionary<string, object>
        {
            {"inspect", m_toDisplay }
        };
        m_onClick?.TriggerEvent(new EventInfo(payload));
    }
}
