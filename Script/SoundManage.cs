using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManage : MonoBehaviour
{   
    [Header("Audio Source")]
    public AudioSource audioSound;
    public AudioSource audioEffect;

    [Header("Audio Clip")]
    public AudioClip audioBomb;
    public AudioClip audioDeath;
    public AudioClip audioWin;
    public AudioClip audioItem;

    [Header("Volume Slider")]
    public Slider musicSlider;
    public Slider effectSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicSlider.value = 0.1f;
        }

        if (PlayerPrefs.HasKey("effectVolume"))
        {
            effectSlider.value = PlayerPrefs.GetFloat("effectVolume");
        }
        else
        {
            effectSlider.value = 0.1f;
        }

        UpdateMusicVolume(musicSlider.value, audioSound, "musicVolume");
        UpdateMusicVolume(effectSlider.value, audioEffect, "effectVolume");
    }
    public void UpdateMusicVolume(float value, AudioSource audioSource, string typeMusic)
    {
        PlayerPrefs.SetFloat(typeMusic, value);
        audioSource.volume = value;
    }
    public void PlayAudioClip(AudioClip audioClip)
    {
        audioEffect.clip = audioClip;
        audioEffect.Play();
    }
    public void SoundVolumeChange()
    {
        UpdateMusicVolume(musicSlider.value, audioSound, "musicVolume");
    }
    public void EffectVolumeChange()
    {
        UpdateMusicVolume(effectSlider.value, audioEffect, "effectVolume");
    }
}
