using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public enum AriaPosition
{ 
    OutsideCam = 0,
    OutsideFirstPerson = 1,
    InsideCafe_Entrance = 2,
    InsideCafe_Counter = 3,
    InsideCafe_Sit = 4,
    InsideCafe_Closeup = 5,
    RoomLeft_Peeking = 6,
    RoomLeft_CloseUp = 7,
    None = -1
}
[Serializable]
public class AriaState
{
    public bool SourceExists;
    public bool IsPossessed;
    public bool HasPastRunMemory;
    public int NumDenied = 0;
    public int Affection = 0;
    public int Sanity = 0;
    [JsonConverter(typeof(StringEnumConverter))]   
    public AriaPosition CurrentLocation;
    public static AriaState Default = new AriaState(true, false, false, 0, 1, 1, AriaPosition.OutsideCam);
    public AriaState(
        bool sourceExists, 
        bool isPossessed, 
        bool hasPastRunMemory, 
        int numDenied, 
        int affection, 
        int sanity, AriaPosition currentLocation)
    {
        SourceExists = sourceExists;
        IsPossessed = isPossessed;
        HasPastRunMemory = hasPastRunMemory;
        NumDenied = numDenied;
        Affection = affection;
        Sanity = sanity;
        CurrentLocation = currentLocation;
    }
    public AriaState(AriaState copy)
    {
        SourceExists = copy.SourceExists;
        IsPossessed = copy.IsPossessed;
        HasPastRunMemory = copy.HasPastRunMemory;
        NumDenied = copy.NumDenied;
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
    public void Init(GameStateSaveData state) 
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
        m_stateManager.Leave();
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
