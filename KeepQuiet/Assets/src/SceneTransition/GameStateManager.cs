using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ViewStateSaveData
{
    public string LocationName;
    public bool IsLightOn;
    public bool IsUnlocked;
    public Dictionary<string, bool> itemsToSpawn;
}
[Serializable]
public class AriaState
{
    public bool SourceFileExists;
    public bool IsPossessed;
    public bool HasPastRunMemory;
    public string CurrentLocation;
    
}
public class GameStateManager : MonoBehaviour
{
    [SerializeField] ViewStateManager m_view = default;
    // Read Meta File states and Locations to update game state
    void EvaluateGameState() { }
    void SetupViewStats() { }
    void SetupAriaState() { }
}

public class Aria : Npc 
{
    [SerializeField] Animator m_anim = default;
    AriaState m_current;
    public void Init(AriaState state) 
    {
        m_current = state;
    }
    void EvaluateState() 
    { 
    
    }
    public override void OnCallAccepted()
    {
        throw new NotImplementedException();
    }

    public override void OnCallDenied()
    {
        throw new NotImplementedException();
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
