using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isShy;
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Picture Anomaly Settings")]
    [SerializeField] GameObject originalPicture;
    [SerializeField] GameObject anomalyPicture;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] CameraChannelSO cameraChannel;
    [SerializeField] AnomalySO anomalyNames;

    private void Start()
    {
        anomalyType = anomalyNames.pictureAnomalyName;
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
        originalPicture.SetActive(false);
        anomalyPicture.SetActive(true);
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        originalPicture.SetActive(true);
        anomalyPicture.SetActive(false);
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }

}
