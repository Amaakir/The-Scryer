using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateChannel",menuName ="Event Channels/Game State Channel")]
public class GameStateChannelSO : ScriptableObject
{
    public delegate void LoadSceneCallback(string name);
    public LoadSceneCallback OnLoadScene;

    public delegate void UnloadSceneCallback(string name);
    public UnloadSceneCallback OnUnloadScene;

    public delegate void LoadUnloadScenesCallback(string sceneToLoad, string sceneToUnload);
    public LoadUnloadScenesCallback OnLoadUnloadScene;

    public delegate void SceneLoadedCallback(string name);
    public SceneLoadedCallback OnSceneLoaded;

    public delegate void SetGameDifficultyCallback(string mode);
    public SetGameDifficultyCallback OnSetGameDifficulty;

    public delegate void UpdateGameTimeCallback(float time);
    public UpdateGameTimeCallback OnUpdateGameTime;

    public delegate void TriggerGameTimeCallback(bool value);
    public TriggerGameTimeCallback OnTriggerGameTime;

    public delegate void TriggerPlayerInteractionCallback(bool value);
    public TriggerPlayerInteractionCallback OnTriggerPlayerInteraction;

    public delegate void WinGameCallback();
    public WinGameCallback OnWinGame;

    public delegate void ReturnToMainMenuCallback();
    public ReturnToMainMenuCallback OnReturnToMainMenu;

    public delegate void ResetCurrentLevelCallback();
    public ResetCurrentLevelCallback OnResetCurrentLevel;

    public delegate void ReloadSceneCallback(string name);
    public ReloadSceneCallback OnReloadScene;

    public delegate void ResetCoreDataCallback();
    public ResetCoreDataCallback OnResetCoreData;

    public delegate void PlayJumpscareCallback();
    public PlayJumpscareCallback OnPlayJumpscare;

    public void LoadSceneAction(string name)
    {
        OnLoadScene?.Invoke(name);
    }

    public void UnloadSceneAction(string name)
    {
        OnUnloadScene?.Invoke(name);
    }

    public void LoadUnloadSceneAction(string sceneToLoad, string sceneToUnload)
    {
        OnLoadUnloadScene?.Invoke(sceneToLoad, sceneToUnload);
    }

    public void SceneLoadedAction(string name)
    {
        OnSceneLoaded?.Invoke(name);
    }

    public void SetGameDifficulty(string mode)
    {
        OnSetGameDifficulty?.Invoke(mode);
    }

    public void UpdateGameTimeAction(float time)
    {
        OnUpdateGameTime?.Invoke(time);
    }

    public void TriggerGameTimeAction(bool value)
    {
        OnTriggerGameTime?.Invoke(value);
    }

    public void TriggerPlayerInteractionAction(bool value)
    {
        OnTriggerPlayerInteraction?.Invoke(value);
    }

    public void WinGameAction()
    {
        OnWinGame?.Invoke();
    }

    public void ReturnToMainMenuAction()
    {
        OnReturnToMainMenu?.Invoke();
    }

    public void ResetCurrentLevelAction()
    {
        OnResetCurrentLevel?.Invoke();
    }

    public void ReloadSceneAction(string name)
    {
        OnReloadScene?.Invoke(name);
    }

    public void ResetCoreDataAction()
    {
        OnResetCoreData?.Invoke();
    }

    public void PlayJumpscareAction()
    {
        OnPlayJumpscare?.Invoke();
    }

}
