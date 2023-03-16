using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraObjAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Extra Object Anomaly Settings")]
    [SerializeField] GameObject extraObject;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] AnomalySO anomalyNames;

    private void Start()
    {
        anomalyType = anomalyNames.extraObjAnomalyName;
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
        extraObject.SetActive(true);
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        extraObject.SetActive(false);
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }
}
