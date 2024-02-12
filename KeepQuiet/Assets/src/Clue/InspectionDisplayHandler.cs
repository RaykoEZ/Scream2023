using Curry.Explore;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering.Universal;
// Script to instantiate object when player click to inspect a clue
public class InspectionDisplayHandler : HideableUI 
{
    [SerializeField] Transform m_contentParent = default;
    [SerializeField] ToolBarUIAnimationHandler m_toolBar = default;
    [SerializeField] Light2D m_lighting = default;
    InspectionDisplay m_currentlyInspecting;
    bool m_inspecting = false;
    public void InspectTarget(InspectionDisplay toDisplay, GameStateSaveData state) 
    {
        if (m_inspecting || toDisplay == null) return;
        m_inspecting = true;
        InspectionDisplay instance = Instantiate(toDisplay, m_contentParent, false);
        instance.Init(state);
        m_currentlyInspecting = instance;
        Show();
    }
    public void StopInspecting() 
    {
        Hide();
        m_toolBar?.Hide();
        m_inspecting = false;
        Destroy(m_currentlyInspecting);
    }
}