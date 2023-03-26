using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDisappearanceAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isShy;
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Object Disappearance Anomaly Settings")]
    [SerializeField] GameObject objectToDisappear;

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

    public void ActivateAnomaly()
    {
        isActive = true;
        objectToDisappear.SetActive(false);
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        objectToDisappear.SetActive(true);
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }

}
