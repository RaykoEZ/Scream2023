using UnityEngine;
[CreateAssetMenu(fileName = "GameState_", menuName = "Game State Preset", order = 0)]
public class GameStateContainer : ScriptableObject 
{
    [SerializeField] GameStateSaveData m_state = default;
    public GameStateSaveData State => m_state;
}

