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

// Hidden unless game stage triggers reveal event
public class HiddenNode : SafeNode 
{ 
    public virtual void Reveal() 
    { 
    
    }
}