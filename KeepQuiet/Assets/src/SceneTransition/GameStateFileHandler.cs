using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using Curry.Events;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
using System;
// Script for persistent game state loading and saving
// Loads persistent game state into game state manager in scene
// Saves updated states coming from game state in scene
public class GameStateFileHandler : MonoBehaviour
{
    // State to load upon first load
    [SerializeField] GameStateContainer m_defaultState = default;
    [SerializeField] UnityEvent m_readyGameLaunch = default;
    [SerializeField] CurryGameEventListener m_exitGame = default;
    [SerializeField] CurryGameEventListener m_onSaveGame = default;
    [SerializeField] CurryGameEventListener m_onLoadGame = default;
    [SerializeField] CurryGameEventTrigger m_saveLoaded = default;
    [SerializeField] CurryGameEventTrigger m_syncSave = default;
    SaveData m_current;
    static bool m_saveInProgress = false;
    static string s_gamestatePath = "saves/gamestate.json";
    public SaveData Current { get => m_current; }
    private void Start()
    {
        m_onLoadGame?.Init();
        m_exitGame?.Init();
        m_onSaveGame?.Init();
        LoadFromFile();
        // Start game launch sequence when game is ready
        m_readyGameLaunch?.Invoke();
    }
    private void OnApplicationQuit()
    {
        // Autosave on quitting
        SaveToFile(m_current);
    }
    public void LoadGame()
    {
        LoadFromFile();
        Dictionary<string, object> payload = new Dictionary<string, object>
        {{"save", new SaveData(m_current)}};
        EventInfo info = new EventInfo(payload);
        m_saveLoaded?.TriggerEvent(info);
    }
    // Set a new game with persistent kept
    public void SetupNewGame()
    {            
        // copy persisting save from current
        SaveData.PersistentSave persist = new SaveData.PersistentSave(m_current.Persistent);
        // reset game state to new game
        m_current = new SaveData(m_defaultState.State);
        // set persistent save states
        m_current.Persistent = persist;
        SaveToFile(m_current);
    }
    public void OnGameReady()
    {
        LoadGame();
    }
    // sets valid incoming save data
    SaveData HandleSave(EventInfo info) 
    {
        Dictionary<string, object> payload = info.Payload;
        if (payload == null) return null;
        if (payload.TryGetValue("save", out object result)
            && result is SaveData save)
        {
            return save;
        }
        else return null;
    }
    // incoming game save data
    public void OnGameSave(EventInfo info) 
    {
        SaveData result = HandleSave(info);
        if (result != null) 
        {
            m_current = result;
        }
        // Do on finish callback
        info.OnFinishedCallback?.Invoke();
        m_saveInProgress = false;
    }
    public IEnumerator SaveGame(Action onFinish = null) 
    {
        m_saveInProgress = true;
        m_syncSave?.TriggerEvent();
        // Wait for save and quit when finished
        yield return new WaitUntil(() => !m_saveInProgress);
        SaveToFile(m_current);
        yield return new WaitForEndOfFrame();
        onFinish?.Invoke();
    }
    public void OnQuitGame()
    {
        // Quit the game
        StartCoroutine(SaveGame(Quit_Internal));
    }
    void Quit_Internal() 
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        SaveToFile(m_current);
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    // Read Meta File states and Locations to update game state
    protected void LoadFromFile() 
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
    protected void SaveToFile(SaveData save) 
    {
        // if we are saving after finish an ending, increment new gamw counter
        SaveData newSave = new SaveData(save);
        string json = JsonConvert.SerializeObject(newSave);
        FileUtil.RawTextTo(FileUtil.s_gamestateSavePath, "saves","gamestate.json", new string[] { json });
    }
}