using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ViewStateSaveData
{
    public bool IsLit;
    public string LocationName;
    public List<string> CluesToHide;
    // If player added something here, put it here
    public List<string> CluesToAdd;
}
[Serializable]
public class GameStateSaveData
{
    public DoorState RoomLeftDoorState;
    public AriaState AriaStatus;
    public List<ViewStateSaveData> ViewStates;
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
    // Read Meta File states and Locations to update game state
    void LoadStates() 
    {
        // Before Start of game, init states from scratch or local files
        GameStateSaveData newSave = new GameStateSaveData();
        m_view?.Init(newSave);
        m_aria?.Init(newSave.AriaStatus);
    }

    void SaveStates() { }
}

