using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipData", menuName = "Data Holder/AudioClip")]
public class AudioClipDataSO : ScriptableObject
{
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public bool trackMaster;
    [SerializeField] public bool trackMusic;
    [SerializeField] public bool trackSFX;
    [SerializeField] public bool trackUI;
    [SerializeField] public float audioVolume;
    [SerializeField] public bool isLoop;
    [SerializeField] public Transform audioOrigin;
}
