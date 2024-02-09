using Curry.Explore;
using UnityEditor.UIElements;
using UnityEngine;
// Script to instantiate object when player click to inspect a clue
public class InspectionDisplayHandler : HideableUI 
{
    [SerializeField] Transform m_contentParent = default;
    [SerializeField] HideableUI m_toolToggle = default;
    [SerializeField] ToolBarUIAnimationHandler m_toolBar = default;
    InspectionDisplay m_currentlyInspecting;
    bool m_inspecting = false;
    public void InspectTarget(InspectionDisplay toDisplay, GameStateSaveData state) 
    {
        if (m_inspecting || toDisplay == null) return;
        m_toolToggle?.Show();
        m_inspecting = true;
        InspectionDisplay instance = Instantiate(toDisplay, m_contentParent, false);
        instance.Init(state);
        m_currentlyInspecting = instance;
        Show();
    }
    public void StopInspecting() 
    {
        Hide();
        m_toolToggle?.Hide();
        m_toolBar?.Hide();
        m_inspecting = false;
        Destroy(m_currentlyInspecting);
    }
}