using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
// handles behaviours for a view
public class ViewStateManager : MonoBehaviour
{
    [SerializeField] protected ScreenFade m_fade = default;
    [SerializeField] protected Volume m_postProcess = default;
    [SerializeField] protected ViewState m_outsideCam = default;
    [SerializeField] protected ViewState m_outsideAria = default;
    [SerializeField] protected ViewState m_insideCafe = default;
    [SerializeField] protected ViewState m_roomLeft = default;
    [SerializeField] protected ViewState m_roomRight = default;
    Dictionary<string, ViewState> m_views;
    ViewState m_currentView;
    private void Start()
    {

    }
    public void Init(GameStateSaveData saved)
    {
        //Hide all view first
        m_outsideCam?.SetVisual(false);
        m_outsideAria?.SetVisual(false);
        m_insideCafe?.SetVisual(false);
        m_roomLeft?.SetVisual(false);
        m_roomRight?.SetVisual(false);
        m_views = new Dictionary<string, ViewState>
        {
            {m_outsideCam.Name, m_outsideCam},
            {m_outsideAria.Name, m_outsideAria},
            {m_insideCafe.Name, m_insideCafe},
            {m_roomLeft.Name, m_roomLeft},
            {m_roomRight.Name, m_roomRight}
        };
    }
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
        m_postProcess.profile = m_currentView.PostProcessVolumeProfile;
        m_currentView?.SetVisual(true);
        yield return new WaitForEndOfFrame();
    }
}
