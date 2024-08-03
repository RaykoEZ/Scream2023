using Curry.Events;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class QuestionBubble : ThoughtBubble
{
    
}
[RequireComponent(typeof(Animator))]
public class ThoughtBubble : DraggableObject
{
    [SerializeField] protected TextMeshProUGUI m_label = default;
    [SerializeField] protected CurryGameEventTrigger m_onConsume = default;
    [SerializeField] protected UITriggers m_ui = default;
    protected ThoughtDetail m_detailRef;
    public ThoughtDetail DetailRef => m_detailRef;
    public virtual void Init(ThoughtDetail detail)
    {
        m_detailRef = detail;
        m_label.text = detail.Description;
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        EventInfo info = new EventInfo();
        m_ui.DragTrigger?.TriggerEvent(info);
    }
    public override void DropObject(Transform parent, int siblingIndex = 0)
    {
        base.DropObject(parent, siblingIndex);
        EventInfo info = new EventInfo();
        m_ui.DropTrigger?.TriggerEvent(info);
    }
    public virtual void RemoveBubble()
    {
        m_onConsume?.TriggerEvent(
            new EventInfo(payload: new Dictionary<string, object> { { "thought", m_detailRef } }));
        Destroy(gameObject);
    }
}
