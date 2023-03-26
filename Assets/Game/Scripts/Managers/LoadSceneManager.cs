using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] GameStateChannelSO gameStateChannel;
    Coroutine loadSceneCoroutine;
    Coroutine loadUnloadScenesCoroutine;
    Coroutine resetSceneCoroutine;

    private void Start()
    {
        SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        gameStateChannel.OnLoadScene += LoadScene;
        gameStateChannel.OnUnloadScene += UnloadScene;
        gameStateChannel.OnLoadUnloadScene += LoadUnloadScenes;
        gameStateChannel.OnReloadScene += RetryLevel;
    }

    private void OnDisable()
    {
        gameStateChannel.OnLoadScene -= LoadScene;
        gameStateChannel.OnUnloadScene -= UnloadScene;
        gameStateChannel.OnLoadUnloadScene -= LoadUnloadScenes;
        gameStateChannel.OnReloadScene -= RetryLevel;
    }

    private void LoadScene(string name)
    {
        loadSceneCoroutine = StartCoroutine(LoadSceneCoroutine(name));
    }

    private IEnumerator LoadSceneCoroutine(string name)
    {
        yield return new WaitForFixedUpdate();
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        // wait until the asynchronous scene fully loads
        while (!loadScene.isDone)
            yield return null;
        gameStateChannel.SceneLoadedAction(name);
        ResetLoadCoroutine();
    }

    private void ResetLoadCoroutine()
    {
        if(loadSceneCoroutine != null)
        {
            StopCoroutine(loadSceneCoroutine);
            loadSceneCoroutine = null;
        }
    }

    private void UnloadScene(string name)
    {
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(name);
    }

    private void LoadUnloadScenes(string sceneToLoad, string sceneToUnload)
    {
        loadUnloadScenesCoroutine = StartCoroutine(LoadUnloadScenesCoroutine(sceneToLoad,sceneToUnload));
    }

    private IEnumerator LoadUnloadScenesCoroutine(string sceneToLoad, string sceneToUnload)
    {
        yield return new WaitForFixedUpdate();
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        // wait until the asynchronous scene fully loads
        while (!loadScene.isDone)
            yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(sceneToUnload);
        while (!unloadScene.isDone)
            yield return null;
        gameStateChannel.SceneLoadedAction(sceneToLoad);
        ResetLoadUnloadCoroutine();
    }

    private void ResetLoadUnloadCoroutine()
    {
        if(loadUnloadScenesCoroutine != null)
        {
            StopCoroutine(loadUnloadScenesCoroutine);
            loadUnloadScenesCoroutine = null;
        }
    }

    private void RetryLevel(string name)
    {
        resetSceneCoroutine = StartCoroutine(ResetSceneCoroutine(name));
    }

    private IEnumerator ResetSceneCoroutine(string sceneToReset)
    {
        yield return new WaitForFixedUpdate();
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(sceneToReset);
        while (!unloadScene.isDone)
            yield return null;
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneToReset, LoadSceneMode.Additive);
        while (!loadScene.isDone)
            yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToReset));
        gameStateChannel.SceneLoadedAction(sceneToReset);
        ResetRetryLevelCoroutine();
    }

    private void ResetRetryLevelCoroutine()
    {
        if (resetSceneCoroutine != null)
        {
            StopCoroutine(resetSceneCoroutine);
            resetSceneCoroutine = null;
        }
    }
}
