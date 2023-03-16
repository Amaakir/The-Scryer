using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> anomalies = new List<GameObject>();
    [SerializeField] List<GameObject> activeAnomalies = new List<GameObject>();

    [Header("Event Channels")]
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] UIChannelSO uiChannel;

    private int totalAnomalies;
    private int detectedAnomalies;

    private void Start()
    {
        InitAnomalyManager();
    }

    private void InitAnomalyManager()
    {
        totalAnomalies = 0;
        detectedAnomalies = 0;
    }

    private void OnEnable()
    {
        anomalyChannel.OnSpawnAnomaly += SpawnAnomaly;
        anomalyChannel.OnSendAnomalyGuess += AnomalyGuess;
    }

    private void OnDisable()
    {
        anomalyChannel.OnSpawnAnomaly -= SpawnAnomaly;
        anomalyChannel.OnSendAnomalyGuess -= AnomalyGuess;
    }

    private void SpawnAnomaly()
    {
        GameObject anomalyToSpawn = SearchForInactiveAnomaly();

        anomalyToSpawn.GetComponent<IAnomaly>().ActivateAnomaly();
        activeAnomalies.Add(anomalyToSpawn);
        anomalies.Remove(anomalyToSpawn);
        totalAnomalies++;
    }

    private GameObject SearchForInactiveAnomaly()
    {
        int randomAnomalyIndex = UnityEngine.Random.Range(0, anomalies.Count);
        Debug.Log($"Inactive anomalies: {anomalies.Count}. Rolling {randomAnomalyIndex}");

        if (!anomalies[randomAnomalyIndex].GetComponent<IAnomaly>().IsAnomalyActive())
        {
            Debug.Log($"Spawning Anomaly {anomalies[randomAnomalyIndex].transform.name} with roll {randomAnomalyIndex}");
            return anomalies[randomAnomalyIndex];
        }
        else
        {
            Debug.Log("Anomaly not found or already active!");
            return null;
        }   
    }

    private void AnomalyGuess(string anomalyGuess, string roomGuess)
    {
        Debug.Log($"Guessing {anomalyGuess} at {roomGuess}");

        if(activeAnomalies.Count == 0)
        {
            NoAnomalyFound();
            return;
        }

        for(int i=0; i < activeAnomalies.Count; i++)
        {
            if (activeAnomalies[i].GetComponent<IAnomaly>().VerifyAnomaly(anomalyGuess, roomGuess))
            {
                detectedAnomalies++;
                uiChannel.PlayFixAnomalyScreenAction();
                activeAnomalies[i].GetComponent<IAnomaly>().DeactivateAnomaly();
                anomalies.Add(activeAnomalies[i]);
                activeAnomalies.Remove(activeAnomalies[i]);
                return;
            }
        }

        NoAnomalyFound();
    }

    private void NoAnomalyFound()
    {
        uiChannel.DisplayErrorMessageAction("No anomaly of that type found...");
    }
}
