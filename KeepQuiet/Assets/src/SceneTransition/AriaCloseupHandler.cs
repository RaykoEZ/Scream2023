using UnityEngine;
using UnityEngine.Playables;

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
    public void Smug() 
    {
        m_anim.SetTrigger("smug");
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
}
