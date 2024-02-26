using Curry.Explore;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering.Universal;
// Script to instantiate object when player click to inspect a clue
public class InspectionDisplayHandler : HideableUI 
{
    [SerializeField] Transform m_contentParent = default;
    [SerializeField] ToolBarUIAnimationHandler m_toolBar = default;
    [SerializeField] HideableUITrigger m_inspectUITrigger = default;
    InspectionDisplay m_currentlyInspecting;
    bool m_inspecting = false;
    public void InspectTarget(InspectionDisplay toDisplay, SaveData state) 
    {
        if (m_inspecting || toDisplay == null) return;
        m_inspecting = true;
        InspectionDisplay instance = Instantiate(toDisplay, m_contentParent, false);
        instance.Init(state);
        m_currentlyInspecting = instance;
        m_inspectUITrigger?.Show();
    }
    public void StopInspecting() 
    {
        Destroy(m_currentlyInspecting.gameObject);
        m_inspectUITrigger?.Hide();
        m_toolBar?.Hide();
        m_inspecting = false;
    }
}