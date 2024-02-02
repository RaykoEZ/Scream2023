using UnityEngine;

// Class to handle UI buttons for moving from one view to another
public class ViewNavigationHandler : MonoBehaviour 
{
    [SerializeField] CanvasGroup m_roomParentNav = default;
    [SerializeField] CanvasGroup m_roomLeft = default;
    [SerializeField] CanvasGroup m_roomRight = default;

    [SerializeField] CanvasGroup m_cameraNav = default;
    [SerializeField] CanvasGroup m_returnLeft = default;
    [SerializeField] CanvasGroup m_returnRight = default;
    public void ToRoomLeft() 
    {
        m_roomLeft.alpha = 0f;
        m_roomLeft.blocksRaycasts = false;

        m_roomRight.alpha = 1f;
        m_roomRight.blocksRaycasts = true;
    }
    public void ToRoomRight() 
    {
        m_roomLeft.alpha = 1f;
        m_roomLeft.blocksRaycasts = true;

        m_roomRight.alpha = 0f;
        m_roomRight.blocksRaycasts = false;
    }
    public void ToCameraCafe()
    {
        m_returnLeft.alpha = 1f;
        m_returnLeft.blocksRaycasts = true;

        m_returnRight.alpha = 0f;
        m_returnRight.blocksRaycasts = false;

        m_roomParentNav.alpha = 0;
        m_cameraNav.alpha = 1f;
    }
    public void ToCameraOutside()
    {
        m_returnLeft.alpha = 0f;
        m_returnLeft.blocksRaycasts = false;

        m_returnRight.alpha = 1f;
        m_returnRight.blocksRaycasts = true;

        m_roomParentNav.alpha = 0;
        m_cameraNav.alpha = 1f;
    }
    public void ReturnToRoom(bool toRoomLeft) 
    {
        m_roomLeft.alpha = toRoomLeft ? 0f : 1f;
        m_roomRight.alpha = toRoomLeft ? 1f : 0f;
        m_roomLeft.blocksRaycasts = !toRoomLeft;
        m_roomRight.blocksRaycasts = toRoomLeft;

        m_roomParentNav.alpha = 1;
        m_cameraNav.alpha = 0;
    }
    public void HideAll() 
    {
        m_roomParentNav.alpha = 0f;
        m_cameraNav.alpha = 0f;
    }
}

