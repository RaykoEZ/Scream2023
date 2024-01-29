using System;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnToolUpdate(QuickTool toUpdate);
[Serializable]
public class QuickTool : DraggableObject 
{
    public event OnToolUpdate OnUse;
    public event OnToolUpdate OnReturn;
    bool m_isUsing = false;
    public bool IsUsing { get => m_isUsing; protected set => m_isUsing = value; }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        Use();
    }
    public override void ReturnToBeforeDrag()
    {
        base.ReturnToBeforeDrag();
        ReturnToUI();
    }
    public void Use()
    {
        // Drag the tool out of the tool bar
        OnUse?.Invoke(this);
    }
    public void ReturnToUI()
    {
        // put the tool back to the tool bar
        OnReturn?.Invoke(this);
    }

}

