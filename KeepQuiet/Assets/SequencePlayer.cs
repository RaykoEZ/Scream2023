using Curry.Events;
using System.Collections;
using System.Collections.Generic;
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
    protected void OnFinishCallback() 
    {
        OnFinish?.Invoke();
    }
    protected virtual IEnumerator PlaySequence_Internal() 
    {      
        m_director?.Play();
        yield return new WaitUntil(() => m_director.state == PlayState.Paused);
        yield return new WaitForSeconds(m_waitAfterSequenceFinishes);
        OnFinish?.Invoke();
    }
}