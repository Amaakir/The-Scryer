using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMovementAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isShy;
    [SerializeField] bool isOnCooldown;
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Object Movement Anomaly Settings")]
    [SerializeField] Animator objectAnimator;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] CameraChannelSO cameraChannel;
    [SerializeField] AnomalySO anomalyNames;

    private void Start()
    {
        anomalyType = anomalyNames.objMovementAnomalyName;
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
        objectAnimator.SetBool("Active", true);
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        isOnCooldown = true;
        objectAnimator.SetBool("Active", false);
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }

}
