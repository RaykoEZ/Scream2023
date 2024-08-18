using Curry.Events;
using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public delegate void OnToolUnlock();
public class ToolInteractionHandler : MonoBehaviour
{
    [SerializeField] ToolBarUIAnimationHandler m_anim = default;
    [SerializeField] ToolAimIcon m_torchAim = default;
    [SerializeField] ToolAimIcon m_specialTorchAim = default;
    //TODO:Coat hanger object, draggable and modifiable
    [SerializeField] QuickTool m_torch = default;
    [SerializeField] QuickTool m_specialTorch = default;
    // Allow RMB to return tool from use state
    [SerializeField] InputActionReference m_mouseClickToReturnTool = default;
    [SerializeField] CurryGameEventTrigger m_onSpecialTorch = default;
    // tool we are currently using
    QuickTool m_using;
    // the current tool aiming object
    ToolAimIcon m_aiming;
    void Start()
    {
        Init();
    }
    public void Init()
    {
        SetToolUnlock(m_torch, true);
        SetToolUnlock(m_specialTorch, true);
    }
    public void SetToolUnlock(QuickTool tool, bool isUnlocked) 
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
        m_aiming?.HideCursor();
        m_anim?.Show();
    }
    public void ReturnTool() 
    {
        if (m_using.ToolName == EToolType.SpecialTorch) 
        {
            OnSpecialTorch(false);
        }
        // disable input action for returning tool
        m_mouseClickToReturnTool.action.performed -= OnReturnTool;
        m_using?.OnReturnTool();
        ReturnTool(m_using);
    }
    // handles PMB input to return tool from using state
    void OnReturnTool(CallbackContext callback) 
    {
        ReturnTool();
    }
    public void UseTool(QuickTool tool) 
    {
        if (tool == null) return;
        EToolType toolName = tool.ToolName;
        ToolAimIcon toolAimRef;
        switch (toolName)
        {
            case EToolType.Torch:
                toolAimRef = m_torchAim;
                break;
            case EToolType.SpecialTorch:
                toolAimRef = m_specialTorchAim;
                OnSpecialTorch(true);
                break;
            default:
                return;
        }
        m_mouseClickToReturnTool.action.performed += OnReturnTool;
        m_aiming = toolAimRef;
        m_using = tool;
        m_aiming?.ShowCursor();
        m_anim?.Hide();
    }

    void OnSpecialTorch(bool isOn = true) 
    {
        var payload = new Dictionary<string, object> { { "isOn", isOn} };
        EventInfo info = new EventInfo(payload);
        m_onSpecialTorch?.TriggerEvent(info);
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

