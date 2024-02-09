using System.Collections.Generic;
using UnityEngine;

public class ToolInteractionHandler : MonoBehaviour
{
    [SerializeField] ToolAimIcon m_aimIcon = default;
    [SerializeField] List<QuickTool> m_tools = default;
    QuickTool m_using;
    private void Start()
    {
        foreach (var item in m_tools)
        {
            item.OnEnter += OnPointerEnter;
            item.OnExit += OnPointerExit;
        }
    }
    public void ReturnTool(QuickTool tool)
    {
        if (tool == null || m_using == null || tool != m_using) return;
        m_using.OnReturn -= ReturnTool;
        m_aimIcon.HideCursor();
    }
    public void ReturnTool() 
    {
        ReturnTool(m_using);
    }
    public void UseTool(QuickTool tool) 
    {
        if (tool == null) return;
        m_using = tool;
        m_using.OnReturn += ReturnTool;
        m_aimIcon.ShowCursor();
    }
    public void OnPointerEnter(QuickTool tool) 
    {
        if (tool == null) return;
        tool.OnUse += UseTool;
    }
    public void OnPointerExit(QuickTool tool) 
    {
        if (tool == null) return;
        tool.OnUse -= UseTool;

    }
}

