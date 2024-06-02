public struct FloatRange
{
    private float m_min;
    private float m_max;
    public FloatRange(float min, float max)
    {
        m_min = min;
        m_max = max;
    }
    public float Min => m_min;
    public float Max => m_max;
}
