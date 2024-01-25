using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    public Image transitionImage;
    public float durationTime = 1f;
    Color color;
    private Coroutine LoadSceneRoutine;
    override public void Awake()
    {
        base.Awake();
        LoadMenuScene();
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMenuSceneWithCoroutine()
    {
        LoadNewSceneWtihFadee("MainMenu");
    }
    public void LoadGameSceneWithCoroutine()
    {
        LoadNewSceneWtihFadee("GameScene1");
    }
    IEnumerator LoadSceneCoroutine(string number)
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
        SceneManager.LoadScene(number);
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
    }
    public void LoadNewSceneWtihFadee(string number)
    {
        StartCoroutine(LoadSceneCoroutine(number));       
    }
}



//    public void LoadNewScene(GameSceneSO gameSceneSO)
//    {
//        sceneToLoad = gameSceneSO;
//        if (currentScene != null)
//        {
//            if (LoadSceneRoutine != null)
//            {
//                StopCoroutine(LoadSceneRoutine);
//            }
//            LoadSceneRoutine = StartCoroutine(nameof(LoadSceneCoroutine));
//        }
//        else
//        {
//            sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Single);
//            currentScene = menuScene;
//        }
//    }
//}
