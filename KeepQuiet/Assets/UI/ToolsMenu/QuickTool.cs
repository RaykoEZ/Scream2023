using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EToolType
{ 
    Bat = 0,
    Torch = 1,
    SpecialTorch = 2
}
public delegate void OnToolUpdate(QuickTool toUpdate);
[Serializable]
public class QuickTool : DraggableObject
{
    [SerializeField] EToolType m_toolName = default;
    [SerializeField] protected bool m_isUnlocked = default;
    public virtual bool IsUnlocked { get => m_isUnlocked; protected set => m_isUnlocked = value; }
    public EToolType ToolName => m_toolName;
    public event OnToolUpdate OnEnter;
    public event OnToolUpdate OnExit;
    public event OnToolUpdate OnUse;
    public event OnToolUpdate OnReturn;
    public event OnToolUpdate OnUnlock;
    public event OnToolUpdate OnLock;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // Drag the tool out of the tool bar
        OnUse?.Invoke(this);
    }
    public virtual void UnlockTool() 
    {
        IsUnlocked = true;
        OnUnlock?.Invoke(this);
    }
    public virtual void LockTool()
    {
        IsUnlocked = false;
        OnLock?.Invoke(this);
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

