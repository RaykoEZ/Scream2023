using Curry.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] GameStateManager m_gameState = default;
    [SerializeField] CurryGameEventTrigger m_toTile = default;
    [SerializeField] CurryGameEventTrigger m_continue = default;
    [SerializeField] CurryGameEventTrigger m_newGame = default;
    [SerializeField] CurryGameEventTrigger m_quit = default;
    public void ToTitle() 
    {
        Action onFinish = () => 
        {
            m_toTile?.TriggerEvent(new EventInfo());
        };
        m_gameState?.SaveGameState(onFinish);
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
    public void QuitGame(bool crash = false, bool ending = false)
    {
        // need reference to game state manager
        // to save data before quitting the game
        Dictionary<string, object> payload = new Dictionary<string, object>
        {{"state", m_gameState}, {"ending", ending }, {"crash", crash } };
        EventInfo info = new EventInfo(payload);
        m_quit?.TriggerEvent(info);
    }
}
