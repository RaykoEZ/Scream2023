using Curry.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider2D))]
public class ThoughtGainTrigger : MonoBehaviour 
{
    [SerializeField] protected CurryGameEventTrigger m_onObtainThought = default;
    [SerializeField] protected PlayableDirector m_director = default;
    protected EventInfo m_thoughtEvent;
    ThoughtGainSource m_currentSource;
    Coroutine m_inProgress;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_inProgress == null && 
            collision.TryGetComponent(out ThoughtGainSource source) &&
            source.Obtainable &&
            source.DetailToGain != null)
        {
            m_currentSource = source;
            m_thoughtEvent = new EventInfo(
                payload: new Dictionary<string, object> { { "thought", source.DetailToGain } });
            m_inProgress = StartCoroutine(ScanTimer());
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (m_inProgress == null) return;
        // Cancel timer
        StopCoroutine(m_inProgress);
        m_inProgress = null;
    }
    IEnumerator ScanTimer() 
    {
        m_director?.Play();
        yield return new WaitForSeconds(0.5f);
        m_onObtainThought?.TriggerEvent(m_thoughtEvent);
        m_currentSource?.Shutdown();
        m_inProgress = null;
        m_currentSource = null;
    }
    public virtual void ObtainThought(ThoughtDetail detail)
    {
        m_thoughtEvent = new EventInfo(
                payload: new Dictionary<string, object> { { "thought", detail } });
        m_onObtainThought?.TriggerEvent(m_thoughtEvent);
    }
}