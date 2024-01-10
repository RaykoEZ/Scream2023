using System;
using UnityEngine;
[Serializable]
public class AriaState
{
    public bool SourceExists;
    public bool IsPossessed;
    public bool HasPastRunMemory;
    public int NumDenied = 0;
    public int Affection = 0;
    public int Sanity = 0;
    public string CurrentLocation;
    public static AriaState Default = new AriaState(true, false, false, 0, 1, 1, "OutsideCamera");

    public AriaState(
        bool sourceExists, 
        bool isPossessed, 
        bool hasPastRunMemory, 
        int numDenied, 
        int affection, 
        int sanity, string currentLocation)
    {
        SourceExists = sourceExists;
        IsPossessed = isPossessed;
        HasPastRunMemory = hasPastRunMemory;
        NumDenied = numDenied;
        Affection = affection;
        Sanity = sanity;
        CurrentLocation = currentLocation;
    }
}
public delegate void OnAriaMove(string newLocation);
public delegate void OnAriaLeaveGame();
public class Aria : Npc 
{
    [SerializeField] Animator m_anim = default;
    [SerializeField] DialogueNode m_introMessage = default;
    AriaState m_current;
    public AriaState Current => m_current;
    // When aria move from one to another
    public event OnAriaMove OnMove;
    // When aria leaves from all scene
    public event OnAriaLeaveGame OnLeave;
    public void Init(AriaState state) 
    {
        m_current = state;
    }   
    public void OnGameIntro() 
    {
        MessagePlayer(m_introMessage);
    }
    public override void OnCallAccepted()
    {
        throw new NotImplementedException();
    }

    public override void OnCallDenied()
    {
        m_current.NumDenied++;
        if (m_current.NumDenied > 3) 
        {
            AffectionDown(1);
        }
    }
    protected void AffectionDown(int val) 
    {
        m_current.Affection -= val;
        if (m_current.Affection < -10)
        {
            // If affecton is low, leave the player
            Leave();
        }
    }
    protected void Leave() 
    {
        m_current.CurrentLocation = "???";
        OnLeave?.Invoke();
    }
    protected void MoveTo(string newLocation)
    {
        m_current.CurrentLocation = newLocation;
        OnMove(newLocation);
    }
    public override void OnCallFinished()
    {
        throw new NotImplementedException();
    }

    public override void OnChatFinished()
    {
        throw new NotImplementedException();
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
