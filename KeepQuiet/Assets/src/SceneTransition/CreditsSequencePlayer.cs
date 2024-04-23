using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CreditsSequencePlayer : MonoBehaviour
{
    [SerializeField] PlayableDirector m_endingDirector = default;
    [SerializeField] PlayableAsset m_credits = default;
    bool m_isPlaying = false;
    public void PlayCredits()
    {
    }
    IEnumerator Credit_Internal() 
    {
        m_isPlaying = true;
        m_endingDirector.Play(m_credits);
        yield return new WaitWhile(()=>m_endingDirector.playableGraph.IsPlaying());
        m_isPlaying = false;
    }
    public void SetPlaySpeed(float speed) 
    {
        if (!m_isPlaying) return;
        m_endingDirector.playableGraph.GetRootPlayable(0).SetSpeed(speed);
    }
}
