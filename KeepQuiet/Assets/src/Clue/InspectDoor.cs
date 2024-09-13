using UnityEngine;
using UnityEngine.UI;

public class InspectDoor : InspectionDisplay
{
    public override void Init(SaveData save) 
    {
        bool deadCheck = save.Persistent.AriaGone;
        if (deadCheck) 
        {
            WatchDisplay time = save.SimulationTime;
            switch (time)
            {
                case WatchDisplay.Present:
                    m_anim?.SetTrigger("blood");
                    break;
                case WatchDisplay.HoursAgo:
                    m_anim?.SetTrigger("blood");
                    break;
                case WatchDisplay.Error:
                    m_anim?.SetTrigger("dead");
                    break;
                default:
                    m_anim?.SetTrigger("normal");
                    break;
            }
        }
        else 
        {
            m_anim?.SetTrigger("normal");
        }
    }
}
