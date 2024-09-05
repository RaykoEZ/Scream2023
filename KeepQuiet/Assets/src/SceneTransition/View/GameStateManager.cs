using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
// handles behaviours for current game state in game scene
// Notifies to save new persistent game state
// Loads saved game state when game scene initializes
public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameSaveSource m_save = default;
    [SerializeField] protected AudioTrigger m_audio = default;
    [SerializeField] protected Volume m_postProcess = default;
    // view states
    [SerializeField] protected ViewState m_outsideCam = default;
    [SerializeField] protected ViewState m_outsideAria = default;
    [SerializeField] protected ViewState m_insideCafe = default;
    [SerializeField] protected ViewState m_roomLeft = default;
    [SerializeField] protected ViewState m_roomRight = default;
    // Called when scene is ready for loading save data
    Dictionary<string, ViewState> m_views;
    ViewState m_currentView;
    SaveData m_currentGameState;
    public void Init(SaveData saved)
    {
        m_currentGameState = saved;
        m_views = new Dictionary<string, ViewState>
        {
            {m_outsideCam.Name, m_outsideCam},
            {m_outsideAria.Name, m_outsideAria},
            {m_insideCafe.Name, m_insideCafe},
            {m_roomLeft.Name, m_roomLeft},
            {m_roomRight.Name, m_roomRight}
        };
        StartCoroutine(ChangeView_Internal(m_views[saved.CurrentlyViewing]));
    }
    public void UpdateSave() 
    {
        m_save.Current.CurrentlyViewing = m_currentView.Name;
    }
    public void ChangeView(ViewState newView) 
    {
        if (newView == null) return;
        StartCoroutine(ChangeView_Internal(newView));
    }
    IEnumerator ChangeView_Internal(ViewState newView) 
    {
        m_audio?.StopBgm();
        m_audio?.StopRain();
        // update state for viewing location
        m_currentGameState.CurrentlyViewing = newView.Name;
        // Hide current view visuals
        m_currentView?.ResetState();
        // Show New Visuals
        m_currentView = newView;
        m_postProcess.profile = m_currentView.PostProcessVolumeProfile;
        m_currentView?.InitState(m_currentGameState);
        yield return new WaitForEndOfFrame();
    }
}