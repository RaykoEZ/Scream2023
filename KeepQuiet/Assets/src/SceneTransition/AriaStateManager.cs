using System;
using UnityEngine;
// Handles Aria's internal states i.e. sanity, movement, affection, memory and dialogue options
[Serializable]
public class AriaStateManager
{
    [SerializeField] AriaDisplayController m_position = default;
    AriaState m_current;
    public AriaState Current => m_current;
    public void InitState(GameStateSaveData change) 
    {
        m_current = change.AriaStatus;
    }
    public void AffectionDown(int val)
    {
        Current.Affection -= val;
        if (Current.Affection < -10)
        {
            // If affecton is low, leave the player
            Leave();
        }
    }
    public void OnDenied()
    {
        Current.NumDenied++;
        if (Current.NumDenied > 3)
        {
            AffectionDown(1);
        }
    }
    public void Leave()
    {
        AriaPosition prev = Current.CurrentLocation;
        Current.CurrentLocation = AriaPosition.None;
        m_position.MoveTo(AriaPosition.None, prev);
    }
    public void MoveTo(AriaPosition newLocation)
    {
        AriaPosition prev = Current.CurrentLocation;
        Current.CurrentLocation = newLocation;
        m_position.MoveTo(newLocation, prev);
    }
}
