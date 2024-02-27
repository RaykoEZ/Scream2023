using System;
using UnityEngine;
// Handles Aria's internal states i.e. sanity, movement, affection, memory and dialogue options
[Serializable]
public class AriaStateManager : MonoBehaviour
{
    [SerializeField] AriaDisplayController m_position = default;
    AriaState m_current;
    public AriaState Current => m_current;
    public void InitState(SaveData change) 
    {
        m_current = change.AriaStatus;
        m_position?.HideAll();
        m_position?.MoveTo(change.AriaStatus.CurrentLocation, AriaPosition.None);
    }
    public void AffectionDown(int val)
    {
        Current.Affection -= val;
        if (Current.Affection < -10)
        {
            // If affecton is low, leave the player
            Hide();
        }
    }
    public void OnDenied()
    {
        AffectionDown(1);
    }
    public void Hide()
    {
        m_position.Hide(Current.CurrentLocation);
        Current.CurrentLocation = AriaPosition.None;
    }
    public void MoveTo(AriaPosition newLocation)
    {
        AriaPosition prev = Current.CurrentLocation;
        Current.CurrentLocation = newLocation;
        m_position.MoveTo(newLocation, prev);
    }
    public void MoveTo(int newLocation)
    {
        AriaPosition newPos = (AriaPosition)newLocation;
        var prev = m_current.CurrentLocation;
        m_current.CurrentLocation = newPos;
        m_position.MoveTo(newPos, prev);
    }
    public void TriggerPossessed(bool value)
    {
        m_current.IsPossessed = value;
        m_position.TriggerPossessed(value);
    }
    public void TriggerSurprise()
    {
        m_position.TriggerSurprise();
    }
}
