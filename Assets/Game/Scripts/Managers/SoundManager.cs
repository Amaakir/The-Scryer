using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains;
using MoreMountains.Tools;
using static UnityEditor.Progress;
using static MoreMountains.Tools.MMSoundManager;

public class SoundManager : MonoBehaviour
{
    [Header("Music Tracks per Scene")]
    [SerializeField] AudioClipDataSO musicMainMenu;
    [SerializeField] AudioClipDataSO musicMansionScene;
    [SerializeField] AudioClipDataSO musicWinScreen;

    [Header("SFX Tracks")]
    [SerializeField] AudioClipDataSO staticShort;
    [SerializeField] AudioClipDataSO staticLong;
    [SerializeField] AudioClipDataSO timeTick;

    [Header("Event Channels")]
    [SerializeField] SoundChannelSO soundChannel;
    private MMSoundManagerPlayOptions audioClipOptions;

    private void Awake()
    {
        audioClipOptions = MMSoundManagerPlayOptions.Default;
    }
    private void OnEnable()
    {
        soundChannel.OnPlayAudio += InitAudioOptions;
        soundChannel.OnPlayMainMenuMusic += PlayMainMenuMusic;
        soundChannel.OnStopAllAudioAction += StopAllAudio;
        soundChannel.OnPlayMansionSceneMusicAction += PlayMansionSceneMusic;
        soundChannel.OnPlayWinScreenMusic += PlayWinScreenMusic;
        soundChannel.OnPlayTimeTickSFX += PlayTimeTickSFX;
    }

    private void OnDisable()
    {
        soundChannel.OnPlayAudio -= InitAudioOptions;
        soundChannel.OnPlayMainMenuMusic -= PlayMainMenuMusic;
        soundChannel.OnStopAllAudioAction -= StopAllAudio;
        soundChannel.OnPlayMansionSceneMusicAction -= PlayMansionSceneMusic;
        soundChannel.OnPlayWinScreenMusic -= PlayWinScreenMusic;
        soundChannel.OnPlayTimeTickSFX -= PlayTimeTickSFX;
    }


    private void InitAudioOptions(AudioClipDataSO audioClipData)
    {
        //Cargamos todas las opciones que podamos poner
        audioClipOptions.Volume = audioClipData.audioVolume;
        audioClipOptions.Loop = audioClipData.isLoop;
        audioClipOptions.MmSoundManagerTrack =
            audioClipData.trackMaster ? MMSoundManagerTracks.Master :
            audioClipData.trackMusic ? MMSoundManagerTracks.Music :
            audioClipData.trackSFX ? MMSoundManagerTracks.Sfx :
            audioClipData.trackUI ? MMSoundManagerTracks.UI :
            MMSoundManagerTracks.Other;
        PlayAudio(audioClipData.audioClip);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        MMSoundManagerSoundPlayEvent.Trigger(audioClip, audioClipOptions);
    }

    private void StopAllAudio()
    {
        MMSoundManagerAllSoundsControlEvent.Trigger(MMSoundManagerAllSoundsControlEventTypes.Stop);
    }

    private void PlayMainMenuMusic()
    {
        InitAudioOptions(musicMainMenu);
    }

    private void PlayMansionSceneMusic()
    {
        InitAudioOptions(musicMansionScene);
    }

    private void PlayTimeTickSFX()
    {
        InitAudioOptions(timeTick);
    }

    private void PlayWinScreenMusic()
    {
        InitAudioOptions(musicWinScreen);
    }
}
