using UnityEngine;
using UnityEngine.UI;

public class InspectPoster : InspectionDisplay
{
    [SerializeField] Image m_poster = default;
    [Range(0f, 1f)]
    [SerializeField] float m_scareRate = 0.1f;
    public override void Init(SaveData state)
    {
    }
    public void TryScare() 
    { 
    
    }
    public void RevealCode() 
    { 
        // Reveal hidden code here
    }
}
public class InspectVent : InspectionDisplay
{
    public override void Init(SaveData state)
    {
        throw new System.NotImplementedException();
    }
}
public class InspectDoor : InspectionDisplay
{
    public override void Init(SaveData state)
    {
        throw new System.NotImplementedException();
    }
}
public class InspectClock : InspectionDisplay
{
    public override void Init(SaveData state)
    {
        throw new System.NotImplementedException();
    }
}
public class InspectDocument : InspectionDisplay
{
    public override void Init(SaveData state)
    {
        throw new System.NotImplementedException();
    }
}
public class InspectCans : InspectionDisplay
{
    public override void Init(SaveData state)
    {
        throw new System.NotImplementedException();
    }
}