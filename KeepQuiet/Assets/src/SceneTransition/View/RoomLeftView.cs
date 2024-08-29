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
        SetDeadBody(saveData.Persistent.AriaDead);
    }
    protected override void SetVisual(bool isOn)
    {
        base.SetVisual(isOn);
        if (isOn) 
        {
            UpdateDoorState(isOn);
        }
    }
    // Change door state and trheir visuals
    public void UpdateDoorState(bool isDoorOn) 
    {
        string stateName = isDoorOn ? "close" : "noDoor";
        m_roomControl.SetTrigger(stateName);
    }
    public void SetDeadBody(bool isDead) 
    {
        m_roomControl.SetBool("dead", isDead);
    }
}
