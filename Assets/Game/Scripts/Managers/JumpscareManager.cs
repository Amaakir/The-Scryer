using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class JumpscareManager : MonoBehaviour
{
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] UIChannelSO uiChannel;
    PlayableDirector director;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        gameStateChannel.OnPlayJumpscare += PlayJumpscareTimeline;
    }

    private void OnDisable()
    {
        gameStateChannel.OnPlayJumpscare -= PlayJumpscareTimeline;
    }

    public void FinishJumpscare()
    {
        uiChannel.StartGameOverScreenAction();
        ResetDirector();
    }

    private void PlayJumpscareTimeline()
    {
        ResetDirector();
        director.Play();
    }

    private void ResetDirector()
    {
        director.time = 0;
        director.Stop();
        director.Evaluate();
    }
}
