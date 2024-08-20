using Curry.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// handles behaviours for current game state in game scene
// Notifies to save new persistent game state
// Loads saved game state when game scene initializes
public class GameStateManager : MonoBehaviour
{
    [SerializeField] protected AudioTrigger m_audio = default;
    [SerializeField] protected Aria m_aria = default;
    [SerializeField] protected ScreenFade m_fade = default;
    [SerializeField] protected Volume m_postProcess = default;
    [SerializeField] protected SnapshotWatch m_watch = default;
    [SerializeField] protected ThoughtStateManager m_thoughts = default;
    // view states
    [SerializeField] protected ViewState m_outsideCam = default;
    [SerializeField] protected ViewState m_outsideAria = default;
    [SerializeField] protected ViewState m_insideCafe = default;
    [SerializeField] protected RoomLeftView m_roomLeft = default;
    [SerializeField] protected ViewState m_roomRight = default;
    // Called when scene is ready for loading save data
    [SerializeField] SaveDataSource m_saveData = default;
    Dictionary<string, ViewState> m_views;
    ViewState m_currentView;
    SaveData m_currentGameState = new SaveData();
    public SaveData CurrentGameState => new SaveData(m_currentGameState);
    void OnEnable()
    {
        m_saveData.OnRefresh += Init;
    }
    void OnDisable()
    {
        m_saveData.OnRefresh -= Init;
    }
    void Start()
    {
        m_saveData?.RequestLoadSave();
    }
    void Init(SaveData saved)
    {
        m_currentGameState = saved;
        m_watch?.Init(saved);
        m_thoughts?.Init(m_currentGameState.HeldThoughts);
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
    public void ChangeView(ViewState newView) 
    {
        if (newView == null) return;
        m_fade?.StartFade(ChangeView_Internal(newView));
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
        //Update Aria state after scene is set up
        m_aria?.Init(m_currentGameState);
    }
}