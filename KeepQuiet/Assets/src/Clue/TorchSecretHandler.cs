using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TorchSecretHandler : MonoBehaviour 
{
    [SerializeField] Material m_hiddenObjectMat = default;
    bool m_isSpecialTorchOn = false;
    static readonly Vector2 c_materialBounds = new Vector2(18, 10);
    Vector2 m_currentTorchMatPosition = Vector2.zero;
    void Start()
    {
        // set torch as out of bounds if we're not using torch
        m_hiddenObjectMat?.SetVector("_lightPos", c_materialBounds);
    }
    void Update()
    {
        if (m_isSpecialTorchOn) 
        {
            // update position
            m_currentTorchMatPosition = 
                GetTorchMaterialPosition(Mouse.current.position.ReadValue());
            // set material light pos
            m_hiddenObjectMat?.SetVector("_lightPos", m_currentTorchMatPosition);
        }
    }
    // toggle hidden object activity
    public void SetHiddenObjectsActive(bool isActive) 
    {
        m_isSpecialTorchOn = isActive;
    }
    Vector2 GetTorchMaterialPosition(Vector2 screenPos) 
    {
        // Transpose screen pos to material coord
        // material coord origin id from centre,
        // screen origin is bottom left
        float x = ((screenPos.x - (0.5f * Screen.width))/ Screen.width);
        // mult by material bounds
        x *= c_materialBounds.x;
        float y = ((screenPos.y - (0.5f * Screen.height))/ Screen.height); 
        y *= c_materialBounds.y;
        return new Vector2(x, y);
    }
}
