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
    [SerializeField] Animator m_doorControl = default;
    // All switchable clues in this view
    [SerializeField] Clue m_clock = default;
    public override string Name => "RoomLeft";
    private DoorState m_doorState = DoorState.Closed;
    public DoorState DoorState => m_doorState;
    protected override void InitStateInternal(SaveData saveData)
    {
        // Set door state
        ChangeDoorState(DoorState.Closed);
        base.InitStateInternal(saveData);
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
        m_doorControl.SetTrigger(stateName);
    }
    protected override void SetVisual(bool isOn)
    {
        if (isOn)
        {
            m_nav.ToRoomLeft();
        }
        base.SetVisual(isOn);
    }
}
