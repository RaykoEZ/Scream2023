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
// Handle ending sequence after credit roll
// ending must be picked before playing, normal ending sequence by default
public class EndingPlayer : SequencePlayer 
{
    [SerializeField] PlayableAsset m_badEndSeq = default;
    [SerializeField] PlayableAsset m_normalEndSeq = default;
    [SerializeField] PlayableAsset m_secretEndSeq = default;
    PlayableAsset m_endToPlay;
    public override void PlaySequence() 
    {
        if (m_endToPlay == null) 
        {
            m_endToPlay = m_normalEndSeq;
        }
        base.PlaySequence();
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
