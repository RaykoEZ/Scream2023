using Newtonsoft.Json;
using UnityEngine;
using System.IO;
public class GameStateFileHandler : MonoBehaviour
{
    [SerializeField] CutsceneHandler m_cutscene = default;
    [SerializeField] GameStateManager m_state = default;
    // State to load upon first load
    [SerializeField] GameStateSaveData m_defaultState = default;
    GameStateSaveData m_current;
    static string s_gamestatePath = "saves/gamestate.json";
    private void Start()
    {
        LoadStates();
    }
    // init the game upon first load
    protected void FreshState() 
    {
        m_current = m_defaultState;
        m_state?.Init(m_current);
        m_cutscene.PlayIntro();
    }
    // Read Meta File states and Locations to update game state
    protected void LoadStates() 
    {
        if (!File.Exists(s_gamestatePath)) 
        {
            FreshState();
            return;
        }

        using (StreamReader r = new StreamReader($"{FileUtil.s_gamestateSavePath}/{s_gamestatePath}"))
        {
            string json = r.ReadToEnd();
            GameStateSaveData loaded = JsonConvert.DeserializeObject<GameStateSaveData>(json);
            m_current = loaded;
            // Before Start of game, init states from scratch or local files
            m_state?.Init(m_current);
        }       
    }
    protected void SaveStates(bool ending = false, bool crash = false) 
    {
        int crashCount = crash ? ++m_current.CrashCount : m_current.CrashCount;
        // if we are saving after finish an ending, increment new gamw counter
        int newGameCount = ending ? ++m_current.NewGameCount : m_current.NewGameCount;
        Aria aria = m_state.GetAria();
        GameStateSaveData newSave = new GameStateSaveData(
            m_state.LeftRoomDoor, newGameCount, crashCount, aria.Current, m_state.GetCurrentViewState());
        string json = JsonConvert.SerializeObject(newSave);
        FileUtil.RawTextTo(FileUtil.s_gamestateSavePath, "saves","gamestate.json", new string[] { json });
    }
}

