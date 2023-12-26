using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class ViewTransitionHandler : MonoBehaviour
{
    [SerializeField] protected LightingHandler m_lighting = default;
    // Use this for initialization
    public void Activate()
    {
        m_lighting?.Activate();
    }

    public void Deactivate() 
    {
        m_lighting?.Deactivate();

    }

}

public abstract class LightingHandler : MonoBehaviour
{
    public abstract void Activate();
    public abstract void Deactivate();
}
