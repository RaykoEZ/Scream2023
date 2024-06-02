using UnityEngine;
[CreateAssetMenu(fileName = "GameState_", menuName = "Game State Preset", order = 0)]
public class GameStateContainer : ScriptableObject 
{
    [SerializeField] SaveData m_state = default;
    public SaveData State => m_state;
}

