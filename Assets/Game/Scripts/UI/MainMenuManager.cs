using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class MainMenuManager : MonoBehaviour
{
    [Header("Navigation Cameras")]
    [SerializeField] CinemachineVirtualCamera startCamera;
    [SerializeField] CinemachineVirtualCamera levelSelectCamera;

    [Header("Menu Parents")]
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject selectLevelUI;
    [SerializeField] GameObject loadingTextUI;

    [Header("Event Channels")]
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] SoundChannelSO soundChannel;

    private void Start()
    {
        InitMainMenu();
    }

    private void InitMainMenu()
    {
        MenuNavigation(1, 0, true, false, false);
    }

    private void MenuNavigation(int cameraMainPriority, int cameraLevelSelectPriority, 
        bool isMainMenuActive, bool isLevelSelectMenuActive, bool isLoadingTextActive)
    {
        startCamera.Priority = cameraMainPriority;
        levelSelectCamera.Priority = cameraLevelSelectPriority;
        mainMenuUI.SetActive(isMainMenuActive);
        selectLevelUI.SetActive(isLevelSelectMenuActive);
        loadingTextUI.SetActive(isLoadingTextActive);
    }

    public void OnClickStart()
    {
        MenuNavigation(0, 1, false, true, false);
    }

    public void OnClickBack()
    {
        MenuNavigation(1, 0, true, false, false);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickMansionNormal()
    {
        MenuNavigation(0, 1, false, false, true);
        gameStateChannel.SetGameDifficulty("Normal");
        gameStateChannel.OnLoadUnloadScene("Mansion", "MainMenu");
    }
}
