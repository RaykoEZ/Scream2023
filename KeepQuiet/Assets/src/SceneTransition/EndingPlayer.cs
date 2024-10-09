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
    // ending sequence before credits
    [SerializeField] PlayableAsset m_bad = default;
    [SerializeField] PlayableAsset m_normal = default;
    [SerializeField] PlayableAsset m_secret = default;
    // play after credit
    [SerializeField] PlayableAsset m_badPostCredit = default;
    [SerializeField] PlayableAsset m_normalPostCredit = default;
    [SerializeField] PlayableAsset m_secretPostCredit = default;
    public void PlayEnding(Ending ending) 
    {
        var toPlay = GetEnding(ending);
        PlaySequence(toPlay);
    }
    public void PostCredit(Ending ending) 
    {
        var toPlay = GetPostCredit(ending);
        PlaySequence(toPlay);
    }
    protected PlayableAsset GetPostCredit(Ending ending)
    {
        switch (ending)
        {
            case Ending.Normal_CaseClosed:
                return m_normalPostCredit;
            case Ending.Bad_Delusion:
                return m_badPostCredit;
            case Ending.Secret_Freedom:
                return m_secretPostCredit;
            default:
                return m_normalPostCredit;
        }
    }
    protected PlayableAsset GetEnding(Ending ending)
    {
        switch (ending)
        {
            case Ending.Normal_CaseClosed:
               return m_normal;
            case Ending.Bad_Delusion:
                return m_bad;
            case Ending.Secret_Freedom:
                return m_secret;
            default:
                return m_normal;
        }
    }
}
