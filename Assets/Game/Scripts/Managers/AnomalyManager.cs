using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    [SerializeField] bool isWarning = false;
    [SerializeField] bool isGameOver = false;
    [SerializeField] int numberForWarning;
    [SerializeField] int numberForGameOver;
    [SerializeField] List<GameObject> anomalies = new List<GameObject>();
    [SerializeField] List<GameObject> activeAnomalies = new List<GameObject>();

    [Header("Event Channels")]
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] UIChannelSO uiChannel;
    [SerializeField] SoundChannelSO soundChannel;

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
        isWarning = false;
        isGameOver = false;
    }

    private void OnEnable()
    {
        anomalyChannel.OnSpawnAnomaly += SpawnAnomaly;
        anomalyChannel.OnSendAnomalyGuess += AnomalyGuess;
        anomalyChannel.OnGetTotalAnomaliesFixed += GetTotalAnomaliesFixed;
    }

    private void OnDisable()
    {
        anomalyChannel.OnSpawnAnomaly -= SpawnAnomaly;
        anomalyChannel.OnSendAnomalyGuess -= AnomalyGuess;
        anomalyChannel.OnGetTotalAnomaliesFixed -= GetTotalAnomaliesFixed;
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            CheckForDefeat();
        }
        
    }

    private void CheckForDefeat()
    {
        if(activeAnomalies.Count > numberForWarning && !isWarning)
        {
            InitWarning();
        }

        if(activeAnomalies.Count > numberForGameOver)
        {
            InitGameOver();
        }
    }

    private void InitWarning()
    {
        isWarning = true;
        soundChannel.PlayWarningSFXAction();
        uiChannel.DisplayWarningMessageAction();
    }

    private void InitGameOver()
    {
        isGameOver = true;
        gameStateChannel.PlayJumpscareAction();
        gameStateChannel.TriggerGameTimeAction(false);
        gameStateChannel.TriggerPlayerInteractionAction(false);
        anomalyChannel.StopSpawnTimerAction();
    }

    private void SpawnAnomaly()
    {
        GameObject anomalyToSpawn = SearchForInactiveAnomaly();
        if(anomalyToSpawn == null)
        {
            SpawnAnomaly();
            return;
        }
        else
        {
            anomalyToSpawn.GetComponent<IAnomaly>().ActivateAnomaly();
            activeAnomalies.Add(anomalyToSpawn);
            anomalies.Remove(anomalyToSpawn);
            totalAnomalies++;
        }        
    }

    private GameObject SearchForInactiveAnomaly()
    {
        int randomAnomalyIndex = UnityEngine.Random.Range(0, anomalies.Count);

        if (!anomalies[randomAnomalyIndex].GetComponent<IAnomaly>().IsAnomalyActive())
        {
            if (!anomalies[randomAnomalyIndex].GetComponent<IAnomaly>().ShyCheck())
            {
                if (!anomalies[randomAnomalyIndex].GetComponent<IAnomaly>().CooldownCheck())
                {
                    Debug.Log($"Spawning Anomaly {anomalies[randomAnomalyIndex].transform.name}");
                    return anomalies[randomAnomalyIndex];
                }
                Debug.Log("Anomaly is on Cooldown!");
                return null;
            }
            Debug.Log("Anomaly is shy and won't come out!");
            return null;
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

    private void GetTotalAnomaliesFixed()
    {
        anomalyChannel.SendTotalAnomaliesFixedAction(detectedAnomalies);
    }
}
