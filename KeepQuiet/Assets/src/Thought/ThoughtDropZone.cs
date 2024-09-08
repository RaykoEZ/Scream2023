using Curry.Explore;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
// onCancel: action to invoke when card activation is cancelled
public delegate void OnThoughtDrop(ThoughtBubble thought);
// For deploying any interactable from hand to play zone 
[RequireComponent(typeof(HideableUI))]
public class ThoughtDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] UnityEvent<ThoughtBubble> m_onDropped = default;
    public event OnThoughtDrop ThoughtDropping;
    public HideableUI DisplayUI => GetComponent<HideableUI>();
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
            Drop(thought);
            ThoughtDropping?.Invoke(thought);
        }
        else 
        {
            toDrop.ReturnToBeforeDrag();
        }
    }
    protected virtual void Drop(ThoughtBubble toDrop) 
    {
        int dropIdx = GetDropPosition(toDrop.transform.position.x);
        m_onDropped?.Invoke(toDrop);
        toDrop?.DropObject(transform, dropIdx);
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
