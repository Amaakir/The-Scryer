using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] UIChannelSO uiChannel;

    private void OnEnable()
    {
        uiChannel.OnTriggerLoadingScreen += TriggerLoadingScreen;
    }

    private void OnDisable()
    {
        uiChannel.OnTriggerLoadingScreen -= TriggerLoadingScreen;
    }

    private void TriggerLoadingScreen(bool value)
    {
        loadingScreen.SetActive(value);
    }
}
