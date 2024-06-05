using Curry.Events;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public delegate void OnSequenceFinish();
//Plays visual sequences in the scene
public class SequencePlayer : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] protected float m_waitAfterSequenceFinishes = default;
    [SerializeField] protected PlayableDirector m_director = default;
    public event OnSequenceFinish OnFinish;
    // Start is called before the first frame update
    public virtual void PlaySequence() 
    {
        StartCoroutine(PlaySequence_Internal());
    }
    public void PlaySequence(PlayableAsset toPlay)
    {
        if (toPlay == null) return;
        m_director?.Play(toPlay);
    }
    protected void OnFinishCallback() 
    {
        OnFinish?.Invoke();
    }
    protected virtual IEnumerator PlaySequence_Internal() 
    {      
        m_director?.Play();
        yield return new WaitForSeconds((float)m_director.playableAsset.duration);
        yield return new WaitForSeconds(m_waitAfterSequenceFinishes);
        OnFinish?.Invoke();
    }
}
