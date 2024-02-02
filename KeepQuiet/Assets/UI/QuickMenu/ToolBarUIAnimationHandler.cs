using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public class ToolBarUIAnimationHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] bool m_showOnHover = default;
        [SerializeField] HideableUITrigger m_hidingUI = default;
        [SerializeField] CanvasGroup m_uiToggle = default;
        UITransitionBuffer m_buffer = new UITransitionBuffer();
        bool m_isOn = false;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!m_showOnHover) return;
            // Buffer transition to stop unwanted flickers on edges
            Show();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!m_showOnHover) return;
            Hide();
        }
        public void SetUIToggleActive(bool isActive) 
        {
            m_uiToggle.alpha = isActive ? 1f : 0f;
            m_uiToggle.blocksRaycasts = isActive;
        }
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
            m_isOn = !m_isOn;
        }
        public void Show() 
        {
            // Buffer transition to stop unwanted flickers on edges
            if (!m_buffer.Buffering)
            {
                m_hidingUI.Show();
                StartCoroutine(m_buffer.Buffer());
            }
        }
        public void Hide() 
        {
            m_hidingUI.Hide();
        }
    }
}