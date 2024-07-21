using UnityEngine;

public class ThoughtGainSource : MonoBehaviour
{
    ThoughtDetail m_toGain;
    private bool m_obtainable = false;
    public ThoughtDetail DetailToGain => m_toGain;
    public bool Obtainable { get => m_obtainable; private set => m_obtainable = value; }
    public void Init(ThoughtDetail detail)
    {
        m_toGain = detail;
        Obtainable = true;
    }
    public void Shutdown()
    {
        Obtainable = false;
    }
}
