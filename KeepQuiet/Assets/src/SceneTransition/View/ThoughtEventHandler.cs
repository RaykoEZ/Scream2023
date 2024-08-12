using Curry.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ThoughtEventHandler : MonoBehaviour
{
    [SerializeField] ThoughtSpawnManager m_spawn = default;
    HashSet<ThoughtDetail> m_heldThoughts = new HashSet<ThoughtDetail>();
    public HashSet<ThoughtDetail> HeldThoughts => m_heldThoughts;
    public void Init(HashSet<ThoughtDetail> heldThoughts) 
    {
        if (heldThoughts != null) 
        {
            m_heldThoughts = heldThoughts;
        }
        // Spawn currently held thoughts
        foreach (var item in m_heldThoughts)
        {
            m_spawn?.SpawnThoughtBubble(item);
        }
    }
    void Add(ThoughtDetail newThought) 
    {
        if (newThought == null) return;
        m_heldThoughts.Add(newThought);
    }
    void Remove(ThoughtDetail toRemove) 
    {
        m_heldThoughts.Remove(toRemove);
    }
    public void OnConsume(EventInfo info) 
    {
        if (info == null || info.Payload == null) return;
        if(info.Payload.TryGetValue("thought", out object result) && 
            result is ThoughtDetail detail) 
        {
            Remove(detail);
        } 
    }
    public void OnObtain(EventInfo info)
    {
        if (info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("thought", out object result) &&
            result is ThoughtBubble bubble)
        {
            Add(bubble.DetailRef);
        }
    }
}
