using Curry.Events;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Animator))]
public class ThoughtBubble : DraggableObject
{
    [SerializeField] protected TextMeshProUGUI m_label = default;
    [SerializeField] protected CurryGameEventListener m_onGlow = default;
    [SerializeField] protected CurryGameEventListener m_onOutcomeTrigger = default;
    [SerializeField] protected UITriggers m_ui = default;
    protected ThoughtDetail m_detailRef;
    public ThoughtDetail DetailRef => m_detailRef;
    protected override Transform OnDragParent => transform.parent.parent;

    public virtual void Init(ThoughtDetail detail)
    {
        m_onGlow?.Init();
        m_onOutcomeTrigger?.Init();
        m_detailRef = detail;
        m_label.text = detail.Description;
    }
    public virtual void Shutdown() 
    {
        m_onOutcomeTrigger?.Shutdown();
        m_onGlow?.Shutdown();
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        var payload = new Dictionary<string, object> { { "thought", this } };
        EventInfo info = new EventInfo(payload);
        m_ui.DragTrigger?.TriggerEvent(info);
    }
    public override void DropObject(Transform parent, int siblingIndex = 0)
    {
        base.DropObject(parent, siblingIndex);
        var payload = new Dictionary<string, object> { { "thought", this } };
        EventInfo info = new EventInfo(payload);
        m_ui.DropTrigger?.TriggerEvent(info);
    }
    public virtual void OnBubbleGlow(EventInfo info) 
    {
        if (info == null || info.Payload == null) return;
        //if this thought should be glowing, glow
        var payload = info.Payload;
        if(payload.TryGetValue("toDrop", out object result) &&
            result is List<ThoughtDetail> details &&
            details.Contains(DetailRef)) 
        {
            GetComponent<Animator>()?.SetBool("Glow", true);
        }
    }
    // when player chose and interacted with another thought bubble
    public virtual void OnThoughtTriggered() 
    {
        GetComponent<Animator>()?.SetBool("Glow", false);
    }
}
