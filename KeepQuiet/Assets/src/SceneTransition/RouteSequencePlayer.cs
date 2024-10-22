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
public class RouteSequencePlayer : SequencePlayer
{
    // ending sequence before credits
    [SerializeField] PlayableAsset m_endingIntro = default;
    // play launch route
    [SerializeField] PlayableAsset m_defaultRoute = default;
    [SerializeField] PlayableAsset m_badRoute = default;
    [SerializeField] PlayableAsset m_normalRoute = default;
    [SerializeField] PlayableAsset m_secretRoute = default;
    public void PlayEnding() 
    {
        PlaySequence(m_endingIntro);
    }
    public void RouteSequence(Ending ending) 
    {
        var toPlay = GetRouteSequence(ending);
        PlaySequence(toPlay);
    }
    protected PlayableAsset GetRouteSequence(Ending ending)
    {
        switch (ending)
        {
            case Ending.Normal_CaseClosed:
                return m_normalRoute;
            case Ending.Bad_Delusion:
                return m_badRoute;
            case Ending.Secret_Freedom:
                return m_secretRoute;
            default:
                return m_defaultRoute;
        }
    }
}
