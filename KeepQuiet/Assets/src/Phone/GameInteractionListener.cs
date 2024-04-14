using Curry.Events;
using UnityEngine;
using UnityEngine.Playables;
// Class to trigger all child ITriggerInteractions in scene, invoke in order of FindInChildren
public class GameInteractionListener : MonoBehaviour 
{
    [SerializeField] protected CurryGameEventListener m_listenTo = default;
    protected virtual void OnEnable()
    {
        m_listenTo?.Init();
    }
    protected virtual void OnDisable()
    {
        m_listenTo?.Shutdown();
    }
    // Trigger called when a listening game event triggers
    public virtual void TriggerInteractions(EventInfo info) 
    {
        TriggerInteractions();
    }
    public virtual void TriggerInteractions()
    {
        var toTrigger = transform.GetComponentsInChildren<ITriggerInteraction>();
        foreach (var item in toTrigger)
        {
            item?.Trigger();
        }
    }
}

public interface ITriggerInteraction 
{
    void Trigger();
}

public class TriggerAudio : MonoBehaviour, ITriggerInteraction
{
    [SerializeField] bool m_loop = default;
    [SerializeField] AudioSource m_audio = default;
    [SerializeField] AudioClip m_toTrigger = default;
    public void Trigger()
    {
        m_audio.loop = m_loop;
        m_audio.clip = m_toTrigger;
        m_audio.Play();
    }
}
public class TriggerPhoneCall : MonoBehaviour, ITriggerInteraction
{
    [SerializeField] protected PlayerCaller m_trigger = default;
    [SerializeField] protected DialResult m_callContent = default;
    public void Trigger()
    {
        m_trigger?.Call(m_callContent.Sequence, m_callContent.EventToTrigger);
    }
}
public class TriggerMessage : MonoBehaviour, ITriggerInteraction
{
    [SerializeField] protected PlayerCaller m_trigger = default;
    [SerializeField] protected DialogueNode m_message = default;
    public void Trigger()
    {
        m_trigger?.Message(m_message);
    }
}

public class TriggerCutscene : MonoBehaviour, ITriggerInteraction
{
    [SerializeField] protected PlayableDirector m_director = default;
    [SerializeField] protected PlayableAsset m_toTrigger = default;
    public void Trigger()
    {
        m_director.playableAsset = m_toTrigger;
        m_director.Play();
    }
}