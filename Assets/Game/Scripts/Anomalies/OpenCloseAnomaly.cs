using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isShy;
    [SerializeField] bool isOnCooldown;
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("OpenClose Anomaly Settings")]
    [SerializeField] bool isBanging;
    [SerializeField] GameObject[] doorPrefabs;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] CameraChannelSO cameraChannel;
    [SerializeField] AnomalySO anomalyNames;


    private void Start()
    {
        anomalyType = anomalyNames.openCloseAnomalyName;
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
        PlayDoorAnimation();
    }

    private void PlayDoorAnimation()
    {
        if (!isBanging)
        {
            foreach(GameObject door in doorPrefabs)
            {
                door.GetComponent<Animator>().SetBool("Opened", true);
            }
        }
        else if (isBanging)
        {
            foreach (GameObject door in doorPrefabs)
            {
                door.GetComponent<Animator>().SetBool("Banging", true);
            }
        }
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        isOnCooldown = true;
        StopDoorAnimation();
    }

    private void StopDoorAnimation()
    {
        if (!isBanging)
        {
            foreach (GameObject door in doorPrefabs)
            {
                door.GetComponent<Animator>().SetBool("Opened", false);
            }
        }
        else if (isBanging)
        {
            foreach (GameObject door in doorPrefabs)
            {
                door.GetComponent<Animator>().SetBool("Banging", false);
            }
        }
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }
}
