public class InsideCafeView : ViewState 
{
    public override string Name => "InsideCafe";
    public override void SetVisual(bool isOn)
    {
        if (isOn)
        {
            m_nav.ToCameraCafe();
        }
        base.SetVisual(isOn);
    }
}
