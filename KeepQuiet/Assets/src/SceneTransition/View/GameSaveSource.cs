using UnityEngine;
using UnityEngine.Events;
// stores current writable save data, saves game to file/cache
public class GameSaveSource : MonoBehaviour 
{
    [SerializeField] GameStateFileHandler m_saveState = default;
    [SerializeField] UnityEvent m_onSaveGame = default; 
    SaveData m_currentGameState;
    public SaveData Current => m_currentGameState;
    public void Init(SaveData save)
    {
        m_currentGameState = save;
    }
    // Update game save cache, may save to file if needed
    public void UpdateSave(bool saveToFile = false)
    {
        // Call all save data updates before saving the game
        m_onSaveGame?.Invoke();
        m_saveState?.UpdateSave(m_currentGameState, saveToFile);
    }
    public void NewGame() 
    {
        m_saveState?.SetupNewGame();
    }
}
