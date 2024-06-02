using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimeline : TriggerInteraction
{
    [SerializeField] protected PlayableDirector m_director = default;
    public override void Trigger()
    {
        m_director.Play();
    }
}