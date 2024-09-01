using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Curry.Explore
{
    // A utility class to prevent UI displays from flickering due to pointer alternating transitions
    // at edges of UI triggers 
    public class UITransitionBuffer 
    {
        const float c_transitionBufferTime = 0.5f;
        bool transitionBuffering = false;
        public bool Buffering => transitionBuffering;
        public IEnumerator Buffer()
        {
            transitionBuffering = true;
            yield return new WaitForSeconds(c_transitionBufferTime);
            transitionBuffering = false;
        }
    }

    // When pointer is in range, show ui, hide if not
    public class ToggleHandler : MonoBehaviour
    {
        [SerializeField] HideableUITrigger m_ui = default;
        UITransitionBuffer m_buffer = new UITransitionBuffer();
        bool m_isOn = false;
        public void Toggle()
        {
            if (m_isOn) 
            {
                Hide();
            } 
            else 
            {
                Show();
            }
        }
        public void Show() 
        {
            // Buffer transition to stop unwanted flickers on edges
            if (!m_buffer.Buffering)
            {
                m_ui.Show();
                StartCoroutine(m_buffer.Buffer());
                m_isOn = true;
            }
        }
        public void Hide() 
        {
            m_ui.Hide();
            m_isOn = false;
        }
    }
}