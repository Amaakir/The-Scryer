using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] TextMeshProUGUI timerText;

    [Header("Event Channels")]
    [SerializeField] GameStateChannelSO gameStateChannel;

    private void OnEnable()
    {
        gameStateChannel.OnUpdateGameTime += DisplayTime;
    }

    private void OnDisable()
    {
        gameStateChannel.OnUpdateGameTime -= DisplayTime;
    }

    private void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
