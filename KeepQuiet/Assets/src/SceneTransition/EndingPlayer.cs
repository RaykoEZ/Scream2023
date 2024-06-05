using Curry.Events;
using System;
using UnityEngine;
using UnityEngine.Playables;
[Serializable]
public enum Ending 
{ 
    Normal_CaseClosed,
    Bad_Delusion, 
    Secret_FreeFromOrbit,
    None
}
// Handle ending sequence after credit roll
// ending must be picked before playing, normal ending sequence by default
public class EndingPlayer : SequencePlayer 
{
    [SerializeField] PlayableAsset m_dismissSeq = default;
    [SerializeField] PlayableAsset m_badEndSeq = default;
    [SerializeField] PlayableAsset m_normalEndSeq = default;
    [SerializeField] PlayableAsset m_secretEndSeq = default;
    PlayableAsset m_endToPlay;
    public override void PlaySequence() 
    {
        if (m_endToPlay == null) 
        {
            Debug.LogWarning("Ending sequebce not set when trying to play an ending.");
            return;
        }
        base.PlaySequence();
    }
    public void DismissSequence() 
    {
        m_endToPlay = m_dismissSeq;
        PlaySequence();
    }
    public void SetEnding(Ending ending)
    {
        switch (ending)
        {
            case Ending.Normal_CaseClosed:
                m_endToPlay = m_normalEndSeq;
                break;
            case Ending.Bad_Delusion:
                m_endToPlay = m_badEndSeq;

                break;
            case Ending.Secret_FreeFromOrbit:
                m_endToPlay = m_secretEndSeq;
                break;
            default:
                m_endToPlay = m_normalEndSeq;
                break;
        }
    }
}
