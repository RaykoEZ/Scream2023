using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class ViewStateSaveData
{
    public bool IsLit;
    public string LocationName;
    public List<string> CluesToHide;
    // If player added something here, put it here
    public List<string> CluesToAdd;

    public ViewStateSaveData(
        bool isLit, 
        string locationName, 
        List<string> cluesToHide, List<string> cluesToAdd)
    {
        IsLit = isLit;
        LocationName = locationName;
        CluesToHide = cluesToHide;
        CluesToAdd = cluesToAdd;
    }
}
[Serializable]
public class GameStateSaveData
{
    [JsonConverter(typeof(StringEnumConverter))]
    public DoorState RoomLeftDoorState;
    public AriaState AriaStatus;
    public List<ViewStateSaveData> ViewStates;
    public GameStateSaveData(
        DoorState roomLeftDoorState, 
        AriaState ariaStatus, 
        List<ViewStateSaveData> viewStates)
    {
        RoomLeftDoorState = roomLeftDoorState;
        AriaStatus = ariaStatus;
        ViewStates = viewStates;
    }

    public Dictionary<string, ViewStateSaveData> GetViewStateCollection() 
    {
        var ret = new Dictionary<string, ViewStateSaveData>();
        foreach (var item in ViewStates) 
        {
            ret.Add(item.LocationName, item);
        }
        return ret;           
    }
    public bool TryGetViewState(string locationName, out ViewStateSaveData result) 
    {
        var collection = GetViewStateCollection();
        return collection.TryGetValue(locationName, out result);
    }
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] ViewStateManager m_view = default;
    [SerializeField] Aria m_aria = default;
    // State to load upon first load
    [SerializeField] GameStateSaveData m_defaultState = default;
    GameStateSaveData m_lastSnapshot;
    static string s_gamestatePath = "saves/gamestate.json";
    private void Start()
    {
        LoadStates();
    }
    // init the game upon first load
    protected void FreshState() 
    {
        m_lastSnapshot = m_defaultState;
        m_view?.Init(m_lastSnapshot);
        m_aria?.Init(m_lastSnapshot.AriaStatus);
    }
    // Read Meta File states and Locations to update game state
    protected void LoadStates() 
    {
        if (!File.Exists(s_gamestatePath)) 
        {
            FreshState();
            return;
        }

        using (StreamReader r = new StreamReader($"{FileUtil.s_gamestateSavePath}/{s_gamestatePath}"))
        {
            string json = r.ReadToEnd();
            GameStateSaveData loaded = JsonConvert.DeserializeObject<GameStateSaveData>(json);
            m_lastSnapshot = loaded;
            // Before Start of game, init states from scratch or local files
            m_view?.Init(m_lastSnapshot);
            m_aria?.Init(m_lastSnapshot.AriaStatus);
        }       
    }
    protected void SaveStates() 
    {
        GameStateSaveData newSave = new GameStateSaveData(
            m_view.LeftRoomDoor, m_aria.Current, m_view.GetCurrentViewState());
        string json = JsonConvert.SerializeObject(newSave);
        FileUtil.RawTextTo(FileUtil.s_gamestateSavePath, "saves","gamestate.json", new string[] { json });
    }
}

