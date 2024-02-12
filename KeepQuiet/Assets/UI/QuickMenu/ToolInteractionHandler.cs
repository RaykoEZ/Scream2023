using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;

public class ToolInteractionHandler : MonoBehaviour
{
    [SerializeField] GameStateManager m_gameState = default;
    [SerializeField] ToolBarUIAnimationHandler m_anim = default;
    [SerializeField] ToolAimIcon m_aimIcon = default;
    [SerializeField] ToolAimIcon m_torch = default;
    [SerializeField] ToolAimIcon m_specialTorch = default;
    [SerializeField] List<QuickTool> m_tools = default;
    // tool we are currently using
    QuickTool m_using;
    // the current tool aiming object
    ToolAimIcon m_aiming;
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
        m_aiming?.HideCursor();
        m_gameState?.OnToolReturn(tool.ToolName);
        m_anim?.Show();
    }
    public void ReturnTool() 
    {
        ReturnTool(m_using);
    }
    public void UseTool(QuickTool tool) 
    {
        if (tool == null) return;
        EToolName toolName = tool.ToolName;
        ToolAimIcon toolAimRef;
        switch (toolName)
        {
            case EToolName.Bat:
                toolAimRef = m_aimIcon;
                break;
            case EToolName.Torch:
                toolAimRef = m_torch;
                break;
            case EToolName.SpecialTorch:
                toolAimRef = m_specialTorch;
                break;
            case EToolName.CoatHanger:
                toolAimRef = m_aimIcon;
                break;
            case EToolName.Hook:
                toolAimRef = m_aimIcon;
                break;
            default:
                toolAimRef = m_aimIcon;
                break;
        }
        m_aiming = toolAimRef;
        m_using = tool;
        m_using.OnReturn += ReturnTool;
        m_aiming?.ShowCursor();
        m_gameState?.OnToolUse(toolName);
        m_anim?.Hide();
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

