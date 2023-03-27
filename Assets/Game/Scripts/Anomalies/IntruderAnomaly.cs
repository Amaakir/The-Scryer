using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntruderAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isShy;
    [SerializeField] bool isOnCooldown;
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Intruder Anomaly Settings")]
    [SerializeField] bool hasAudio;
    [SerializeField] AudioClip audioClip;
    [SerializeField] GameObject intruderPrefab;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] CameraChannelSO cameraChannel;
    [SerializeField] AnomalySO anomalyNames;

    private void Start()
    {
        anomalyType = anomalyNames.intruderAnomalyName;
    }
    public bool VerifyAnomaly(string anomalyGuess, string roomGuess)
    {
        if (isActive)
        {
            if (anomalyGuess == anomalyType && roomGuess == roomName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public bool ShyCheck()
    {
        if (isShy)
        {
            string currentRoom = cameraChannel.CompareRoomNameAction();
            if (currentRoom == roomName)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    public bool CooldownCheck()
    {
        if (isOnCooldown)
        {
            return true;
        }
        return false;

    }

    public void ActivateAnomaly()
    {
        isActive = true;
        intruderPrefab.SetActive(true);
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        isOnCooldown = true;
        intruderPrefab.SetActive(false);
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }
}
