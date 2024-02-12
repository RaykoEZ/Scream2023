using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class DragRotationHandler : MonoBehaviour
{
    [SerializeField] Transform m_neutralPosition = default;
    Vector3 m_mousePos = Vector3.zero;
    Vector3 m_originDir;
    private void Start()
    {
        // initialize direction
        m_originDir = (m_neutralPosition.position - transform.position);
    }
    public void BeginRotation()
    {
        Vector3 dir;
        float angle;
        Vector3 newRotation;
        m_mousePos = Mouse.current.position.ReadValue();
        m_mousePos = Camera.main.ScreenToWorldPoint(m_mousePos);
        dir = (m_mousePos - transform.position);
        angle = Vector2.SignedAngle(m_originDir, dir);
        newRotation = new Vector3(0f, 0f, angle);
        newRotation.z = angle;
        Quaternion rotateTo = Quaternion.Euler(newRotation);
        // rotate to new cursor direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, 360f);
    }
}
