using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundChannel", menuName = "Event Channels/Sound Channel")]
public class SoundChannelSO : ScriptableObject
{
    public delegate void PlayAudioCallback(AudioClipDataSO audioClipData);
    public PlayAudioCallback OnPlayAudio;

    public delegate void StopAllAudioCallback();
    public StopAllAudioCallback OnStopAllAudioAction;

    public delegate void PlayMainMenuMusicCallback();
    public PlayMainMenuMusicCallback OnPlayMainMenuMusic;

    public delegate void PlayMansionSceneMusicCallback();
    public PlayMansionSceneMusicCallback OnPlayMansionSceneMusicAction;


    public void PlayAudioAction(AudioClipDataSO audioClipData)
    {
        OnPlayAudio?.Invoke(audioClipData);
    }

    public void StopAllAudioAction()
    {
        OnStopAllAudioAction?.Invoke();
    }

    public void PlayMainMenuMusicAction()
    {
        OnPlayMainMenuMusic?.Invoke();
    }

    public void PlayMansionSceneMusic()
    {
        OnPlayMansionSceneMusicAction?.Invoke();
    }
}
