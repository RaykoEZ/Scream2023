using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(RectTransform))]
public class DragRotationHandler : MonoBehaviour
{
    [SerializeField] Transform m_neutralPosition = default;
    Vector3 m_mousePos = Vector3.zero;
    Vector3 m_originDir;
    float m_currentAngle = 0;
    public float CurrentAngle => m_currentAngle;
    private void Start()
    {
        // initialize direction
        m_originDir = (m_neutralPosition.position - transform.position);
    }
    public void SetRotation(float signedAngle) 
    {
        m_currentAngle = signedAngle;
        GameUtil.SignedRotationDegree(transform, signedAngle);
    }
    public void BeginRotation()
    {
        Vector3 dir;
        float angle;
        m_mousePos = Mouse.current.position.ReadValue();
        m_mousePos = Camera.main.ScreenToWorldPoint(m_mousePos);
        dir = (m_mousePos - transform.position);
        angle = Vector2.SignedAngle(m_originDir, dir);
        SetRotation(angle);
    }
}
