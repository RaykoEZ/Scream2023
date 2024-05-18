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
    static bool m_isDirty = true;
    // Get New save data 
    public event SaveDataUpdate OnUpdate;
    public SaveData CurrentGameState => new SaveData(m_currentGameState);
    // Set this flag if game save data changed and isn't loaded here
    public static bool IsDirty => m_isDirty;
    public static void SetDirty() 
    {
        m_isDirty = true;
    }
    public void RequestGameState() 
    {
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
            m_isDirty = false;
            // Send new copy to listeners
            OnUpdate?.Invoke(CurrentGameState);
        }
    }
}