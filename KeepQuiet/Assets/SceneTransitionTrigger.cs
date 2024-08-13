using Curry.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] CurryGameEventTrigger m_saveGameRequest = default;
    [SerializeField] CurryGameEventTrigger m_toTile = default;
    [SerializeField] CurryGameEventTrigger m_continue = default;
    [SerializeField] CurryGameEventTrigger m_newGame = default;
    [SerializeField] CurryGameEventTrigger m_quit = default;
    public void ToTitle() 
    {
        Action onFinish = () => 
        {
            m_toTile?.TriggerEvent();
        };
        m_saveGameRequest?.TriggerEvent(new EventInfo(null, onFinish));
    }
    public void Continue() 
    {
        EventInfo info = new EventInfo();
        m_continue?.TriggerEvent(info);
    }
    public void NewGame() 
    {
        EventInfo info = new EventInfo();
        m_newGame?.TriggerEvent(info);
    }
    public void QuitGame()
    {
        m_quit?.TriggerEvent();
    }
}
