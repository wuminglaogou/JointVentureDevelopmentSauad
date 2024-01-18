using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("ÒýÓÃ")]
    public GameSceneSO gameScene;
    public Button startGameButton;
    public Button quitGameButton;
    public Button instructionButton;
    private void OnEnable()
    {
        startGameButton.onClick.AddListener(StartGame);
        quitGameButton.onClick.AddListener(QuitGame);
        instructionButton.onClick.AddListener(Instruction);
    }

    private void Instruction()
    {
        Debug.Log("Instuction");
    }

    private void QuitGame()
    {
        Debug.Log("QuitGame");
    }

    private void StartGame()
    {
        SceneLoader.Instance.LoadNewScene(gameScene);
    }
}
