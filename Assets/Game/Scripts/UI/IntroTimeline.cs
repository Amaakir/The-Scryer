using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroTimeline : MonoBehaviour
{
    [SerializeField] UIChannelSO uiChannel;
    PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        uiChannel.OnPlayIntroDirector += PlayIntroTimeline;
        uiChannel.OnResetIntroTimeline += ResetDirector;
    }

    private void OnDisable()
    {
        uiChannel.OnPlayIntroDirector -= PlayIntroTimeline;
        uiChannel.OnResetIntroTimeline -= ResetDirector;
    }

    private void PlayIntroTimeline()
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
