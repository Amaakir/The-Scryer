using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMovementAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("Object Movement Anomaly Settings")]
    [SerializeField] Animator objectAnimator;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
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

    public void ActivateAnomaly()
    {
        isActive = true;
        objectAnimator.SetBool("Active", true);
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        objectAnimator.SetBool("Active", false);
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }

}
