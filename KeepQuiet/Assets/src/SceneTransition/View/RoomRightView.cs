using UnityEngine;
public class RoomRightView : ViewState
{
    public override string Name => "RoomRight";
    protected override void SetVisual(bool isOn)
    {
        if (isOn) 
        {
            m_nav.ToRoomRight();
        }
        base.SetVisual(isOn);
    }
}