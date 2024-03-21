using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BypassNode : HideableUI
{
    [SerializeField] protected SpriteRenderer m_sprite = default;
    public virtual void OnHit() 
    {
        GetAnim.ResetTrigger("Hit");
        GetAnim.SetTrigger("Hit");
    }
    protected virtual void Flash()
    {
        GetAnim.ResetTrigger("Flash");
        GetAnim.SetTrigger("Flash");
    }
    protected virtual void SolidLight() 
    {
        GetAnim.ResetTrigger("Solid");
        GetAnim.SetTrigger("Solid");
    }
}
public class MissNode : BypassNode 
{
    public override void OnHit() 
    {
        base.OnHit();
    }

}
