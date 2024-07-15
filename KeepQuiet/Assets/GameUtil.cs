using System;
using System.Collections;
using UnityEngine;
public static class GameUtil 
{
    // Spawn a gameobject a prefab reference (preferrably)
    public static T SpawnObject<T>(T spawnRef, Vector3 position, Transform parent = null) where T : MonoBehaviour
    {
        T ret = UnityEngine.Object.Instantiate(spawnRef);
        ret.transform.SetParent(parent, false);
        ret.transform.position = position;
        return ret;
    }
    public static Vector3 RandomPositionInBounds(Bounds bounds)
    {
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        float z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
    public static Vector2 RandomPositionInBounds2D(Bounds bounds)
    {
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
    // Coountdown in seconds
    public static IEnumerator Countdown(float secondsLeft, Action onFinish = null)
    {
        float t = secondsLeft;
        while (t > 0f) 
        {
            yield return new WaitForSeconds(1f);
            t--;
        }
        onFinish?.Invoke();
    }
    // Get min and max of a angle with range thresholds
    public static FloatRange SignedAngleThresholdRange(float threshold, float margin)
    {
        float a = threshold - margin;
        float b = threshold + margin;
        float min;
        float max;
        if (a < -180f)
        {
            max = a + 360f;
            min = b;
        }
        else if (b > 180f)
        {
            min = b - 360f;
            max = a;
        }
        else
        {
            min = a;
            max = b;
        }
        return new FloatRange(min, max);
    }
    // rotate a transform by a given signed angle
    public static void SignedRotationDegree(Transform target, float signedAngle) 
    {
        Vector3 newRotation = new Vector3(0f, 0f, signedAngle);
        newRotation.z = signedAngle;
        Quaternion rotateTo = Quaternion.Euler(newRotation);
        // rotate to new direction
        target.rotation = Quaternion.RotateTowards(target.rotation, rotateTo, 360f);
    }
}
