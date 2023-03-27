using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isShy;
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Light Anomaly Settings")]
    [SerializeField] GameObject[] lightsToTrigger;
    [SerializeField] GameObject[] prefabsToTrigger;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] CameraChannelSO cameraChannel;
    [SerializeField] AnomalySO anomalyNames;

    private void Start()
    {
        anomalyType = anomalyNames.lightAnomalyName;
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

    public void DeactivateAnomaly()
    {
        isActive = false;
        foreach (GameObject light in lightsToTrigger)
        {
            light.SetActive(!light.activeInHierarchy);
        }
        foreach (GameObject prefab in prefabsToTrigger)
        {
            prefab.SetActive(!prefab.activeInHierarchy);
        }
    }

    public void ActivateAnomaly()
    {
        isActive = true;
        foreach(GameObject light in lightsToTrigger)
        {
            light.SetActive(!light.activeInHierarchy);
        }
        foreach(GameObject prefab in prefabsToTrigger)
        {
            prefab.SetActive(!prefab.activeInHierarchy);
        }
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }
}
