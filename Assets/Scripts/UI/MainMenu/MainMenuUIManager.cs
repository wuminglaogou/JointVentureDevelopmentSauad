using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("引用")]
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
        while (color.a > 0f) // 当颜色Alpha通道值小于1（完全不透明）时，循环执行
        {

            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / 0.2f);

            // 更新图像的颜色
            image.color = color;
            // 让协程暂停一帧，并在下一帧继续执行
            yield return null;
        }
        while (color.a < 1f) // 当颜色Alpha通道值小于1（完全不透明）时，循环执行
        {
            // 每帧增加Alpha通道值的计算公式
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / 0.2f);
            //本质是，每0.02s，增加那么多，其实就是求deltatime的单位速度，也就是总路程:1/fadeduration/一秒钟有多少帧也就是*Time.DeltaTime
            // 更新图像的颜色
            image.color = color;
            // 让协程暂停一帧，并在下一帧继续执行
            yield return null;
        }
      
    }
}

