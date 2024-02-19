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
}
