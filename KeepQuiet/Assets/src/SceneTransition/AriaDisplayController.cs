using System;
using UnityEngine;
// display aria's position in cafe
[Serializable]
public class AriaDisplayController 
{
    [SerializeField] Animator m_cafePoses = default;
    [SerializeField] Animator m_roomLeftPeek = default;
    public void TriggerPossessed(bool value)
    {
        m_roomLeftPeek.ResetTrigger("possessed");
        if (value) 
        {
            m_roomLeftPeek.SetTrigger("possessed");
        }
        else 
        {
            m_roomLeftPeek.SetTrigger("exit");
        }
    }
    public void TriggerSurprise()
    {
        m_roomLeftPeek.ResetTrigger("surprise");
        m_roomLeftPeek.SetTrigger("surprise");
    }
    public void PeekIntoRoom() 
    {
        if (m_roomLeftPeek.isActiveAndEnabled)
        {
            m_roomLeftPeek.SetTrigger("peek");
        }
    }
    public void MoveTo(AriaPosition newLocation, AriaPosition previous) 
    {
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
