﻿using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
[Serializable]
public class SaveData
{
    [JsonConverter(typeof(StringEnumConverter))]
    public DoorState RoomLeftDoorState;
    // Where is the player looking at
    public string CurrentlyViewing;
    public AriaState AriaStatus;
    public List<ViewStateSaveData> ViewStates;

    public SaveData()
    {
        CurrentlyViewing = "RoomRight";
        AriaStatus = AriaState.Default;
        RoomLeftDoorState = DoorState.Closed;
        ViewStates = new List<ViewStateSaveData>();
    }

    public SaveData(SaveData copy)
    {
        CurrentlyViewing = copy.CurrentlyViewing;
        RoomLeftDoorState = copy.RoomLeftDoorState;
        AriaStatus = new AriaState(copy.AriaStatus);
        ViewStates = new List<ViewStateSaveData>(copy.ViewStates);
    }
    public SaveData(
        DoorState roomLeftDoorState,
        int newGameCount,
        int crashes,
        string viewing,
        AriaState ariaStatus, 
        List<ViewStateSaveData> viewStates)
    {
        CurrentlyViewing = viewing;
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
