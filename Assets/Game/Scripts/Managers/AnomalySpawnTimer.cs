using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalySpawnTimer : MonoBehaviour
{
    [Header("Anomaly Timer Settings")]
    [SerializeField] bool canAnomaliesSpawn = false;
    [SerializeField] bool pauseSpawnTimer = false;
    [SerializeField] float anomalySpawnTimer = 0;
    [SerializeField] float minAnomalyTimer;
    [SerializeField] float maxAnomalyTimer;
    [SerializeField] float anomalySpawnCooldown;
    Coroutine SpawnCooldownCoroutine;
    GameTime gameTime;

    [Header("Event Channels")]
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] GameStateChannelSO gameStateChannel;

    private void Start()
    {
        gameTime = GetComponent<GameTime>();
        InitAnomalySpawnTimer();
    }

    private void InitAnomalySpawnTimer()
    {
        canAnomaliesSpawn = false;
        pauseSpawnTimer = false;
        anomalySpawnTimer = 0;
    }

    private void OnEnable()
    {
        anomalyChannel.OnStartSpawnTimer += StartAnomalyTimer;
        anomalyChannel.OnStopSpawnTimer += StopAnomalyTimer;
        gameStateChannel.OnResetCoreData += InitAnomalySpawnTimer;
    }

    private void OnDisable()
    {
        anomalyChannel.OnStartSpawnTimer -= StartAnomalyTimer;
        anomalyChannel.OnStopSpawnTimer -= StopAnomalyTimer;
        gameStateChannel.OnResetCoreData -= InitAnomalySpawnTimer;
    }

    private void Update()
    {
        if (canAnomaliesSpawn && !pauseSpawnTimer)
        {
            AnomalyTimer();
        }
    }

    private void StartAnomalyTimer()
    {
        TriggerAnomalyTimer(true);
        ResetAnomalyTimer();
    }

    private void StopAnomalyTimer()
    {
        TriggerAnomalyTimer(false);
    }

    private void TriggerAnomalyTimer(bool value)
    {
        canAnomaliesSpawn = value;
    }

    private void PauseAnomalyTimer(bool value)
    {
        pauseSpawnTimer = value;
    }

    private void ResetAnomalyTimer()
    {
        anomalySpawnTimer = GetRandomAnomalyTimer();
        PauseAnomalyTimer(false);
    }

    private float GetRandomAnomalyTimer()
    {
        return UnityEngine.Random.Range(minAnomalyTimer, maxAnomalyTimer);
    }

    private void AnomalyTimer()
    {
        anomalySpawnTimer -= Time.deltaTime * gameTime.timeScale;
        CheckForAnomalySpawn();
    }

    private void CheckForAnomalySpawn()
    {
        if (anomalySpawnTimer < 0)
        {
            PauseAnomalyTimer(true);
            anomalyChannel.SpawnAnomalyAction();
            SpawnCooldownCoroutine = StartCoroutine(SpawnerCooldown());
        }
    }

    IEnumerator SpawnerCooldown()
    {
        yield return new WaitForSeconds(anomalySpawnCooldown);
        ResetAnomalyTimer();
        ResetCooldownCoroutine();
    }

    private void ResetCooldownCoroutine()
    {
        StopCoroutine(SpawnCooldownCoroutine);
        SpawnCooldownCoroutine = null;
    }
}
