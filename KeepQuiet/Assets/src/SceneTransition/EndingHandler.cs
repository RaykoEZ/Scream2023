using Curry.Events;
using UnityEngine;
using UnityEngine.Playables;
//Handle ending sequence after credit roll
public class EndingHandler : MonoBehaviour 
{
    [SerializeField] PlayableDirector m_endingDirector = default;
    [SerializeField] PlayableAsset m_badEndSeq = default;
    [SerializeField] PlayableAsset m_normalEndSeq = default;
    [SerializeField] PlayableAsset m_secretEndSeq = default;
    [SerializeField] CurryGameEventListener m_badEnd = default;
    [SerializeField] CurryGameEventListener m_normalEnd = default;
    [SerializeField] CurryGameEventListener m_secretEnd = default;
    public void BadEnd()
    {
        m_endingDirector.Play(m_badEndSeq);
    }

    public void NormalEnd()
    {
        m_endingDirector.Play(m_normalEndSeq);
    }
    public void SecretEnd()
    {
        m_endingDirector.Play(m_secretEndSeq);
    }
}
