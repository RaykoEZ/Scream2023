using Curry.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
// A class to edit store and recieve save data from persistent save data handler
[Serializable]
public class SaveDataSource : MonoBehaviour
{
    public delegate void SaveDataUpdate(SaveData newSave);
    [SerializeField] CurryGameEventListener m_onDataRecieve = default;
    [SerializeField] CurryGameEventTrigger m_readyForData = default;
    static SaveData m_currentGameState = new SaveData();
    // Get New save data 
    public event SaveDataUpdate OnUpdate;
    public SaveData CurrentGameState => new SaveData(m_currentGameState);
    bool m_loading = false;
    // Set this flag if game save data changed and isn't loaded here
    public void RequestGameState() 
    {
        if (m_loading) return;
        m_loading = true;
        m_onDataRecieve?.Init();
        m_readyForData?.TriggerEvent(new EventInfo());
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
            OnUpdate?.Invoke(CurrentGameState);
            m_loading = false;
        }
    }
}