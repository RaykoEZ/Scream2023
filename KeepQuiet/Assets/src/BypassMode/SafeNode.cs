using UnityEngine;
public class SafeNode: BypassNode
{
    public event OnBypassNodeHit OnSuccess;

    protected override void OnHit()
    {
        base.OnHit();
        OnSuccess?.Invoke();
    }

}