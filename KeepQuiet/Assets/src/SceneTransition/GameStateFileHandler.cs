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
        m_onGameReady?.Init();
        m_exitGame?.Init();
        m_onsaveGameState?.Init();
        LoadStates();
    }
    private void OnApplicationQuit()
    {            
        // Autosave on quitting
        SaveStates(m_current);
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
    // sets valid incoming save data
    void HandleSaveData(EventInfo info) 
    {
        Dictionary<string, object> payload = info.Payload;
        if (payload == null) return;
        if (payload.TryGetValue("save", out object result)
            && result is SaveData save)
        {
            m_current = save;
        }
    }
    public void OnGameSave(EventInfo info) 
    {
        HandleSaveData(info);
        // Autosave
        SaveStates(m_current);
        // Do on finish callback
        info.OnFinishedCallback?.Invoke();
    }
    public void OnQuitGame(EventInfo info) 
    {
        HandleSaveData(info);
        // Quit the game
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        SaveStates(m_current);
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    // Read Meta File states and Locations to update game state
    protected void LoadStates() 
    {
        if (!File.Exists($"{FileUtil.s_gamestateSavePath}/{s_gamestatePath}")) 
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
    protected void SaveStates(SaveData save) 
    {
        // if we are saving after finish an ending, increment new gamw counter
        SaveData newSave = new SaveData(save);
        string json = JsonConvert.SerializeObject(newSave);
        FileUtil.RawTextTo(FileUtil.s_gamestateSavePath, "saves","gamestate.json", new string[] { json });
    }
}