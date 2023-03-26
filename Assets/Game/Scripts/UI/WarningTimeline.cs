using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WarningTimeline : MonoBehaviour
{
    [SerializeField] UIChannelSO uiChannel;
    PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        uiChannel.OnDisplayWarningMessage += PlayWarningTimeline;
        uiChannel.OnResetWarningTimeline += ResetDirector;
    }

    private void OnDisable()
    {
        uiChannel.OnDisplayWarningMessage -= PlayWarningTimeline;
        uiChannel.OnResetWarningTimeline -= ResetDirector;
    }

    private void PlayWarningTimeline()
    {
        ResetDirector();
        director.Play();
    }

    public void ResetDirector()
    {
        director.time = 0;
        director.Stop();
        director.Evaluate();
    }
}
