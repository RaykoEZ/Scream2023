using Curry.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] CurryGameEventTrigger m_continue = default;
    [SerializeField] CurryGameEventTrigger m_newGame = default;
    [SerializeField] CurryGameEventTrigger m_quit = default;
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
        EventInfo info = new EventInfo();
        m_quit?.TriggerEvent(info);
    }
}
