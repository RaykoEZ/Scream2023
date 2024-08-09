using UnityEngine;
public class OutsideCameraView : ViewState
{
    [SerializeField] AnimationLooper m_blink = default;
    [SerializeField] Animator m_anim = default;
    [SerializeField] AnimationClip m_noAria = default;
    public override string Name => "OutsideCamera";
    protected override void InitStateInternal(SaveData gamestate)
    {
        if (gamestate.AriaStatus.CurrentLocation == AriaPosition.Outside) 
        {
            OnAriaEnter();
        } 
        else
        {
            OnAriaExit();
        }
        base.InitStateInternal(gamestate);
    }
    protected override void SetVisual(bool isOn)
    {
        if (isOn)
        {
            m_nav.ToCameraOutside();
        }
        base.SetVisual(isOn);
    }
    public override void OnAriaEnter() 
    {
        m_anim.StopPlayback();
        m_blink.BeginLoop();
    }
    public override void OnAriaExit()
    {
        m_blink.StopLoop();
        m_anim.Play(m_noAria.name);
    }
}
