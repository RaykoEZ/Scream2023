using System;
using System.Collections;
using UnityEngine;
// handles behaviours for a view
public class ViewStateManager : MonoBehaviour
{
    [SerializeField] protected ScreenFade m_fade = default;
    [SerializeField] protected ViewState m_outsideCam = default;
    [SerializeField] protected ViewState m_outsideAria = default;
    [SerializeField] protected ViewState m_insideCafe = default;
    [SerializeField] protected ViewState m_roomLeft = default;
    [SerializeField] protected ViewState m_roomRight = default;
    ViewState m_currentView;
    public void ChangeView(ViewState newView) 
    {
        if (newView == null) return;
        m_fade?.StartFade(ChangeView_Internal(newView));  
    }
    IEnumerator ChangeView_Internal(ViewState newView) 
    {
        // Hide current view visuals
        m_currentView?.SetVisual(false);
        // Show New Visuals
        m_currentView = newView;
        m_currentView?.SetVisual(true);
        yield return new WaitForEndOfFrame();
    }
}
