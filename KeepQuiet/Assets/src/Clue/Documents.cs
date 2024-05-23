using UnityEngine;

public class Documents: Clue 
{
    [SerializeField] InspectDocument m_toInspect = default;
    public override InspectionDisplay GetInspectionDisplay(SaveData state)
    {
        return m_toInspect;
    }
}