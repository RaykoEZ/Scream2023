using UnityEngine;
using UnityEngine.UI;
// Toggle for cafe lock
public class CafeLock : MonoBehaviour 
{
    [SerializeField] Animator m_anim = default;
    [SerializeField] Image m_lockImage = default;
    [SerializeField] Sprite m_unlocked = default;
    [SerializeField] Sprite m_locked = default;
    bool m_isDoorUnlocked = false;
    public bool IsDoorUnlocked { get => m_isDoorUnlocked; }
    public void ToggleLock(bool isOn) 
    {
        m_isDoorUnlocked = isOn;
        m_lockImage.sprite = m_isDoorUnlocked ? m_unlocked : m_locked;
    }
    public void Alert() 
    {
        m_anim.SetTrigger("clue_Alert");
    }
}
