using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
// animates a rotating 2d scene with a zoom/panini distortion
public class SceneTiltEffectHandler : MonoBehaviour 
{
    [SerializeField] bool m_zoomIn = default;
    // Rotation direction
    [SerializeField] bool m_rotatingPositively = default;
    [Range(0f, 180f)]
    [SerializeField] float m_maxRotation = default;
    [SerializeField] float m_duration = default;
    [SerializeField] Volume m_zoomInDistortion = default;
    [SerializeField] Transform m_parentToRotate = default;
    public void Begin() 
    {
        StartCoroutine(RotateImage());
        StartCoroutine(ZoomDistortion());
    }
    IEnumerator ZoomDistortion() 
    {
        // start further away if zooming in,
        // volume weight at 1f = zoomed in
        m_zoomInDistortion.weight = m_zoomIn ? 0f : 1f;
        float t = 0f;
        while (t < 1f) 
        {
            // if we zoom in, we need to set distance by 1 - t
            m_zoomInDistortion.weight = m_zoomIn? t : 1f - t;
            yield return null;
            t += Time.deltaTime / m_duration;
        }
    }
    IEnumerator RotateImage()
    {
        float t = 0f;
        float angle;
        float dir = m_rotatingPositively ? 1f : -1f;
        while (t < 1f) 
        {
            t += Time.deltaTime / m_duration;
            angle = dir * t * m_maxRotation;
            GameUtil.SignedRotationDegree(m_parentToRotate, angle);
            yield return null;
        }
    }

}
