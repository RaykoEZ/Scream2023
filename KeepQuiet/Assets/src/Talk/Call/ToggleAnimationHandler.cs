using UnityEngine;

public class ToggleAnimationHandler : MonoBehaviour 
{
    [SerializeField] Animator m_toggleAnim = default;
    public void HideToggle()
    {
        if (!m_toggleAnim.GetBool("Alert"))
        {
            m_toggleAnim.SetBool("Show", false);
        }
    }
    public void ShowToggle()
    {
        m_toggleAnim.SetBool("Show", true);
    }
    public void AnimateAlertIcon(bool isOn)
    {
        // Animate Toggle Icon here
        m_toggleAnim.SetBool("Alert", isOn);
    }
    public void AlertOff()
    {
        // Animate Toggle Icon here
        m_toggleAnim.SetBool("Alert", false);
    }
}
