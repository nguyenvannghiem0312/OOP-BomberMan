using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage : MonoBehaviour
{   
    public AudioClip audioBomb;
    public AudioClip audioDeath;
    public AudioClip audioWin;
    public AudioClip audioItem;
    public AudioSource audioSource;

    public void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
