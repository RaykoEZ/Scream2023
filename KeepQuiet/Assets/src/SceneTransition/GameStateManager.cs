using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ViewStateSaveData
{
    public string LocationName;
    public bool IsLightOn;
    public bool IsUnlocked;
    public Dictionary<string, bool> cluessToSpawn;
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] ViewStateManager m_view = default;

    // Read Meta File states and Locations to update game state
    void EvaluateGameState() { }
    void SaveGameStates() { }
    void LoadGameStates() { }
    void SetupAriaState() { }
}

