using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntruderAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Intruder Anomaly Settings")]
    [SerializeField] bool hasAudio;
    [SerializeField] AudioClip audioClip;
    [SerializeField] GameObject intruderPrefab;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
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

    public void ActivateAnomaly()
    {
        isActive = true;
        intruderPrefab.SetActive(true);
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        intruderPrefab.SetActive(false);
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }
}
