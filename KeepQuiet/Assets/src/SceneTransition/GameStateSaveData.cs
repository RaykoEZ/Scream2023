﻿using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class GameStateSaveData
{
    [JsonConverter(typeof(StringEnumConverter))]
    public DoorState RoomLeftDoorState;
    public int NewGameCount = 0;
    public int CrashCount = 0;
    // Where is the player looking at
    public string CurrentlyViewing;
    public AriaState AriaStatus;
    public List<ViewStateSaveData> ViewStates;
    public GameStateSaveData(
        DoorState roomLeftDoorState,
        int newGameCount,
        int crashes,
        AriaState ariaStatus, 
        List<ViewStateSaveData> viewStates)
    {
        RoomLeftDoorState = roomLeftDoorState;
        NewGameCount = newGameCount;
        CrashCount = crashes;
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

