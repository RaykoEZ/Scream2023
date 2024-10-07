using System;
using UnityEngine;
using UnityEngine.Playables;
[Serializable]
public enum Ending 
{ 
    Normal_CaseClosed,
    Bad_Delusion, 
    Secret_Freedom,
    None
}
// Handle ending sequence after credit roll
// ending must be picked before playing, normal ending sequence by default
public class EndingPlayer : SequencePlayer 
{
    [SerializeField] PlayableAsset m_badEnd = default;
    [SerializeField] PlayableAsset m_closeCase = default;
    [SerializeField] PlayableAsset m_secretEndSeq = default;
    public void PlayEnding(Ending ending) 
    {
        var toPlay = GetEnding(ending);
        PlaySequence(toPlay);
    }
    protected PlayableAsset GetEnding(Ending ending)
    {
        switch (ending)
        {
            case Ending.Normal_CaseClosed:
               return m_closeCase;
            case Ending.Bad_Delusion:
                return m_badEnd;
            case Ending.Secret_Freedom:
                return m_secretEndSeq;
            default:
                return m_closeCase;
        }
    }
}
