using Curry.Explore;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolAimIcon : HideableUI 
{
    bool m_show = false;
    Vector3 m_cursorPos;
    protected virtual void Update()
    {
        if (m_show) 
        {
            m_cursorPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            m_cursorPos.z = 0f;
            transform.position = m_cursorPos;
        }
    }
    public virtual void ShowCursor() 
    {
        m_show = true;
        Show();
    }
    public virtual void HideCursor() 
    {
        m_show = false;
        Hide();
    }
}

