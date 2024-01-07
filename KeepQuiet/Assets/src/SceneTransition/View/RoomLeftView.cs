using UnityEngine;
public enum DoorState 
{ 
    Open,
    Closed,
    SlightlyOpen,
    NoDoor
}
public class RoomLeftView : ViewState 
{
    // Controls doors, light/dark
    [SerializeField] CanvasGroup m_darkness = default;
    [SerializeField] Animator m_doorControl = default;
    // All possible clues in this view
    [SerializeField] Clue m_calendar = default;
    [SerializeField] Clue m_clock = default;
    [SerializeField] Clue m_door = default;
    [SerializeField] Clue m_doorLight = default;
    [SerializeField] Clue m_bat = default;
    [SerializeField] Clue m_vent = default;
    [SerializeField] Clue m_screen = default;
    public override string Name => "RoomLeft";
    private DoorState m_doorState = DoorState.Closed;
    public DoorState DoorState => m_doorState;
    public void SetViewDarkness(bool isLit) 
    {
        m_darkness.alpha = isLit ? 0f : 1f;
    }
    // Change door state and trheir visuals
    public void ChangeDoorState(DoorState newState) 
    {
        m_doorState = newState;
        string stateName = "noDoor";
        switch (m_doorState)
        {
            case DoorState.Open:
                stateName = "open";
                break;
            case DoorState.Closed:
                stateName = "close";
                break;
            case DoorState.SlightlyOpen:
                stateName = "slightlyOpen";
                break;
            default:
                break;
        }
        m_doorControl.ResetTrigger(stateName);
        m_doorControl.SetTrigger(stateName);
    }
}
