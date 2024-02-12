using UnityEngine;
using UnityEngine.UI;

public class InspectPoster : InspectionDisplay
{
    [SerializeField] Image m_poster = default;
    [SerializeField] Image m_hiddenCode = default;
    [SerializeField] Sprite m_defaultPosterImage = default;
    [SerializeField] Sprite m_glitchPosterImage = default;
    public override void Init(GameStateSaveData state)
    {
        m_poster.sprite = state.CrashCount > 0? m_glitchPosterImage : m_defaultPosterImage;
    }
    public void RevealCode() 
    { 
        // Reveal hidden code here
    }
}
public class InspectVent : InspectionDisplay
{
    public override void Init(GameStateSaveData state)
    {
        throw new System.NotImplementedException();
    }
}
public class InspectDoor : InspectionDisplay
{
    public override void Init(GameStateSaveData state)
    {
        throw new System.NotImplementedException();
    }
}
public class InspectClock : InspectionDisplay
{
    public override void Init(GameStateSaveData state)
    {
        throw new System.NotImplementedException();
    }
}
public class InspectDocument : InspectionDisplay
{
    public override void Init(GameStateSaveData state)
    {
        throw new System.NotImplementedException();
    }
}
public class InspectCans : InspectionDisplay
{
    public override void Init(GameStateSaveData state)
    {
        throw new System.NotImplementedException();
    }
}