using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] LoadingScreenSequencer m_loadScreen = default;
    // Loads a single scene and sets it active 
    public void LoadScene(int sceneIndex, bool additive = false, bool setActive = true) 
    {
        if (sceneIndex < 0) 
        { 
            return; 
        }
        StartCoroutine(LoadLevel_Internal(sceneIndex, additive, setActive));
    }
    public void LoadScene(string sceneName, bool additive = false, bool setActive = true)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }
        StartCoroutine(LoadLevel_Internal(sceneName, additive, setActive));
    }
    IEnumerator LoadLevel_Internal(int sceneIndex, bool additive = false, bool setActive = true) 
    {
        // play transition animaton
        yield return m_loadScreen.FadeOut();
        AsyncOperation op;
        // start scene loading
        op = SceneManager.LoadSceneAsync(
            sceneIndex,
            additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        op.allowSceneActivation = false;
        // transition to new scene when loading and animation are done
        if (setActive)
        {
            yield return StartCoroutine(SetLoadedScene(op));
        }
    }
    IEnumerator LoadLevel_Internal(string sceneName, bool additive = false, bool setActive = true)
    {
        // play transition animaton
        yield return m_loadScreen.FadeOut();
        AsyncOperation op;
        op = SceneManager.LoadSceneAsync(
            sceneName,
            additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        op.allowSceneActivation = false;
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
        yield return new WaitForEndOfFrame();
        yield return m_loadScreen.FadeIn();
    }

    public void UnloadScene(string toUnload)
    {
        SceneManager.UnloadSceneAsync(toUnload);
    }
}