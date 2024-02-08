using UnityEngine;
// Script to instantiate object when player click to inspect a clue
public class InspectionDisplayHandler : MonoBehaviour 
{
    [SerializeField] Transform m_contentParent = default;
    InspectionDisplay m_currentlyInspecting;
    bool m_inspecting = false;
    public void InspectTarget(InspectionDisplay toDisplay, GameStateSaveData state) 
    {
        if (m_inspecting || toDisplay == null) return;

        m_inspecting = true;
        InspectionDisplay instance = Instantiate(toDisplay, m_contentParent, false);
        instance.Init(state);
        m_currentlyInspecting = instance;
    }
    public void StopInspecting() 
    {
        m_inspecting = false;
        Destroy(m_currentlyInspecting);
    }
}