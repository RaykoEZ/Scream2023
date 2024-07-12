using Curry.Events;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
// onCancel: action to invoke when card activation is cancelled
public delegate void OnThoughtDrop(ThoughtBubble thought);
// For deploying any interactable from hand to play zone 
public class ThoughtDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] CurryGameEventListener m_onThoughtDrag = default;
    [SerializeField] UnityEvent<ThoughtBubble> m_onDropped = default;
    public event OnThoughtDrop ThoughtDropping;
    void OnEnable() 
    {
        m_onThoughtDrag?.Init();
    }
    void OnDisable() 
    {
        m_onThoughtDrag?.Shutdown();
    }
    // Called before the dropped card invokes its OnDragEnd,
    // trigger drop event when drag finishes (drop starts)
    public virtual void OnDrop(PointerEventData eventData)
    {
        DraggableObject draggable;
        if (eventData.pointerDrag.TryGetComponent(out draggable) && draggable is ThoughtBubble thought)
        {
            draggable.OnDragFinish += PrepareDrop;
        }
    }
    // Drop event, called after the draggable card finishes its OnDragEnd
    protected virtual void PrepareDrop(DraggableObject toDrop)
    {
        toDrop.OnDragFinish -= PrepareDrop;
        if (toDrop is ThoughtBubble thought) 
        {
            DropCard(thought);
            ThoughtDropping?.Invoke(thought);
        }
        else 
        {
            toDrop.ReturnToBeforeDrag();
        }
    }
    public virtual void DropCard(ThoughtBubble toDrop) 
    {
        int dropIdx = GetDropPosition(toDrop.transform.position.x);
        toDrop?.DropObject(transform, dropIdx);
        m_onDropped?.Invoke(toDrop);
    }
    // Called when card is dropped into this zone
    protected int GetDropPosition(float dropX)
    {
        int ret;
        for (ret = 0; ret < transform.childCount; ++ret)
        {
            if (dropX < transform.GetChild(ret).transform.position.x)
            {
                break;
            }
        }
        return ret;
    }
}
