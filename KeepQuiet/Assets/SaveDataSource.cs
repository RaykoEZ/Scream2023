using Curry.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
// A class to edit store and recieve save data from persistent save data handler
// Only one instance of this in an active scene
[Serializable]
public class SaveDataSource : MonoBehaviour
{
    public delegate void SaveDataUpdate(SaveData newSave);
    public delegate void SaveDataRequest();
    // Recieve save data from game state SaveFileHandler
    [SerializeField] CurryGameEventListener m_onSaveLoaded = default;
    [SerializeField] CurryGameEventTrigger m_loadSaveRequest = default;
    // Game save needs current state for saving game to file
    [SerializeField] CurryGameEventListener m_syncSave = default;
    // Save game to file
    [SerializeField] CurryGameEventTrigger m_saveGameRequest = default;
    static SaveData m_currentGameState = new SaveData();
    // Get New save data 
    public event SaveDataUpdate OnRefresh;
    public SaveData CurrentGameState => new SaveData(m_currentGameState);
    bool m_loading = false;
    void OnEnable()
    {
        m_onSaveLoaded?.Init();
        m_syncSave?.Init();
    }
    void OnDisable()
    {
        m_onSaveLoaded?.Shutdown();
        m_syncSave?.Shutdown();
    }
    // Set this flag if game save data changed and isn't loaded here
    public void RequestLoadSave() 
    {
        if (m_loading) return;
        m_loading = true;
        m_loadSaveRequest?.TriggerEvent();
    }
    public void SaveGameToFile(Action onFinish = null) 
    {
        Dictionary<string, object> payload = new Dictionary<string, object>
        {{"save", CurrentGameState }};
        EventInfo info = new EventInfo(payload, onFinishCallback: onFinish);
        m_saveGameRequest?.TriggerEvent(info);
    }
    public void SyncSave(EventInfo info) 
    {
        SaveGameToFile(info.OnFinishedCallback);
    }
    public void OnRecieve(EventInfo info) 
    {
        Dictionary<string, object> payload = info.Payload;
        if (payload == null) return;
        if (payload.TryGetValue("save", out object result)
            && result is SaveData save)
        {
            // update static game state
            m_currentGameState = save;
            // Send new copy to listeners
            OnRefresh?.Invoke(CurrentGameState);
            m_loading = false;
        }
    }
}