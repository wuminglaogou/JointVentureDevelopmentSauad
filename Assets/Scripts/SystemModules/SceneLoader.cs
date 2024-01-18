using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    public GameSceneSO menuScene;
    public GameSceneSO currentScene;
    public GameSceneSO sceneToLoad;
    public Image transitionImage;
    public float durationTime = 1f;
    Color color;
    private Coroutine LoadSceneRoutine;
    override public void Awake()
    {
        base.Awake();
        LoadNewScene(menuScene);
    }
    
    public void LoadMenuScene()
    {
        LoadNewScene(menuScene);
        Time.timeScale = 1f;
    }
    IEnumerator LoadSceneCoroutine()
    {
        transitionImage.gameObject.SetActive(true);
        color.a = 0;
        while (color.a < 1f) // 当颜色Alpha通道值小于1（完全不透明）时，循环执行
        {
            // 每帧增加Alpha通道值的计算公式
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / durationTime);
            //本质是，每0.02s，增加那么多，其实就是求deltatime的单位速度，也就是总路程:1/fadeduration/一秒钟有多少帧也就是*Time.DeltaTime
            // 更新图像的颜色
            transitionImage.color = color;

            // 让协程暂停一帧，并在下一帧继续执行
            yield return null;
        }
        //用single模式，不用卸载场景了
        //AsyncOperationHandle unloadHandle = currentScene.sceneReference.UnLoadScene();
        //yield return unloadHandle;
        sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Single, true);
        while (color.a > 0f) // 当颜色Alpha通道值小于1（完全不透明）时，循环执行
        {
            // 每帧增加Alpha通道值的计算公式
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / durationTime);
            //本质是，每0.02s，增加那么多，其实就是求deltatime的单位速度，也就是总路程:1/fadeduration/一秒钟有多少帧也就是*Time.DeltaTime
            // 更新图像的颜色
            transitionImage.color = color;
            // 让协程暂停一帧，并在下一帧继续执行
            yield return null;
        }
        transitionImage.gameObject.SetActive(false);
        currentScene = sceneToLoad;
    }
    public void LoadNewScene(GameSceneSO gameSceneSO)
    {
        sceneToLoad = gameSceneSO;
        if (currentScene != null)
        {
            if (LoadSceneRoutine != null)
            {
                StopCoroutine(LoadSceneRoutine);
            }
            LoadSceneRoutine = StartCoroutine(nameof(LoadSceneCoroutine));
        }
        else
        {
            sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Single);
            currentScene = menuScene;
        }
    }
}
