using UnityEngine;

public class RoomPoster : Clue 
{
    [SerializeField] InspectPoster m_inspectPoster = default;
    public override InspectionDisplay GetInspectionDisplay(SaveData state)
    {
        return m_inspectPoster;
    }
}

public class Bin : Clue 
{
    [SerializeField] InspectBin m_inspectBin = default;
    public override InspectionDisplay GetInspectionDisplay(SaveData state)
    {
        return m_inspectBin;
    }
}

public class Documents: Clue 
{ 

}