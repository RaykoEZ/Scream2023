using UnityEngine;
public class OutsideCameraView : ViewState
{
    [SerializeField] AnimationLooper m_blink = default;
    [SerializeField] Animator m_anim = default;
    [SerializeField] AnimationClip m_noAria = default;
    public override string Name => "OutsideCamera";
    protected override void InitStateInternal(GameStateSaveData gamestate, ViewStateSaveData selfState)
    {
        base.InitStateInternal(gamestate, selfState);
        if (gamestate.AriaStatus.CurrentLocation == AriaPosition.OutsideCam) 
        {
            OnAriaEnter();
        } 
        else
        {
            OnAriaExit();
        }
    }
    public override void SetVisual(bool isOn)
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
