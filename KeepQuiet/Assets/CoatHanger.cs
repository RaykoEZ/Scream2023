using Curry.Explore;
using UnityEngine;

public enum ECoatHangerState 
{
    FreeForm = 0,
    Hook = 1,
}
public class CoatHanger : ToolAimIcon
{
    [SerializeField] DragRotationHandler m_hook = default;
    [SerializeField] DragRotationHandler m_left = default;
    [Range(-180f, 180f)]
    [SerializeField] float m_hookAngleThreshold = default;
    [Range(-180f, 180f)]
    [SerializeField] float m_leftAngleThreshold = default;
    [Range(0, 30f)]
    [SerializeField] float m_thresholdMargin = default;
    ECoatHangerState m_state = ECoatHangerState.FreeForm;
    private FloatRange m_hookAngleRange;
    private FloatRange m_leftAngleRange;
    public ECoatHangerState ToolState => m_state;
    public FloatRange HookAngleRange => m_hookAngleRange; 
    public FloatRange LeftAngleRange => m_leftAngleRange; 
    private void Start()
    {
        m_hookAngleRange = GameUtil.SignedAngleThresholdRange(m_hookAngleThreshold, m_thresholdMargin);
        m_leftAngleRange = GameUtil.SignedAngleThresholdRange(m_leftAngleThreshold, m_thresholdMargin);
    }
    protected override void Update() { }
    public override void ShowCursor()
    {
        Show();
    }
    public override void HideCursor()
    {
        Hide();
    }
    // Update tool drag box animator and shape in animator
    public void UpdateToolState(ECoatHangerState newState)
    {
        m_state = newState;
        AnimateTool();
    }
    // check hook & left rotation, transform into a new tool if both threshold reached
    public void OnHangerModified()
    {
        ECoatHangerState newState = CheckRotationThresholds();
        if (m_state == newState)
        {
            return;
        }
        m_state = newState;
        // snap to rotation states, control with animator
        AnimateTool();
    }
    void AnimateTool() 
    {
        // snap to rotation states, control with animator
        switch (m_state)
        {
            case ECoatHangerState.FreeForm:
                GetAnim?.SetTrigger("Free");
                break;
            case ECoatHangerState.Hook:
                GetAnim?.SetTrigger("Hook");
                break;
            default:
                GetAnim?.SetTrigger("Free");
                break;
        }
    }
    ECoatHangerState CheckRotationThresholds()
    {
        float hook = m_hook.CurrentAngle;
        float left = m_left.CurrentAngle;
        bool hookCheck = hook <= HookAngleRange.Max && hook >= HookAngleRange.Min;
        bool leftCheck = left <= LeftAngleRange.Max && left >= LeftAngleRange.Min;
        Debug.Log($" {HookAngleRange.Min} <= {hook} <= {HookAngleRange.Max}");
        Debug.Log($" {LeftAngleRange.Min} <= {left} <= {LeftAngleRange.Max}");

        ECoatHangerState result = ECoatHangerState.FreeForm;
        if (hookCheck && leftCheck) 
        {
            result = ECoatHangerState.Hook;
        }
        Debug.Log(result);
        return result;
    }
    public void ResetHangerState()
    {
        UpdateToolState(ECoatHangerState.FreeForm);
    }
}
