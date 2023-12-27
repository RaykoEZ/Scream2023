using System;
using UnityEngine;
// Stores info about a view (e.g. currently viewing? visible clues, clue/puzzle states)
[Serializable]
public class ViewState 
{

}
// handles behaviours for a view
public abstract class ViewStateManager : MonoBehaviour
{
    [SerializeField] ScreenFade m_fade = default;

    public abstract void Show();
    public abstract void Hide();
}


public class GameStateManager : MonoBehaviour
{
    [SerializeField] ViewStateManager m_view = default;

}
