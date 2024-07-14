public class InsideCafeView : ViewState 
{
    public override string Name => "InsideCafe";
    protected override void SetVisual(bool isOn)
    {
        if (isOn)
        {
            m_nav.ToCameraCafe();
        }
        base.SetVisual(isOn);
    }
}
