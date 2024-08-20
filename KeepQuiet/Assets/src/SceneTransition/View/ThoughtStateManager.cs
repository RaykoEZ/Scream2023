using Curry.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
// Holds states of held thoughts
[Serializable]
public class ThoughtStateManager : MonoBehaviour
{
    [SerializeField] ThoughtSpawnManager m_spawn = default;
    [SerializeField] ToggleAnimationHandler m_toggleAnim = default;
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
    // When thoughts can be dropped into things
    public void OnThoughtPrompt(EventInfo info)
    {
        if (info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("toDrop", out object result) &&
            result is List<ThoughtDetail> toDrop)
        {
            // Check if player has any held thoughts to drop
            HashSet<ThoughtDetail> check = new HashSet<ThoughtDetail>(m_heldThoughts);
            check.IntersectWith(toDrop);
            m_toggleAnim?.AnimateAlertIcon(check.Count > 0);
        }
    }
    public void OnConsume(EventInfo info) 
    {
        if (info == null || info.Payload == null) return;
        if(info.Payload.TryGetValue("thought", out object result) && 
            result is ThoughtBubble bubble) 
        {
            Remove(bubble.DetailRef);
            bubble?.Shutdown();
            Destroy(bubble.gameObject);
        } 
    }
    public void OnObtain(EventInfo info)
    {
        if (info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("thought", out object result) &&
            result is ThoughtDetail detail)
        {
            Add(detail);
            m_spawn?.SpawnThoughtBubble(detail);
        }
    }
}
