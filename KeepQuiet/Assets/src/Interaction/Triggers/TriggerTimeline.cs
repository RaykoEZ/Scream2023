using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimeline : TriggerInteraction
{
    [SerializeField] protected PlayableDirector m_director = default;
    [SerializeField] protected PlayableAsset m_toTrigger = default;
    public override void Trigger()
    {
        m_director.playableAsset = m_toTrigger;
        m_director.Play();
    }
}