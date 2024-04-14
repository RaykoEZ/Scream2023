using Curry.Events;
using System.Collections.Generic;
using UnityEngine;
// Class to trigger all child ITriggerInteractions in scene, invoke in order of FindInChildren
public class GameInteractionListener : MonoBehaviour 
{
    [SerializeField] protected CurryGameEventListener m_listenTo = default;
    [SerializeReference] List<TriggerInteraction> m_toTrigger;
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
        foreach (var item in m_toTrigger)
        {
            item?.Trigger();
        }
    }
}
public abstract class TriggerInteraction : MonoBehaviour
{
    public abstract void Trigger();
}
