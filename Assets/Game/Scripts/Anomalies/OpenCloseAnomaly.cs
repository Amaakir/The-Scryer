using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseAnomaly : MonoBehaviour, IAnomaly
{
    [Header("Anomaly Info")]
    [SerializeField] bool isActive = false;
    [SerializeField] string anomalyType;
    [SerializeField] string roomName;

    [Header("OpenClose Anomaly Settings")]
    [SerializeField] bool isBanging;
    [SerializeField] GameObject[] doorPrefabs;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] AnomalySO anomalyNames;


    private void Start()
    {
        anomalyType = anomalyNames.openCloseAnomalyName;
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
        PlayDoorAnimation();
    }

    private void PlayDoorAnimation()
    {
        if (!isBanging)
        {
            foreach(GameObject door in doorPrefabs)
            {
                door.GetComponent<Animator>().SetBool("Opened", true);
            }
        }
        else if (isBanging)
        {
            foreach (GameObject door in doorPrefabs)
            {
                door.GetComponent<Animator>().SetBool("Banging", true);
            }
        }
    }

    public void DeactivateAnomaly()
    {
        isActive = false;
        StopDoorAnimation();
    }

    private void StopDoorAnimation()
    {
        if (!isBanging)
        {
            foreach (GameObject door in doorPrefabs)
            {
                door.GetComponent<Animator>().SetBool("Opened", false);
            }
        }
        else if (isBanging)
        {
            foreach (GameObject door in doorPrefabs)
            {
                door.GetComponent<Animator>().SetBool("Banging", false);
            }
        }
    }

    public bool IsAnomalyActive()
    {
        return isActive;
    }
}
