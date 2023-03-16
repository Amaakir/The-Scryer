using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Room Settings")]
    [SerializeField] RoomSettingsSO[] mansionRooms;

    [Header("Event Channels")]
    [SerializeField] UIChannelSO uiChannel;
    [SerializeField] SoundChannelSO soundChannel;
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] CameraChannelSO cameraChannel;

    private string gameDifficulty = "Normal";

    private void Start()
    {
        InitGame();
    }

    private void OnEnable()
    {
        gameStateChannel.OnSceneLoaded += InitLoadedScene;
        gameStateChannel.OnSetGameDifficulty += SetGameDifficulty;
    }

    private void OnDisable()
    {
        gameStateChannel.OnSceneLoaded -= InitLoadedScene;
        gameStateChannel.OnSetGameDifficulty -= SetGameDifficulty;
    }

    private void InitGame()
    {
        gameStateChannel.OnLoadScene("MainMenu");
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
        gameStateChannel.OnLoadScene("GameplayUI");
    }

    private void SetGameDifficulty(string mode)
    {
        gameDifficulty = mode;
    }

    private void InitMainMenu()
    {
        soundChannel.PlayMainMenuMusicAction();
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
        gameStateChannel.TriggerGameTimeAction(true);
        cameraChannel.InitSceneCamerasAction();
        //anomalyChannel.StartAnomalyTimerAction();
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

}
