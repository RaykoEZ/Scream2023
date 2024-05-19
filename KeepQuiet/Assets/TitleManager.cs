using UnityEngine;
using UnityEngine.Playables;
// play sequence for title
public class TitleManager : MonoBehaviour
{
    [SerializeField] PlayableAsset m_default = default;
    [SerializeField] PlayableAsset m_freedomRoute = default;
    [SerializeField] PlayableAsset m_freedomEnd = default;
    [SerializeField] PlayableAsset m_deadEnd = default;

    [SerializeField] SequencePlayer m_title = default;
    [SerializeField] SaveDataSource m_saveData = default;
    void OnEnable()
    {
        m_saveData.OnUpdate += OnGameDataReceive;
    }
    void OnDisable()
    {
        m_saveData.OnUpdate -= OnGameDataReceive;
    }
    void Start()
    {
        // Get current game state
        m_saveData.RequestGameState();
    }
    void OnGameDataReceive(SaveData save) 
    {
        // If player killed Aria, show dead sequence, no music

        // If bad end (alter ego leaked into Aria and escaped), glitches sequence 
       
        // if on true end route, play special sequence

        // if true end reached, white aria close eye, calmer music
        m_title.PlaySequence();
    }
    // Change title screen behaviour depending on game state
    public void OnTitleLoaded()
    {

    }
}