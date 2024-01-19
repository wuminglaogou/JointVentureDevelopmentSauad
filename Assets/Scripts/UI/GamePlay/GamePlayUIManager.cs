using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GamePlayUIManager : Singleton<GamePlayUIManager>
{
    public GameObject pauseUI;
    public GameObject volume;
    [Header("组件")]
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider sfxVolume;
    public Button settingButton;
    public Button resumeButton;
    public Button menuButton;
    private void OnEnable()
    {
        // 订阅Slider的ValueChanged事件
        masterVolume.onValueChanged.AddListener(OnMasterValueChanged);
        musicVolume.onValueChanged.AddListener(OnMusicValueChanged);
        sfxVolume.onValueChanged.AddListener(OnSFXValueChanged);
        settingButton.onClick.AddListener(TogglePauseUI);
        resumeButton.onClick.AddListener(TogglePauseUI);
        menuButton.onClick.AddListener(BackToMenu);
    }

    private void OnDisable()
    {
        masterVolume.onValueChanged.RemoveAllListeners();
        musicVolume.onValueChanged.RemoveAllListeners();
        sfxVolume.onValueChanged.RemoveAllListeners();
        settingButton.onClick.RemoveAllListeners();
        menuButton.onClick.RemoveAllListeners();
    }
    private void TogglePauseUI()
    {
        if(pauseUI.activeSelf)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseUI.SetActive(true);
            AudioManager.Instance.ChangeSliderVolume();
            Time.timeScale = 0;
        }
    }
    private void BackToMenu()
    {
        SceneLoader.Instance.LoadMenuScene();
    }
    private void OnMasterValueChanged(float value)
    {
        // 在这里处理滑块值变化后的逻辑
        AudioManager.Instance.ChangeMainVolume(value);

        // 或者根据value进行其他操作，比如设置音量等
        // AudioListener.volume = value;  （仅作为示例，实际使用需确保AudioListener存在）
    }
    private void OnMusicValueChanged(float value)
    {
        // 在这里处理滑块值变化后的逻辑
        AudioManager.Instance.ChangeMusicVolume(value);

        // 或者根据value进行其他操作，比如设置音量等
        // AudioListener.volume = value;  （仅作为示例，实际使用需确保AudioListener存在）
    }
    private void OnSFXValueChanged(float value)
    {
        // 在这里处理滑块值变化后的逻辑
        AudioManager.Instance.ChangeSFXVolume(value);

        // 或者根据value进行其他操作，比如设置音量等
        // AudioListener.volume = value;  （仅作为示例，实际使用需确保AudioListener存在）
    }
    public void ChangeSlider(float mainAmount,float musicAmount,float sfxAmount)
    {
        Debug.Log("enter");
        masterVolume.value = (mainAmount+80)/100;
        musicVolume.value = (musicAmount+80)/100;
        sfxVolume.value = (sfxAmount + 80) / 100;
    }
}
