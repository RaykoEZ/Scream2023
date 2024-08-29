using System;
using UnityEngine;
// display aria's position in cafe
[Serializable]
public class AriaDisplayController 
{
    [SerializeField] Animator m_cafePoses = default;
    public void MoveTo(AriaPosition newLocation, AriaPosition previous) 
    {
        if (!m_cafePoses.isActiveAndEnabled) return;
        switch (newLocation)
        {
            case AriaPosition.InsideCafe_Counter:
                if (m_cafePoses.isActiveAndEnabled)
                {
                    m_cafePoses.SetTrigger("counter");
                }
                break;
            case AriaPosition.InsideCafe_Sit:
                if (m_cafePoses.isActiveAndEnabled)
                {
                    m_cafePoses.SetTrigger("sit");
                }
                break;
            default:
                Hide();
                break;
        }
    }
    public void Hide() 
    {
        m_cafePoses.SetTrigger("hide");
    }
}
