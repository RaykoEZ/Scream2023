public class OutsideAriaView : ViewState
{
    public override string Name => "OutsideAria";
    public override void SetVisual(bool isOn)
    {
        if (isOn)
        {
            m_nav.HideAll();
        }
        base.SetVisual(isOn);
    }
}
