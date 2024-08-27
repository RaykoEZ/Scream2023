using Curry.Events;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CanvasGroup))]
public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Serializable]
    protected struct UITriggers
    {
        [SerializeField] CurryGameEventTrigger m_cardDragTrigger;
        [SerializeField] CurryGameEventTrigger m_cardDropTrigger;
        public CurryGameEventTrigger DragTrigger { get { return m_cardDragTrigger; } }
        public CurryGameEventTrigger DropTrigger { get { return m_cardDropTrigger; } }
    }
    [SerializeField] bool m_draggable = default;
    public delegate void OnDragUpdate(DraggableObject dragged);
    public event OnDragUpdate OnDragFinish;
    public event OnDragUpdate OnDragBegin;
    protected Vector2 m_anchorOffset = Vector2.zero;
    Transform m_origin;
    int m_originIndex;

    public virtual bool GetDraggable()
    {
        return m_draggable;
    }
    protected virtual void SetDraggable(bool value)
    {
        m_draggable = value;
        GetComponent<CanvasGroup>().blocksRaycasts = value;
    }

    protected virtual Transform OnDragParent => transform.parent;
    // Move one above original parent when dragging the object 
    protected virtual void OnEnable()
    {
        SetDraggable(m_draggable);
    }
    protected virtual void OnDisable() 
    {
        SetDraggable(false);
    }
    public virtual void SetDropOrigin(Transform parent, int siblingIndex = 0)
    {
        m_origin = parent;
        m_originIndex = siblingIndex;
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        LeaveOrigin(eventData);
        OnDragBegin?.Invoke(this);
    }
    protected virtual void LeaveOrigin(PointerEventData eventData)
    {
        // Set default drop to return to hand
        SetDropOrigin(transform.parent, transform.GetSiblingIndex());
        // move parent to intermediate parent until we see a drop zone/return to original parent
        Vector3 scale = transform.localScale;
        transform.SetParent(OnDragParent, true);
        transform.localScale = scale;
        Vector2 objectPos = eventData.pressEventCamera.WorldToScreenPoint(transform.position);
        m_anchorOffset = eventData.position - objectPos;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        // Do not move when drag is held, if the object needs to do something else
        if (GetDraggable())
        {
            SetDragPosition(eventData);
        }
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        FinishDragCallback();
        ReturnToBeforeDrag();
    }
    public virtual void DropObject(Transform parent, int siblingIndex = 0)
    {
        transform.SetParent(parent, false);
        transform.SetSiblingIndex(siblingIndex);
    }
    public virtual void ReturnToBeforeDrag()
    {
        DropObject(m_origin, m_originIndex);
    }
    protected void FinishDragCallback()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = GetDraggable();
        OnDragFinish?.Invoke(this);
    }
    protected virtual void SetDragPosition(PointerEventData e)
    {
        Vector2 worldPos = e.pressEventCamera.ScreenToWorldPoint(e.position - m_anchorOffset);
        GetComponent<RectTransform>().position = worldPos;
    }
}