using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Object Disappearance Anomaly Settings")]
    [SerializeField] GameObject objectToDisappear;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] AnomalySO anomalyNames;

    public bool VerifyAnomaly(string anomalyGuess, string roomGuess)
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

    public void DeactivateAnomaly()
    {
        throw new System.NotImplementedException();
    }

    public void ActivateAnomaly()
    {
        throw new System.NotImplementedException();
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }
}
