using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("����")]
    public AudioData clickSound;
    public GameSceneSO gameScene;
    public Button startGameButton;
    public Button quitGameButton;
    public Button instructionButton;
    public Button makerButton;
    public GameObject instructContent;
    public GameObject makerContent;
    Color color;
    private Coroutine imageCoroutine;
    private void OnEnable()
    {
        startGameButton.onClick.AddListener(StartGame);
        quitGameButton.onClick.AddListener(QuitGame);
        instructionButton.onClick.AddListener(Instruction);
        makerButton.onClick.AddListener(Maker);
    }

    private void Maker()
    {
        AudioManager.Instance.PlayAudio(clickSound);
        instructContent.SetActive(false); 
        makerContent.SetActive(true);
    }

    private void Instruction()
    {
        AudioManager.Instance.PlayAudio(clickSound);
        makerContent.SetActive(false );
        instructContent.SetActive(true);
    }

    private void QuitGame()
    {
        AudioManager.Instance.PlayAudio(clickSound);
        Application.Quit();
    }

    private void StartGame()
    {
        AudioManager.Instance.PlayAudio(clickSound);
        SceneLoader.Instance.LoadNewScene(gameScene);
    }
    IEnumerator ImageCoroutine(Image image)
    {    
        color=image.color;
        color.a = 1;
        while (color.a > 0f) // ����ɫAlphaͨ��ֵС��1����ȫ��͸����ʱ��ѭ��ִ��
        {

            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / 0.2f);

            // ����ͼ�����ɫ
            image.color = color;
            // ��Э����ͣһ֡��������һ֡����ִ��
            yield return null;
        }
        while (color.a < 1f) // ����ɫAlphaͨ��ֵС��1����ȫ��͸����ʱ��ѭ��ִ��
        {
            // ÿ֡����Alphaͨ��ֵ�ļ��㹫ʽ
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / 0.2f);
            //�����ǣ�ÿ0.02s��������ô�࣬��ʵ������deltatime�ĵ�λ�ٶȣ�Ҳ������·��:1/fadeduration/һ�����ж���֡Ҳ����*Time.DeltaTime
            // ����ͼ�����ɫ
            image.color = color;
            // ��Э����ͣһ֡��������һ֡����ִ��
            yield return null;
        }
      
    }
}

