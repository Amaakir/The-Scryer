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
    [SerializeField] UIChannelSO uiChannel;

    Coroutine startLevelCoroutine;

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
        startLevelCoroutine = StartCoroutine(StartLevel("Mansion"));
    }

    private IEnumerator StartLevel(string name)
    {
        yield return new WaitForFixedUpdate();
        uiChannel.TriggerLoadingScreenAction(true);
        yield return new WaitForFixedUpdate();
        MenuNavigation(0, 1, false, false, true);
        yield return new WaitForFixedUpdate();
        gameStateChannel.SetGameDifficulty("Normal");
        gameStateChannel.OnLoadUnloadScene(name, "MainMenu");
        yield return new WaitForFixedUpdate();
        ResetStartLevelCoroutine();
    }

    private void ResetStartLevelCoroutine()
    {
        if (startLevelCoroutine != null)
        {
            StopCoroutine(startLevelCoroutine);
            startLevelCoroutine = null;
        }
    }
}
