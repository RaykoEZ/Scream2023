using Curry.Explore;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void OnBypassNodeHit();
public class BypassNode : HideableUI, 
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool m_pressable = default;
    [SerializeField] bool m_isHidden = default;
    [SerializeField] protected AudioSource m_hitAudio = default;
    [SerializeField] protected Image m_image = default;
    bool m_isSafe = false;
    public event OnBypassNodeHit OnSuccess;
    public event OnBypassNodeHit OnFail;

    public bool IsSafe => m_isSafe;
    public void SetSafe(bool isSafe) 
    {
        m_isSafe = isSafe;
        GetAnim?.SetBool("IsSafe", m_isSafe);
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (m_pressable) 
        {
            OnHit();
        }
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }
    public virtual void Init()
    {
        if (m_isHidden) 
        {
            Hide();
        } 
        else
        {
            Show();
        }
    }
    protected virtual void OnHit() 
    {
        GetAnim.ResetTrigger("Hit");
        GetAnim.SetTrigger("Hit");
        m_hitAudio?.Play();
        if (m_isSafe) 
        {
            OnSuccess?.Invoke();
        }
        else 
        {
            OnFail?.Invoke();
        }
    }
}
