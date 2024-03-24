using UnityEngine;
public class MissNode : BypassNode
{
    public event OnBypassNodeHit OnFail;
    protected override void OnHit() 
    {
        base.OnHit();
        OnFail?.Invoke();
    }

}
