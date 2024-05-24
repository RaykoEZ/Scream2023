using UnityEngine;

public class RoomPoster : Clue 
{
    [SerializeField] InspectPoster m_inspectPoster = default;
    public override InspectionDisplay GetInspectionDisplay(SaveData state)
    {
        return m_inspectPoster;
    }
}
