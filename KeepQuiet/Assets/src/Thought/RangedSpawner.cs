using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class RangedSpawner : MonoBehaviour 
{
    Collider2D SpawnRange => GetComponent<Collider2D>();
    public T Spawn<T>(T spawnRef, bool clearZ = true) where T : MonoBehaviour
    {
        Vector3 spawnPos = GameUtil.RandomPositionInBounds(SpawnRange.bounds);
        T ret = GameUtil.SpawnObject(spawnRef, spawnPos, transform);
        // To stop instance from inheriting z position to stop z fighting 
        if (clearZ) 
        {
            Vector3 pos = ret.transform.localPosition;
            pos.z = 0f;
            ret.transform.localPosition = pos;
        }
        return ret;
    }
    // static version providing the range
    public static T Spawn<T>(T spawnRef, Collider2D range, bool clearZ = true) where T : MonoBehaviour 
    {
        Vector3 spawnPos = GameUtil.RandomPositionInBounds(range.bounds);
        T ret = GameUtil.SpawnObject(spawnRef, spawnPos, range.transform);
        // To stop instance from inheriting z position to stop z fighting 
        if (clearZ)
        {
            Vector3 pos = ret.transform.localPosition;
            pos.z = 0f;
            ret.transform.localPosition = pos;
        }
        return ret;
    }
}