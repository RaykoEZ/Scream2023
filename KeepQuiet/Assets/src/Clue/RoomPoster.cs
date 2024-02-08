using UnityEngine;

public class RoomPoster : Clue 
{
    [SerializeField] InspectPoster m_inspectPoster = default;
    public override InspectionDisplay GetInspectionDisplay(GameStateSaveData state)
    {
        return m_inspectPoster;
    }
}
