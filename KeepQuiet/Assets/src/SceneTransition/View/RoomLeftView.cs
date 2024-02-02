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
    [SerializeField] Clue m_bat = default;
    public override string Name => "RoomLeft";
    private DoorState m_doorState = DoorState.Closed;
    public DoorState DoorState => m_doorState;
    protected override void InitStateInternal(GameStateSaveData saveData, ViewStateSaveData selfState)
    {
        base.InitStateInternal(saveData, selfState);
        // Set door state
        ChangeDoorState(saveData.RoomLeftDoorState);
        // hide clues?
        if (selfState.CluesToHide.Contains(m_clock.name))
        {
            m_clock.Hide();
        }
        if (selfState.CluesToHide.Contains(m_bat.name))
        {
            m_bat.Hide();
        }
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
    public override void SetVisual(bool isOn)
    {
        if (isOn)
        {
            m_nav.ToRoomLeft();
        }
        base.SetVisual(isOn);
    }
}
