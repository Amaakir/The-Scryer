using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnomalyChannel", menuName = "Event Channels/Anomaly Channel")]
public class AnomalyChannelSO : ScriptableObject
{
    public delegate void StartSpawnTimerCallback();
    public StartSpawnTimerCallback OnStartSpawnTimer;

    public delegate void SpawnAnomalyCallback();
    public SpawnAnomalyCallback OnSpawnAnomaly;

    public delegate void SendAnomalyGuessCallback(string anomalyGuess, string roomGuess);
    public SendAnomalyGuessCallback OnSendAnomalyGuess;

    public delegate void StopSpawnTimerCallback();
    public StopSpawnTimerCallback OnStopSpawnTimer;

    public delegate void GetTotalAnomaliesFixedCallback();
    public GetTotalAnomaliesFixedCallback OnGetTotalAnomaliesFixed;

    public delegate void SendTotalAnomaliesFixedCallback(int amount);
    public SendTotalAnomaliesFixedCallback OnSendTotalAnomaliesFixed;

    public void StartSpawnTimerAction()
    {
        OnStartSpawnTimer?.Invoke();
    }

    public void SpawnAnomalyAction()
    {
        OnSpawnAnomaly?.Invoke();
    }

    public void SendAnomalyGuessAction(string anomalyGuess, string roomGuess)
    {
        OnSendAnomalyGuess?.Invoke(anomalyGuess, roomGuess);
    }

    public void StopSpawnTimerAction()
    {
        OnStopSpawnTimer?.Invoke();
    }

    public void GetTotalAnomaliesFixedAction()
    {
        OnGetTotalAnomaliesFixed?.Invoke();
    }

    public void SendTotalAnomaliesFixedAction(int amount)
    {
        OnSendTotalAnomaliesFixed?.Invoke(amount);
    }

}
