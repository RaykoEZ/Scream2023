using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
// sequence can be skipped or fast-forwarded
public class SkippableSequencePlayer : SequencePlayer
{
    [Range(1f, 10f)]
    [SerializeField] float m_fastforwardSpeed = default;
    bool m_isPlaying = false;
    protected override IEnumerator PlaySequence_Internal() 
    {
        m_isPlaying = true;
        yield return base.PlaySequence_Internal();
        m_isPlaying = false;
    }
    public void FastForward() 
    {
        if (!m_isPlaying) return;
        m_director.playableGraph.GetRootPlayable(0).SetSpeed(m_fastforwardSpeed);
    }
    public void Skip() 
    {
        if (!m_isPlaying) return;
        m_director.playableGraph.Stop();
    }
}
