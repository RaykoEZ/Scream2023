using UnityEngine;
public enum MouthState 
{ 
    Closed,
    Open,
    Half
}
public class AriaCloseupHandler : MonoBehaviour 
{
    [SerializeField] Animator m_anim = default;
    public void EnterScene() { }
    public void ExitScene() { }
    public void Curious() { }
    public void Angry() { }

    public void Smug(MouthState mouth) 
    {
        switch (mouth)
        {
            case MouthState.Closed:
                break;
            case MouthState.Open:
                break;
            case MouthState.Half:
                break;
            default:
                break;
        }
    }
    public void Scary() 
    { 
    
    }
    public void ScaryShaded() 
    { 
    
    }
    public void Shadow() 
    { 
    
    }
    public void LaughingShadow() 
    { 
    
    }
    public void BloodyShadow() 
    { 
    
    }
}
