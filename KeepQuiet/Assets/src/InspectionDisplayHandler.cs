using UnityEngine;
// Script to instantiate object when player click to inspect a clue
public class InspectionDisplayHandler : MonoBehaviour 
{
    InspectionDisplay m_currentlyInspecting;
    bool m_inspecting = false;
    public void InspectTarget(InspectionDisplay toDisplay, GameStateSaveData state) 
    {
        if (m_inspecting || toDisplay == null) return;

        m_inspecting = true;
        InspectionDisplay instance = Instantiate(toDisplay, transform, false);
        instance.Init(state);
        m_currentlyInspecting = instance;
    }
}