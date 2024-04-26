using Curry.Events;
using System;
using UnityEngine;
using UnityEngine.Playables;
[Serializable]
public enum Ending 
{ 
    Normal,
    Bad, 
    Secret,
    None
}
//Handle ending sequence after credit roll
public class EndingHandler : MonoBehaviour 
{
    [SerializeField] PlayableDirector m_endingDirector = default;
    [SerializeField] PlayableAsset m_badEndSeq = default;
    [SerializeField] PlayableAsset m_normalEndSeq = default;
    [SerializeField] PlayableAsset m_secretEndSeq = default;
    PlayableAsset m_endToPlay;
    public void PlayEnding() 
    {
        if (m_endToPlay == null) 
        {
            m_endToPlay = m_normalEndSeq;
        }
        m_endingDirector.Play(m_endToPlay);
    }
    public void SetEnding(Ending ending)
    {
        switch (ending)
        {
            case Ending.Normal:
                m_endToPlay = m_normalEndSeq;
                break;
            case Ending.Bad:
                m_endToPlay = m_badEndSeq;

                break;
            case Ending.Secret:
                m_endToPlay = m_secretEndSeq;
                break;
            default:
                m_endToPlay = m_normalEndSeq;
                break;
        }
    }
}
