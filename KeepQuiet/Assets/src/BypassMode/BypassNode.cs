using Curry.Explore;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public delegate void OnBypassNodeHit();

public abstract class BypassNode : HideableUI, 
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected SpriteRenderer m_sprite = default;
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnHit();
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }
    public virtual void Init()
    {
        Show();
    }
    protected virtual void OnHit() 
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
    public event OnBypassNodeHit OnFail;
    protected override void OnHit() 
    {
        base.OnHit();
        OnFail?.Invoke();
    }

}
