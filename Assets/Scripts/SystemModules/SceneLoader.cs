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
        while (color.a < 1f) // ����ɫAlphaͨ��ֵС��1����ȫ��͸����ʱ��ѭ��ִ��
        {
            // ÿ֡����Alphaͨ��ֵ�ļ��㹫ʽ
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / durationTime);
            //�����ǣ�ÿ0.02s��������ô�࣬��ʵ������deltatime�ĵ�λ�ٶȣ�Ҳ������·��:1/fadeduration/һ�����ж���֡Ҳ����*Time.DeltaTime
            // ����ͼ�����ɫ
            transitionImage.color = color;

            // ��Э����ͣһ֡��������һ֡����ִ��
            yield return null;
        }
        //��singleģʽ������ж�س�����
        //AsyncOperationHandle unloadHandle = currentScene.sceneReference.UnLoadScene();
        //yield return unloadHandle;
        sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Single, true);
        while (color.a > 0f) // ����ɫAlphaͨ��ֵС��1����ȫ��͸����ʱ��ѭ��ִ��
        {
            // ÿ֡����Alphaͨ��ֵ�ļ��㹫ʽ
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / durationTime);
            //�����ǣ�ÿ0.02s��������ô�࣬��ʵ������deltatime�ĵ�λ�ٶȣ�Ҳ������·��:1/fadeduration/һ�����ж���֡Ҳ����*Time.DeltaTime
            // ����ͼ�����ɫ
            transitionImage.color = color;
            // ��Э����ͣһ֡��������һ֡����ִ��
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
