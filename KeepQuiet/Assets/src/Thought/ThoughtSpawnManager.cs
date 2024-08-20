using Curry.Events;
using UnityEngine;
public class ThoughtSpawnManager : MonoBehaviour 
{
    [SerializeField] ThoughtBubble m_spawnRef = default;
    [SerializeField] RangedSpawner m_spawner = default;
    public void SpawnThoughtBubble(ThoughtDetail detail) 
    {
        var instance = m_spawner?.Spawn(m_spawnRef);
        instance?.Init(detail);
    }
}