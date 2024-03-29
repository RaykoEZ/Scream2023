﻿using UnityEngine;
public class AriaDisplayController : MonoBehaviour 
{
    [SerializeField] ViewState m_outsideCam = default;
    [SerializeField] Animator m_cafePoses = default;
    [SerializeField] AriaCloseupHandler m_cafeCloseup = default;
    [SerializeField] Animator m_roomLeftPeek = default;
    [SerializeField] AriaCloseupHandler m_roomLeftCloseup = default;
    AriaPosition m_current = AriaPosition.None;
    public AriaPosition Current => m_current;

    private bool m_isPossessed = false;

    public bool GetIsPossessed()
    {
        return m_isPossessed;
    }
    public void TriggerPossessed(bool value)
    {
        m_isPossessed = value;
        m_roomLeftPeek.ResetTrigger("possessed");
        if (m_isPossessed) 
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

    public void MoveTo(int newLocation) 
    {
        AriaPosition newPos = (AriaPosition) newLocation;
        var prev = Current;
        m_current = newPos;
        MoveTo(Current, prev);
    }
    public void MoveTo(AriaPosition newLocation, AriaPosition previous) 
    {
        Hide(previous);
        switch (newLocation)
        {
            case AriaPosition.OutsideCam:
                m_outsideCam?.OnAriaEnter();
                break;
            case AriaPosition.InsideCafe_Entrance:
                m_cafePoses.ResetTrigger("entrance");
                m_cafePoses.SetTrigger("entrance");
                break;
            case AriaPosition.InsideCafe_Counter:
                m_cafePoses.ResetTrigger("counter");
                m_cafePoses.SetTrigger("counter");
                break;
            case AriaPosition.InsideCafe_Sit:
                m_cafePoses.ResetTrigger("sit");
                m_cafePoses.SetTrigger("sit");
                break;
            case AriaPosition.InsideCafe_Closeup:
                m_cafeCloseup.EnterScene();
                break;
            case AriaPosition.RoomLeft_Peeking:
                m_roomLeftPeek.ResetTrigger("peek");
                m_roomLeftPeek.SetTrigger("peek");
                break;
            case AriaPosition.RoomLeft_CloseUp:
                m_roomLeftCloseup.EnterScene();
                break;
            default:
                break;
        }
    }
    void Hide(AriaPosition toHide) 
    {
        switch (toHide)
        {
            case AriaPosition.OutsideCam:
                m_outsideCam?.OnAriaExit();
                break;
            case AriaPosition.InsideCafe_Entrance:
                m_cafePoses.ResetTrigger("hide");
                m_cafePoses.SetTrigger("hide");
                break;
            case AriaPosition.InsideCafe_Counter:
                m_cafePoses.ResetTrigger("hide");
                m_cafePoses.SetTrigger("hide");
                break;
            case AriaPosition.InsideCafe_Sit:
                m_cafePoses.ResetTrigger("hide");
                m_cafePoses.SetTrigger("hide");
                break;
            case AriaPosition.InsideCafe_Closeup:
                m_cafeCloseup.ExitScene();
                break;
            case AriaPosition.RoomLeft_Peeking:
                m_roomLeftPeek.ResetTrigger("exit");
                m_roomLeftPeek.SetTrigger("exit");
                break;
            case AriaPosition.RoomLeft_CloseUp:
                m_roomLeftCloseup.ExitScene();
                break;
            default:
                break;
        }
    }
    public void HideCurrent() 
    {
        Hide(Current);
    }
}
