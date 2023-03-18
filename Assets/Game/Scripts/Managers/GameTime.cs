using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [Header("Game Time Settings")]
    [SerializeField] bool isTime = false;
    [SerializeField] float gameTime = 0;
    [SerializeField] bool hasGameIntro = false;
    [SerializeField] float timeBeforeAnomalies;
    [SerializeField] float winTime = 360;
    public float timeScale = 0.25f;
    float playAudioEvery = 60;
    float minutesPassed = 0;


    [Header("Sounds")]
    [SerializeField] AudioClipDataSO timePassAudio;

    [Header("Event Channels")]
    [SerializeField] UIChannelSO uiChannel;
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] SoundChannelSO soundChannel;

    private void Start()
    {
        InitGameTime();
    }

    private void InitGameTime()
    {
        isTime = false;
        hasGameIntro = false;
        minutesPassed += playAudioEvery;
    }

    private void OnEnable()
    {
        gameStateChannel.OnTriggerGameTime += TriggerGameTime;
    }

    private void OnDisable()
    {
        gameStateChannel.OnTriggerGameTime -= TriggerGameTime;
    }

    private void Update()
    {
        if (isTime)
        {
            ProcessTime();
        }
    }

    private void TriggerGameTime(bool value)
    {
        isTime = value;
    }
 
    private void ProcessTime()
    {
        gameTime += Time.deltaTime * timeScale;
        gameStateChannel.UpdateGameTimeAction(gameTime);
        
        if (!hasGameIntro)
        {
            CheckIntroTimer();
        }

        if(gameTime > minutesPassed)
        {
            minutesPassed += playAudioEvery;
            soundChannel.PlayAudioAction(timePassAudio);
        }

        CheckWinTime();
    }

    private void CheckIntroTimer()
    {
        if(gameTime > timeBeforeAnomalies)
        {
            hasGameIntro = true;
            uiChannel.StartGameIntroAction();
        }
    }

    private void CheckWinTime()
    {
        if(gameTime > winTime)
        {
            TriggerGameTime(false);
            gameStateChannel.UpdateGameTimeAction(winTime);
            gameStateChannel.OnWinGame();
        }
    }
}
