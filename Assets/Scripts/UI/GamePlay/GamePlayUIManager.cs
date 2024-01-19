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
    [Header("���")]
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider sfxVolume;
    public Button settingButton;
    public Button resumeButton;
    public Button menuButton;
    private void OnEnable()
    {
        // ����Slider��ValueChanged�¼�
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
        // �����ﴦ����ֵ�仯����߼�
        AudioManager.Instance.ChangeMainVolume(value);

        // ���߸���value����������������������������
        // AudioListener.volume = value;  ������Ϊʾ����ʵ��ʹ����ȷ��AudioListener���ڣ�
    }
    private void OnMusicValueChanged(float value)
    {
        // �����ﴦ����ֵ�仯����߼�
        AudioManager.Instance.ChangeMusicVolume(value);

        // ���߸���value����������������������������
        // AudioListener.volume = value;  ������Ϊʾ����ʵ��ʹ����ȷ��AudioListener���ڣ�
    }
    private void OnSFXValueChanged(float value)
    {
        // �����ﴦ����ֵ�仯����߼�
        AudioManager.Instance.ChangeSFXVolume(value);

        // ���߸���value����������������������������
        // AudioListener.volume = value;  ������Ϊʾ����ʵ��ʹ����ȷ��AudioListener���ڣ�
    }
    public void ChangeSlider(float mainAmount,float musicAmount,float sfxAmount)
    {
        Debug.Log("enter");
        masterVolume.value = (mainAmount+80)/100;
        musicVolume.value = (musicAmount+80)/100;
        sfxVolume.value = (sfxAmount + 80) / 100;
    }
}
