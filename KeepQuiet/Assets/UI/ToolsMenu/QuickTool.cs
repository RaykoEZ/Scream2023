using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EToolType
{ 
    Torch = 1,
    SpecialTorch = 2
}
public delegate void OnToolUpdate(QuickTool toUpdate);
[Serializable]
public class QuickTool : DraggableObject
{
    [SerializeField] EToolType m_toolName = default;
    public EToolType ToolName => m_toolName;
    public event OnToolUpdate OnEnter;
    public event OnToolUpdate OnExit;
    public event OnToolUpdate OnUse;
    bool m_using = false;
    public override void OnBeginDrag(PointerEventData eventData)
    {
        // Drag the tool out of the tool bar
        UseTool();
    }
    public override void OnDrag(PointerEventData eventData)
    {
    }
    public override void ReturnToBeforeDrag()
    {
    }
    public void UseTool() 
    {
        if (m_using) return;
        m_using = true;
        OnUse?.Invoke(this);
    }
    public void OnReturnTool() 
    {
        m_using = false;
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

