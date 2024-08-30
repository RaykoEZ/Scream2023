using System;
using UnityEngine;
public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] GameSaveSource m_save = default;
    [SerializeField] LevelEventHandler m_level = default;
    public void ToTitle() 
    {
        m_save?.UpdateSave();
        m_level.ReturnToTitle();
    }
    // Going from title to game scene, save current data
    public void Continue()
    {
        m_save?.UpdateSave();
        m_level?.ContinueGame();
    }
    public void NewGame() 
    {
        m_save?.NewGame();
        m_level?.NewGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
