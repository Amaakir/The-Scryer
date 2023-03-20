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
    [SerializeField] AudioClipDataSO musicGameOver;

    [Header("SFX Tracks")]
    [SerializeField] AudioClipDataSO staticShort;
    [SerializeField] AudioClipDataSO staticLong;
    [SerializeField] AudioClipDataSO timeTick;
    [SerializeField] AudioClipDataSO jumpscareSFX;

    [Header("Event Channels")]
    [SerializeField] SoundChannelSO soundChannel;
    [SerializeField] GameStateChannelSO gameStateChannel;
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
        gameStateChannel.OnPlayJumpscare += PlayJumpscareSFX;
        soundChannel.OnPlayGameOverMusic += PlayGameOverMusic;
    }

    private void OnDisable()
    {
        soundChannel.OnPlayAudio -= InitAudioOptions;
        soundChannel.OnPlayMainMenuMusic -= PlayMainMenuMusic;
        soundChannel.OnStopAllAudioAction -= StopAllAudio;
        soundChannel.OnPlayMansionSceneMusicAction -= PlayMansionSceneMusic;
        soundChannel.OnPlayWinScreenMusic -= PlayWinScreenMusic;
        soundChannel.OnPlayTimeTickSFX -= PlayTimeTickSFX;
        gameStateChannel.OnPlayJumpscare -= PlayJumpscareSFX;
        soundChannel.OnPlayGameOverMusic -= PlayGameOverMusic;
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

    private void PlayJumpscareSFX()
    {
        InitAudioOptions(jumpscareSFX);
    }

    private void PlayWinScreenMusic()
    {
        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.StopTrack, MMSoundManager.MMSoundManagerTracks.Music);
        InitAudioOptions(musicWinScreen);
    }

    private void PlayGameOverMusic()
    {
        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.StopTrack, MMSoundManager.MMSoundManagerTracks.Music);
        InitAudioOptions(musicGameOver);
    }
}
