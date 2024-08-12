using Curry.Events;
using UnityEngine;
public class ThoughtSpawnManager : MonoBehaviour 
{
    [SerializeField] ThoughtBubble m_spawnRef = default;
    [SerializeField] RangedSpawner m_spawner = default;
    [SerializeField] CurryGameEventListener m_obtainThought = default;

    void Start()
    {
        m_obtainThought?.Init();
    }
    public void OnThoughtObtain(EventInfo info) 
    {
        if (info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("thought", out object result) &&
            result is ThoughtDetail detail)
        {
            SpawnThoughtBubble(detail);
        }
    }
    public void SpawnThoughtBubble(ThoughtDetail detail) 
    {
        var instance = m_spawner?.Spawn(m_spawnRef);
        instance?.Init(detail);
    }
}