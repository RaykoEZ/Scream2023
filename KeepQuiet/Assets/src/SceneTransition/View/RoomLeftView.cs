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
    // All switchable clues in this view
    [SerializeField] Clue m_clock = default;
    [SerializeField] Clue m_door = default;
    [SerializeField] Clue m_bat = default;
    public override string Name => "RoomLeft";
    private DoorState m_doorState = DoorState.Closed;
    public DoorState DoorState => m_doorState;
    public void SetViewDarkness(bool isLit) 
    {
        IsLit = isLit;
        m_darkness.alpha = IsLit ? 0f : 1f;
    }
    protected override void InitStateInternal(GameStateSaveData saveData, ViewStateSaveData viewState)
    {
        base.InitStateInternal(saveData, viewState);
        // darkness adjust
        SetViewDarkness(viewState.IsLit);
        // Set door state
        ChangeDoorState(saveData.RoomLeftDoorState);
        // hide clues?
        if (viewState.CluesToHide.Contains(m_clock.name))
        {
            m_clock.Hide();
        }
        if (viewState.CluesToHide.Contains(m_door.name))
        {
            m_door.Hide();
        }
        if (viewState.CluesToHide.Contains(m_bat.name))
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
}
