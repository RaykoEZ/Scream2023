using Curry.Events;
using Curry.Explore;
using UnityEngine.UI;
using UnityEngine;
using System.Dynamic;
// For displaying something attached to a message
public class ItemDisplayHandler : HideableUI 
{
    [SerializeField] Transform m_contentParent = default;
    [SerializeField] CurryGameEventListener m_onItemShow = default;
    GameObject m_currentlyInspecting;
    void Start()
    {
        m_onItemShow?.Init();
    }
    public void OnClickAttachment(EventInfo info) 
    {
        if (m_currentlyInspecting != null || 
            info == null || info.Payload == null) return;
        if(info.Payload.TryGetValue("inspect", out object result)
            && result is GameObject go) 
        {
            m_currentlyInspecting = Instantiate(go, m_contentParent);
            Show();
        }
    }
    public override void Hide()
    {
        base.Hide();
        // clear inspection object
        Destroy(m_currentlyInspecting);
    }
}
