using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : PersistentSingleton<AudioManager>
{
    public const float Min_Pitch = 0.9f;
    public const float Max_Pitch = 1.1f;
    public AudioSource movingPlayer;
    public AudioSource sfxPlayer;
    public AudioSource musicPlayer;
    public AudioMixer mixer;
    //放音频
    public void PlayMovingAudio()
    {
        movingPlayer.Play();
    }
    public void StopMovingAudio()
    {
        movingPlayer.Stop();
    }
    public void ChangeMainVolume(float amount)
    {
        mixer.SetFloat("MasterVolume", amount * 100 - 80);
    }
    public void ChangeMusicVolume(float amount)
    {
        mixer.SetFloat("BGMVolume", amount * 100 - 80);
    }
    public void ChangeSFXVolume(float amount)
    {
        mixer.SetFloat("SFXVolume", amount * 100 - 80);
    }
    public void PlayAudio(AudioData audioData)
    {
       sfxPlayer.PlayOneShot(audioData.clip,audioData.volume);
    }
    public void ChangeSliderVolume()
    {
        float masterAmount;
        float musicAmount;
        float sfxAmount;
        mixer.GetFloat("MasterVolume",out masterAmount);
        mixer.GetFloat("BGMVolume", out musicAmount);
        mixer.GetFloat("SFXVolume", out sfxAmount);
        GamePlayUIManager.Instance.ChangeSlider(masterAmount, musicAmount, sfxAmount);
    }
    //随机音高放音频
    public void RandomlyPlayAudio(AudioData audioData)
    {
        sfxPlayer.pitch=Random.Range(Min_Pitch, Max_Pitch);
        sfxPlayer.PlayOneShot(audioData.clip, audioData.volume);
    }
    //在众多音效中随机播放
    public void PlayRandomSFX(AudioData[] audioData)
    {
        RandomlyPlayAudio(audioData[Random.Range(0, audioData.Length)]);
    }
}

