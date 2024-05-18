using UnityEngine;
public static class GameUtil 
{
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
