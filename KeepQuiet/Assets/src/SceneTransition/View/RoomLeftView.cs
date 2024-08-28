using UnityEngine;

public class RoomLeftView : ViewState 
{
    [SerializeField] Animator m_roomControl = default;
    // All switchable clues in this view
    public override string Name => "RoomLeft";
    public bool IsDoorOn { get; set; }
    protected override void InitStateInternal(SaveData saveData)
    {
        // Set door state
        base.InitStateInternal(saveData);
    }
    // Change door state and trheir visuals
    public void UpdateDoorState(bool isOn) 
    {
        string stateName = isOn ? "close" : "noDoor";
        m_roomControl.SetTrigger(stateName);
    }
    public void SetDeadBody(bool isDead) 
    {
        m_roomControl.SetBool("dead",isDead);
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
