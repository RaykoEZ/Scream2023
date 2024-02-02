using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator m_transition = default;
    // Loads a single scene and sets it active 
    public void LoadScene(int sceneIndex) 
    {
        if (sceneIndex < 0) 
        { 
            return; 
        }
        StartCoroutine(LoadLevel_Internal(sceneIndex));
    }
    public void LoadScene(string sceneName, bool additive = false, bool setActive = false)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }
        StartCoroutine(LoadLevel_Internal(sceneName, additive, setActive));
    }
    IEnumerator LoadLevel_Internal(int sceneIndex) 
    {
        AsyncOperation op;
        // start scene loading
        op = SceneManager.LoadSceneAsync(sceneIndex);
        op.allowSceneActivation = false;
        // play transition animaton
        m_transition?.SetTrigger("transition");
        // transition to new scene when loading and animation are done
        yield return StartCoroutine(SetLoadedScene(op));
    }
    IEnumerator LoadLevel_Internal(string sceneName, bool additive = false, bool setActive = false)
    {
        AsyncOperation op;
        op = SceneManager.LoadSceneAsync(
            sceneName,
            additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        op.allowSceneActivation = false;
        // play transition animaton
        m_transition?.SetTrigger("transition");
        if (setActive)
        {
            yield return StartCoroutine(SetLoadedScene(op));
        }
    }
    IEnumerator SetLoadedScene(AsyncOperation op) 
    {
        // transition to new scene when loading and animation are done
        yield return new WaitUntil(() => op.progress >= 0.9f);
        op.allowSceneActivation = true;
    }

    public void UnloadScene(string toUnload)
    {
        SceneManager.UnloadSceneAsync(toUnload);
    }
}