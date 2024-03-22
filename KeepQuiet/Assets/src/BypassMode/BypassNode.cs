using Curry.Explore;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public delegate void OnBypassNodeHit();
// A class to handle multiple stages of spot-the-odd-one mini-game 
public class BypassManager : MonoBehaviour 
{
    [SerializeField] List<BypassStage> m_stages = default;
    int m_currentStage;
    public void Init(int currentStage)
    {
        m_currentStage = currentStage;
    }
    public void Continue() 
    { 
        if (m_currentStage < 0 && m_currentStage >= m_stages.Count) 
        {
            m_currentStage = 0;
        }
        m_stages[m_currentStage]?.LoadStage();
    }
}

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
