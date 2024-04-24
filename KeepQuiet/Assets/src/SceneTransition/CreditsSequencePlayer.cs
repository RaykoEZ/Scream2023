using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
public delegate void OnCreditEnd();
public class CreditsSequencePlayer : MonoBehaviour
{
    [SerializeField] PlayableDirector m_creditDirector = default;
    bool m_isPlaying = false;
    public event OnCreditEnd OnFinish;
    public void PlayCredits()
    {
        StartCoroutine(Credit_Internal());
    }
    IEnumerator Credit_Internal() 
    {
        m_isPlaying = true;
        m_creditDirector?.Play();
        yield return new WaitWhile(()=> m_creditDirector.playableGraph.IsPlaying());
        OnFinish?.Invoke();
        m_isPlaying = false;
    }
    public void SetPlaySpeed(float speed) 
    {
        if (!m_isPlaying) return;
        m_creditDirector.playableGraph.GetRootPlayable(0).SetSpeed(speed);
    }
    public void Skip() 
    {
        if (!m_isPlaying) return;
        m_creditDirector.playableGraph.Stop();
    }
}
