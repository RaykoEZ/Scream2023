﻿using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public enum AriaPosition
{ 
    InsideCafe_Entrance = 0,
    InsideCafe_Counter = 1,
    InsideCafe_Sit = 2,
    InsideCafe_Closeup = 3,
    RoomLeft_Peeking = 4,
    RoomLeft_CloseUp = 5,
    None = -1
}
[Serializable]
public class AriaState
{
    public bool IsPossessed;
    public bool HasPastRunMemory;
    public int Affection = 0;
    public int Sanity = 0;
    [JsonConverter(typeof(StringEnumConverter))]   
    public AriaPosition CurrentLocation;
    public static AriaState Default = new AriaState(false, false, 1, 1, AriaPosition.InsideCafe_Entrance);
    public AriaState(
        bool isPossessed, 
        bool hasPastRunMemory, 
        int affection, 
        int sanity, AriaPosition currentLocation)
    {
        IsPossessed = isPossessed;
        HasPastRunMemory = hasPastRunMemory;
        Affection = affection;
        Sanity = sanity;
        CurrentLocation = currentLocation;
    }
    public AriaState(AriaState copy)
    {
        IsPossessed = copy.IsPossessed;
        HasPastRunMemory = copy.HasPastRunMemory;
        Affection = copy.Affection;
        Sanity = copy.Sanity;
        CurrentLocation = copy.CurrentLocation;
    }
}
public delegate void OnAriaMove(AriaPosition newLocation);
public delegate void OnAriaLeaveGame();
public class Aria : Npc 
{
    [SerializeField] DialogueNode m_introFirstLoad = default;
    [SerializeField] DialEvent m_testDial = default;
    [SerializeField] AriaStateManager m_stateManager = default;
    public AriaState Current => m_stateManager.Current;
    // When aria move from one to another
    public event OnAriaMove OnMove;
    // When aria leaves from all scene
    public event OnAriaLeaveGame OnLeave;
    public void Init(SaveData state) 
    {
        m_stateManager.InitState(state);
    } 
    public void OnGameIntro() 
    {
        //MessagePlayer(m_introFirstLoad);
        //CallPlayer(m_testDial);

    }
    public override void OnCallDenied()
    {
        m_stateManager.OnDenied();
    }
    public void MoveTo(AriaPosition newLocation)
    {
        if (Current.CurrentLocation == newLocation) { return; }
        m_stateManager.MoveTo(newLocation);
        OnMove?.Invoke(newLocation);
    }
    protected void Leave() 
    {
        m_stateManager.Hide();
        OnLeave?.Invoke();
    }

    public override void OnPlayerCallCanceled()
    {
        throw new NotImplementedException();
    }

    public override void OnPlayerDecided(DialogueNode chosen, int choiceIndex)
    {
        throw new NotImplementedException();
    }

    public override void OnPlayerDialed()
    {
        throw new NotImplementedException();
    }
}
