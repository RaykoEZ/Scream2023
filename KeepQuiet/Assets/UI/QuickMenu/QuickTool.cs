using System;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnToolUpdate(QuickTool toUpdate);
[Serializable]
public class QuickTool : DraggableObject
{
    public event OnToolUpdate OnEnter;
    public event OnToolUpdate OnExit;
    public event OnToolUpdate OnUse;
    public event OnToolUpdate OnReturn;
    public override void OnBeginDrag(PointerEventData eventData)
    {
        // Drag the tool out of the tool bar
        OnUse?.Invoke(this);
    }
    public override void OnDrag(PointerEventData eventData)
    {
    }
    public override void ReturnToBeforeDrag()
    {
        OnReturn?.Invoke(this);
    }
    public virtual void OnPointerEnter() 
    {
        OnEnter?.Invoke(this);
    }
    public virtual void OnPointerExit()
    {
        OnExit?.Invoke(this);
    }
}

