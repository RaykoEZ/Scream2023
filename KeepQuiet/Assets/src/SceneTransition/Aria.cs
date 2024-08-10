using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public enum AriaPosition
{ 
    Outside = 0,
    InsideCafe_Counter = 1,
    InsideCafe_Sit = 2,
    None = -1
}
[Serializable]
public class AriaState
{
    public bool IsPossessed;
    public bool HasPastRunMemory;
    [JsonConverter(typeof(StringEnumConverter))]   
    public AriaPosition CurrentLocation;
    public static AriaState Default = new AriaState(false, false, AriaPosition.None);
    public AriaState(
        bool isPossessed, 
        bool hasPastRunMemory, 
        AriaPosition currentLocation)
    {
        IsPossessed = isPossessed;
        HasPastRunMemory = hasPastRunMemory;
        CurrentLocation = currentLocation;
    }
    public AriaState(AriaState copy)
    {
        IsPossessed = copy.IsPossessed;
        HasPastRunMemory = copy.HasPastRunMemory;
        CurrentLocation = copy.CurrentLocation;
    }
}
public delegate void OnAriaMove(AriaPosition newLocation);
public delegate void OnAriaLeaveGame();
public class Aria : Npc 
{
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
}
