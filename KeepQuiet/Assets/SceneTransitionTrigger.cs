using Curry.Events;
using System;
using UnityEngine;
public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] SaveDataSource m_saveData = default;
    [SerializeField] CurryGameEventTrigger m_resetSave = default;
    [SerializeField] CurryGameEventTrigger m_toTile = default;
    [SerializeField] CurryGameEventTrigger m_continue = default;
    [SerializeField] CurryGameEventTrigger m_newGame = default;
    public void ToTitle() 
    {
        Action onFinish = () => 
        {
            m_toTile?.TriggerEvent();
        };
        m_saveData?.SaveGameToFile(onFinish);
    }
    // Going from title to game scene, save current data
    public void Continue()
    {
        Action onFinish = () =>
        {
            m_continue?.TriggerEvent();
        };
        m_saveData?.SaveGameToFile(onFinish);
    }
    public void NewGame() 
    {
        Action onFinish = () =>
        {
            m_newGame?.TriggerEvent();
        };
        m_resetSave?.TriggerEvent( new EventInfo(null, onFinish));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
