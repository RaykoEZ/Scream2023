using Curry.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
// handles behaviours for current game state in game scene
public class GameStateManager : MonoBehaviour
{
    [SerializeField] protected InspectionDisplayHandler m_inspect = default;
    [SerializeField] protected Aria m_aria = default;
    [SerializeField] protected ScreenFade m_fade = default;
    [SerializeField] protected Volume m_postProcess = default;
    [SerializeField] protected ViewState m_outsideCam = default;
    [SerializeField] protected ViewState m_outsideAria = default;
    [SerializeField] protected ViewState m_insideCafe = default;
    [SerializeField] protected RoomLeftView m_roomLeft = default;
    [SerializeField] protected ViewState m_roomRight = default;
    [SerializeField] CurryGameEventTrigger m_saveGameState = default;
    [SerializeField] CurryGameEventListener m_onGameLoad = default;

    Dictionary<string, ViewState> m_views;
    ViewState m_currentView;
    GameStateSaveData m_currentGameState;
    public DoorState LeftRoomDoor => m_roomLeft.DoorState;
    public GameStateSaveData CurrentGameState => new GameStateSaveData(m_currentGameState);
    private void Start()
    {
        m_onGameLoad?.Init();
    }
    public List<ViewStateSaveData> GetCurrentViewState()
    {
        List<ViewStateSaveData> ret = new List<ViewStateSaveData>
        {
            m_outsideCam.GetCurrentState(),
            m_outsideAria.GetCurrentState(),
            m_insideCafe.GetCurrentState(),
            m_roomLeft.GetCurrentState(),
            m_roomRight.GetCurrentState()
        };
        return ret;
    }
    public void SaveGameState()
    {
        EventInfo info = new EventInfo();
        m_saveGameState?.TriggerEvent(info);
    }
    public void OnGameStateLoaded(EventInfo info) 
    { 
    
    }
    public Aria GetAria()
    {
        return m_aria;
    }
    public void Init(GameStateSaveData saved)
    {
        m_aria?.Init(saved);
        m_currentGameState = saved;
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
        StartCoroutine(ChangeView_Internal(m_views[saved.CurrentlyViewing]));
    }
    public void ChangeView(ViewState newView) 
    {
        if (newView == null) return;
        m_fade?.StartFade(ChangeView_Internal(newView));
    }
    IEnumerator ChangeView_Internal(ViewState newView) 
    {
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
    }
    public void InspectClue(Clue toInspect) 
    {
        var current = CurrentGameState;
        m_inspect?.InspectTarget(toInspect.GetInspectionDisplay(current), current);
    }
    public void OnToolUse(EToolType usingTool) 
    {
        m_currentView?.OnUsingTool(usingTool);
    }
    public void OnToolReturn(EToolType returningTool) 
    {
        m_currentView?.OnReturningTool(returningTool);
    }
}
