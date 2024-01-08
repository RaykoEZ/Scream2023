using UnityEngine;
public class RoomRightView : ViewState
{
    [SerializeField] CanvasGroup m_darkness = default;
    public override string Name => "RoomRight";
    public void SetViewDarkness(bool isLit)
    {
        IsLit = isLit;
        m_darkness.alpha = IsLit ? 0f : 1f;
    }
    protected override void InitStateInternal(GameStateSaveData saveData, ViewStateSaveData viewState)
    {
        // darkness adjust
        SetViewDarkness(viewState.IsLit);
    }
}