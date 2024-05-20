using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
public class ToolInteractionHandler : MonoBehaviour
{
    [SerializeField] GameStateManager m_gameState = default;
    [SerializeField] ToolBarUIAnimationHandler m_anim = default;
    [SerializeField] ToolAimIcon m_aimIcon = default;
    [SerializeField] ToolAimIcon m_torchAim = default;
    [SerializeField] ToolAimIcon m_specialTorchAim = default;
    //TODO:Coat hanger object, draggable and modifiable
    [SerializeField] QuickTool m_torch = default;
    [SerializeField] QuickTool m_specialTorch = default;
    [SerializeField] QuickTool m_bat = default;
    // tool we are currently using
    QuickTool m_using;
    // the current tool aiming object
    ToolAimIcon m_aiming;
    void Start()
    {
        Init(new SaveData());
    }
    public void Init(SaveData saved)
    {
        SetToolUnlock(m_torch, true);
        SetToolUnlock(m_specialTorch, saved.SpecialTorchUnlocked);
        SetToolUnlock(m_bat, saved.BatTaken);
    }
    void SetToolUnlock(QuickTool tool, bool isUnlocked) 
    {
        if (isUnlocked) 
        {
            tool.gameObject.SetActive(true);
            tool.OnEnter += OnPointerEnter;
            tool.OnExit += OnPointerExit;
        }
        else 
        {
            tool.OnEnter -= OnPointerEnter;
            tool.OnExit -= OnPointerExit;
            tool.gameObject.SetActive(false);
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
        EToolType toolName = tool.ToolName;
        ToolAimIcon toolAimRef;
        switch (toolName)
        {
            case EToolType.Bat:
                toolAimRef = m_aimIcon;
                break;
            case EToolType.Torch:
                toolAimRef = m_torchAim;
                break;
            case EToolType.SpecialTorch:
                toolAimRef = m_specialTorchAim;
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

