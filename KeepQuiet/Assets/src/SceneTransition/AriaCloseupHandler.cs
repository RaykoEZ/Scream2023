using UnityEngine;
public enum MouthState 
{ 
    Closed = 0,
    Open = 1,
    Half = 2
}
public class AriaCloseupHandler : MonoBehaviour 
{
    [SerializeField] Animator m_anim = default;
    public void EnterScene() 
    {
        m_anim.SetTrigger("enter");

    }
    public void ExitScene() 
    {
        m_anim.SetTrigger("exit");
    }
    public void Curious() 
    {
        m_anim.SetTrigger("curious");
    }
    public void Angry() 
    {
        m_anim.SetTrigger("angry");
    }
    public void Smug(int mouthState) 
    {
        Smug((MouthState)mouthState);
    }
    public void Smug(MouthState mouth) 
    {
        switch (mouth)
        {
            case MouthState.Closed:
                m_anim.SetTrigger("smugClose");
                break;
            case MouthState.Open:
                m_anim.SetTrigger("smugOpen");
                break;
            case MouthState.Half:
                m_anim.SetTrigger("smugHalf");
                break;
            default:
                break;
        }
    }
    public void Scary() 
    {
        m_anim.SetTrigger("scary");
    }
    public void ScaryShaded() 
    {
        m_anim.SetTrigger("scaryDark");
    }
    public void Shadow() 
    {
        m_anim.SetTrigger("hideScary");
    }
    public void LaughingShadow() 
    {
        m_anim.SetTrigger("laughShadow");
    }
    public void BloodyShadow() 
    {
        m_anim.SetTrigger("bloodShadow");
    }
}
