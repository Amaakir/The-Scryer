using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIChannel", menuName = "Event Channels/UI Channel")]
public class UIChannelSO : ScriptableObject
{
    public delegate void PlayUIDirectorCallback();
    public PlayUIDirectorCallback OnPlayUIDirector;

    public delegate void ResetUITimelineCallback();
    public ResetUITimelineCallback OnResetUITimeline;

    public delegate void ResetIntroTimelineCallback();
    public ResetIntroTimelineCallback OnResetIntroTimeline;

    public delegate void PlayIntroDirectorCallback();
    public PlayIntroDirectorCallback OnPlayIntroDirector;

    public delegate void StartGameIntroCallback();
    public StartGameIntroCallback OnStartGameIntro;

    public delegate void DisplayErrorMessageCallback(string message);
    public DisplayErrorMessageCallback OnDisplayErrorMessage;

    public delegate void PlayFixAnomalyScreenCallback();
    public PlayFixAnomalyScreenCallback OnPlayFixAnomalyScreen;

    public void StartGameIntroAction()
    {
        OnStartGameIntro?.Invoke();
    }

    public void PlayUIDirectorAction()
    {
        OnPlayUIDirector?.Invoke();
    }

    public void PlayIntroDirector()
    {
        OnPlayIntroDirector?.Invoke();
    }

    public void ResetUITimelineAction()
    {
        OnResetUITimeline?.Invoke();
    }

    public void ResetIntroTimelineAction()
    {
        OnResetIntroTimeline?.Invoke();
    }

    public void DisplayErrorMessageAction(string message)
    {
        OnDisplayErrorMessage?.Invoke(message);
    }

    public void PlayFixAnomalyScreenAction()
    {
        OnPlayFixAnomalyScreen?.Invoke();
    }
}
