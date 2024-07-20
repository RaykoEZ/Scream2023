using Curry.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ThoughtEventHandler : MonoBehaviour
{
    HashSet<ThoughtDetail> m_heldThoughts = new HashSet<ThoughtDetail>();
    public HashSet<ThoughtDetail> HeldThoughts => m_heldThoughts;
    public void Init(HashSet<ThoughtDetail> heldThoughts) 
    {
        if (heldThoughts != null) 
        {
            m_heldThoughts = heldThoughts;
        }
    }
    public void Add(ThoughtDetail newThought) 
    {
        if (newThought == null) return;
        m_heldThoughts.Add(newThought);
    }
    public void Remove(ThoughtDetail toRemove) 
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
