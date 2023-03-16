using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoodAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Blood Anomaly Settings")]
    [SerializeField] GameObject bloodObject;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] AnomalySO anomalyNames;


    private void Start()
    {
        anomalyType = anomalyNames.bloodAnomalyName;
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

    public void ActivateAnomaly()
    {
        isActive = true;
        bloodObject.SetActive(true);
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        bloodObject.SetActive(false);
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }
}
