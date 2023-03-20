using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Unlocks")]
    [SerializeField] bool nightmareMansion = false;

    [Header("Room Settings")]
    [SerializeField] RoomSettingsSO[] mansionRooms;

    [Header("Event Channels")]
    [SerializeField] UIChannelSO uiChannel;
    [SerializeField] SoundChannelSO soundChannel;
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] CameraChannelSO cameraChannel;

    private string gameDifficulty = "Normal";
    private string currentLevel = "Menu";

    private void Start()
    {
        InitGame();
    }

    private void OnEnable()
    {
        gameStateChannel.OnSceneLoaded += InitLoadedScene;
        gameStateChannel.OnSetGameDifficulty += SetGameDifficulty;
        gameStateChannel.OnWinGame += WinGame;
        gameStateChannel.OnReturnToMainMenu += ReturnToMainMenu;
    }

    private void OnDisable()
    {
        gameStateChannel.OnSceneLoaded -= InitLoadedScene;
        gameStateChannel.OnSetGameDifficulty -= SetGameDifficulty;
        gameStateChannel.OnWinGame -= WinGame;
        gameStateChannel.OnReturnToMainMenu -= ReturnToMainMenu;
    }

    private void InitGame()
    {
        gameStateChannel.LoadSceneAction("MainMenu");
    }

    private void InitLoadedScene(string name)
    {
        switch (name)
        {
            case "MainMenu":
                InitMainMenu();
                break;
            case "GameplayUI":
                PlayNewSceneMusic();
                break;
            case "Mansion":
                InitMansion();
                LoadGameplayUI();
                break;
            default:
                Debug.LogError("Something went wrong here");
                break;
        }
    }

    private void LoadGameplayUI()
    {
        gameStateChannel.LoadSceneAction("GameplayUI");
    }

    private void SetGameDifficulty(string mode)
    {
        gameDifficulty = mode;
    }

    private void InitMainMenu()
    {
        soundChannel.PlayMainMenuMusicAction();
        gameStateChannel.ResetCoreDataAction();
        currentLevel = "Menu";
    }

    private int SelectRooms()
    {
        if (gameDifficulty == "Nightmare")
        {
            return 8;
        }
        else
        {
            return 5;
        }
    }

    private void InitMansion()
    {
        SpawnRoomCameras();
        currentLevel = "Mansion";
        gameStateChannel.TriggerGameTimeAction(true);
        cameraChannel.InitSceneCamerasAction();
    }

    private void SpawnRoomCameras()
    {
        int roomsToSpawn = SelectRooms();

        for (int i = 0; i < roomsToSpawn; i++)
        {
            cameraChannel.InstantiateRoomCameraAction(mansionRooms[i].roomCamera, mansionRooms[i].roomName);
        }
    }

    private void PlayNewSceneMusic()
    {
        soundChannel.OnStopAllAudioAction();
        soundChannel.OnPlayMansionSceneMusicAction();//Refactor para que cambie la musica segun la escena y usarlo en InitMainMenu
    }

    private void WinGame()
    {
        gameStateChannel.TriggerPlayerInteractionAction(false);
        anomalyChannel.StopSpawnTimerAction();
        soundChannel.StopAllAudioAction();
        soundChannel.PlayTimeTickSFXAction();
        soundChannel.PlayWinScreenMusicAction();
        HandleUnlocks();
    }

    private void HandleUnlocks()
    {
        switch (currentLevel)
        {
            case "Mansion":
                if (!nightmareMansion)
                {
                    nightmareMansion = true;
                }
                break;
            default:
                Debug.Log("Nothing to unlock...");
                break;
        }
    }

    private void ReturnToMainMenu()
    {
        gameStateChannel.UnloadSceneAction(currentLevel);
        gameStateChannel.LoadUnloadSceneAction("MainMenu", "GameplayUI");
    }

}
