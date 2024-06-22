using Curry.Events;
using Curry.Explore;
using System.Collections;
using UnityEngine;
// Script to instantiate object when player click to inspect a clue
public class InspectionDisplayHandler : HideableUI 
{
    [SerializeField] GameStateManager m_state = default;
    [SerializeField] Transform m_contentParent = default;
    [SerializeField] ToolBarUIAnimationHandler m_toolBar = default;
    [SerializeField] HideableUITrigger m_inspectUITrigger = default;
    InspectionDisplay m_currentlyInspecting;
    bool m_inspecting = false;
    public void InspectTarget(Clue toDisplay) 
    {
        var save = m_state.CurrentGameState;
        var display = toDisplay?.GetInspectionDisplay(save);
        if (m_inspecting || toDisplay == null || display == null) return;
        m_inspecting = true;
        m_currentlyInspecting = display;
        m_currentlyInspecting.gameObject.SetActive(true);
        m_currentlyInspecting?.Init(save);
        m_inspectUITrigger?.Show();
    }
    public void StopInspecting() 
    {
        StartCoroutine(Stop_Internal());
    }
    IEnumerator Stop_Internal() 
    {
        yield return m_currentlyInspecting?.OnExit();
        yield return new WaitForEndOfFrame();
        m_currentlyInspecting.gameObject.SetActive(false);
        m_inspectUITrigger?.Hide();
        m_toolBar?.Hide();
        m_inspecting = false;
    }
}