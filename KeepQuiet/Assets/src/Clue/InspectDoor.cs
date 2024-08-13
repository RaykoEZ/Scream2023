using UnityEngine;
using UnityEngine.UI;

public class InspectDoor : InspectionDisplay
{
    [SerializeField] Image m_door = default;
    [SerializeField] Sprite m_normal = default;
    [SerializeField] Sprite m_bloody = default;
    [SerializeField] Sprite m_dead = default;
    public override void Init(SaveData save) 
    {
        bool deadCheck = save.Persistent.AriaDead;
        Sprite toSet = m_normal;
        if (deadCheck) 
        {
            WatchDisplay time = save.SimulationTime;
            switch (time)
            {
                case WatchDisplay.Present:
                    toSet = m_bloody;
                    break;
                case WatchDisplay.HoursAgo:
                    toSet = m_bloody;
                    break;
                case WatchDisplay.Error:
                    toSet = m_dead;
                    break;
                default:
                    toSet = m_normal;
                    break;
            }
        }
        m_door.sprite = toSet;
    }

}
