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
        m_roomLeft.interactable = false;
        m_roomLeft.blocksRaycasts = false;

        m_roomRight.alpha = 1f;
        m_roomRight.interactable = true;
        m_roomRight.blocksRaycasts = true;
    }
    public void ToRoomRight() 
    {
        m_roomLeft.alpha = 1f;
        m_roomLeft.interactable = true;
        m_roomLeft.blocksRaycasts = true;

        m_roomRight.alpha = 0f;
        m_roomRight.interactable = false;
        m_roomRight.blocksRaycasts = false;
    }
    public void ToCameraCafe()
    {
        m_returnLeft.alpha = 1f;
        m_roomLeft.interactable = true;
        m_returnLeft.blocksRaycasts = true;

        m_returnRight.alpha = 0f;
        m_roomRight.interactable = false;
        m_returnRight.blocksRaycasts = false;

        m_roomParentNav.alpha = 0;
        m_roomParentNav.interactable = false;
        m_roomParentNav.blocksRaycasts = false;

        m_cameraNav.alpha = 1f;
        m_cameraNav.interactable = true;
        m_cameraNav.blocksRaycasts = true;
    }
    public void ToCameraOutside()
    {
        m_returnLeft.alpha = 0f;
        m_returnLeft.interactable = true;
        m_returnLeft.blocksRaycasts = false;

        m_returnRight.alpha = 1f;
        m_returnRight.interactable = true;
        m_returnRight.blocksRaycasts = true;

        m_roomParentNav.alpha = 0;
        m_roomParentNav.interactable = false;
        m_roomParentNav.blocksRaycasts = false;

        m_cameraNav.alpha = 1f;
        m_cameraNav.interactable = true;
        m_cameraNav.blocksRaycasts = true;
    }
    public void ReturnToRoom(bool toRoomLeft) 
    {
        m_roomLeft.alpha = toRoomLeft ? 0f : 1f;
        m_roomRight.alpha = toRoomLeft ? 1f : 0f;
        m_roomLeft.interactable = !toRoomLeft;
        m_roomLeft.blocksRaycasts = !toRoomLeft;

        m_roomLeft.interactable = toRoomLeft;
        m_roomRight.blocksRaycasts = toRoomLeft;

        m_roomParentNav.alpha = 1;
        m_roomParentNav.interactable = true;
        m_roomParentNav.blocksRaycasts = true;
        m_cameraNav.alpha = 0;
        m_cameraNav.interactable = false;
        m_cameraNav.blocksRaycasts = false;
    }
    public void HideAll() 
    {
        m_roomParentNav.alpha = 0f;
        m_roomParentNav.interactable = false;
        m_roomParentNav.blocksRaycasts = false;
        m_cameraNav.alpha = 0f;
        m_cameraNav.interactable = false;
        m_cameraNav.blocksRaycasts = false;
    }
}

