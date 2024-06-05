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
    [SerializeField] protected ToolInteractionHandler m_toolMenu = default;
    [SerializeField] protected Aria m_aria = default;
    [SerializeField] protected ScreenFade m_fade = default;
    [SerializeField] protected Volume m_postProcess = default;
    [SerializeField] protected SnapshotWatch m_watch = default;
    // view states
    [SerializeField] protected ViewState m_outsideCam = default;
    [SerializeField] protected ViewState m_outsideAria = default;
    [SerializeField] protected ViewState m_insideCafe = default;
    [SerializeField] protected RoomLeftView m_roomLeft = default;
    [SerializeField] protected ViewState m_roomRight = default;
    [SerializeField] CurryGameEventTrigger m_saveGameState = default;
    // Called when scene is ready for loading save data
    [SerializeField] SaveDataSource m_saveData = default;
    Dictionary<string, ViewState> m_views;
    ViewState m_currentView;
    SaveData m_currentGameState = new SaveData();
    public DoorState LeftRoomDoor => m_roomLeft.DoorState;
    public SaveData CurrentGameState => new SaveData(m_currentGameState);
    void OnEnable()
    {
        m_saveData.OnUpdate += Init;
        m_toolMenu.BatUnlocked += OnBatUnlock;
        m_toolMenu.SpecialTorchUnlocked += OnSpecialTorchUnlock;
    }
    void OnDisable()
    {
        m_saveData.OnUpdate -= Init;
        m_toolMenu.BatUnlocked -= OnBatUnlock;
        m_toolMenu.SpecialTorchUnlocked -= OnSpecialTorchUnlock;
    }
    void Start()
    {
        UpdateGameState();
    }
    void UpdateGameState() 
    {
        //Listen to savedatat load event, ready to receive save data
        m_saveData.RequestGameState();
    }
    public void SaveGameState(Action onFinish = null)
    {
        Dictionary<string, object> payload = new Dictionary<string, object>
        {{"save", CurrentGameState }};
        EventInfo info = new EventInfo(payload, onFinishCallback: onFinish);
        m_saveGameState?.TriggerEvent(info);
        // new save data available to update
        UpdateGameState();
    }
    public Aria GetAria()
    {
        return m_aria;
    }
    void Init(SaveData saved)
    {
        m_currentGameState = saved;
        m_watch?.Init(saved);
        //Hide all view first
        m_outsideCam?.SetVisual(false);
        m_outsideAria?.SetVisual(false);
        m_insideCafe?.SetVisual(false);
        m_roomLeft?.SetVisual(false);
        m_roomRight?.SetVisual(false);
        m_toolMenu?.Init(saved);
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
        m_currentView?.SetVisual(false);
        // Show New Visuals
        m_currentView = newView;
        m_postProcess.profile = m_currentView.PostProcessVolumeProfile;
        m_currentView?.SetVisual(true);
        m_currentView?.InitState(m_currentGameState);
        yield return new WaitForEndOfFrame();
        //Update Aria state after scene is set up
        m_aria?.Init(m_currentGameState);
    }
    public void OnToolUse(EToolType usingTool) 
    {
        m_currentView?.OnUsingTool(usingTool);
    }
    public void OnToolReturn(EToolType returningTool) 
    {
        m_currentView?.OnReturningTool(returningTool);
    }
    void OnBatUnlock() 
    {
        m_currentGameState.BatTaken = true;
        SaveGameState();
    }

    void OnSpecialTorchUnlock() 
    {
        m_currentGameState.SpecialTorchUnlocked = true;
        SaveGameState();
    }
}
