using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDisappearanceAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isShy;
    [SerializeField] bool isOnCooldown;
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Object Disappearance Anomaly Settings")]
    [SerializeField] bool replaceObjects;
    [SerializeField] GameObject[] objectsToDisappear;
    [SerializeField] GameObject[] objectsToReplaceWith;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] CameraChannelSO cameraChannel;
    [SerializeField] AnomalySO anomalyNames;

    private void Start()
    {
        anomalyType = anomalyNames.objDisappearanceAnomalyName;
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
        foreach(GameObject prefabs in objectsToDisappear)
        {
            prefabs.SetActive(false);
        }
        if (replaceObjects)
        {
            foreach(GameObject prefabs in objectsToReplaceWith)
            {
                prefabs.SetActive(true);
            }
        }
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        isOnCooldown = true;
        foreach (GameObject prefabs in objectsToDisappear)
        {
            prefabs.SetActive(true);
        }
        if (replaceObjects)
        {
            foreach (GameObject prefabs in objectsToReplaceWith)
            {
                prefabs.SetActive(false);
            }
        }
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }

}
