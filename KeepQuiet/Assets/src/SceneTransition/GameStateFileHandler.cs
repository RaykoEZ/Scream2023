using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using Curry.Events;
using System.Collections.Generic;
// Script for persistent game state loading and saving
// Loads persistent game state into game state manager in scene
// Saves updated states coming from game state in scene
public class GameStateFileHandler : MonoBehaviour
{
    // State to load upon first load
    [SerializeField] GameStateContainer m_defaultState = default;
    [SerializeField] CurryGameEventListener m_exitGame = default;
    [SerializeField] CurryGameEventListener m_onsaveGameState = default;
    [SerializeField] CurryGameEventListener m_onGameReady = default;
    [SerializeField] CurryGameEventTrigger m_loadGameState = default;
    SaveData m_current;
    static string s_gamestatePath = "saves/gamestate.json";
    private void Start()
    {
        m_exitGame?.Init();
        m_onsaveGameState?.Init();
        LoadStates();
    }
    public void LoadGameState()
    {
        Dictionary<string, object> payload = new Dictionary<string, object>
        {{"save", new SaveData(m_current)}};
        EventInfo info = new EventInfo(payload);
        m_loadGameState?.TriggerEvent(info);
    }
    public void OnGameReady(EventInfo info)
    {
        LoadGameState();
    }
    public void OnGameSave(EventInfo info) 
    {
        Dictionary<string, object> payload = info.Payload;
        if (payload == null) return;
        bool ending = payload.TryGetValue("ending", out object e) ? (bool)e : false;
        bool crash = payload.TryGetValue("crash", out object c) ? (bool)c : false;
        if (payload.TryGetValue("state", out object result)
            && result is GameStateManager state)
        {
            // Autosave
            SaveStates(state, ending, crash);
        }
        // Do on finish callback
        info.OnFinishedCallback?.Invoke();
    }
    public void OnQuitGame(EventInfo info) 
    {
        OnGameSave(info);
        Application.Quit();
    }
    // Read Meta File states and Locations to update game state
    protected void LoadStates() 
    {
        if (!File.Exists(s_gamestatePath)) 
        {
            m_current = new SaveData(m_defaultState.State);
            return;
        }
        using (StreamReader r = new StreamReader($"{FileUtil.s_gamestateSavePath}/{s_gamestatePath}"))
        {
            string json = r.ReadToEnd();
            SaveData loaded = JsonConvert.DeserializeObject<SaveData>(json);
            m_current = loaded;
        }       
    }
    protected void SaveStates(GameStateManager currentState, bool ending = false, bool crash = false) 
    {
        int crashCount = crash ? ++m_current.CrashCount : m_current.CrashCount;
        // if we are saving after finish an ending, increment new gamw counter
        int newGameCount = ending ? ++m_current.NewGameCount : m_current.NewGameCount;
        Aria aria = currentState.GetAria();
        SaveData newSave = new SaveData(
            currentState.LeftRoomDoor, newGameCount, crashCount, aria.Current, currentState.GetCurrentViewState());
        string json = JsonConvert.SerializeObject(newSave);
        FileUtil.RawTextTo(FileUtil.s_gamestateSavePath, "saves","gamestate.json", new string[] { json });
    }
}