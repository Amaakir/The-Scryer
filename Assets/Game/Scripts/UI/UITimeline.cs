using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UITimeline : MonoBehaviour
{
    [SerializeField] UIChannelSO uiChannel;
    PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        uiChannel.OnPlayUIDirector += PlayUITimeline;
        uiChannel.OnResetUITimeline += ResetDirector;
    }

    private void OnDisable()
    {
        uiChannel.OnPlayUIDirector -= PlayUITimeline;
        uiChannel.OnResetUITimeline -= ResetDirector;
    }

    private void PlayUITimeline()
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
