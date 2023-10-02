using System.Collections;
using UnityEngine;
using MyEnum;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class MySceneManager : MonoSingleton<MySceneManager>
{
    public MyEnum.SceneNameEnum startSceneNameEnum;
    private MyEnum.SceneNameEnum nowSceneNameEnum;
    public override void OnAwake()
    {
        EventManager.Instance.OneGameEndAction += this.OnOneGameEnd;

    }
    private void OnDestroy()
    {
        EventManager.Instance.OneGameEndAction -= this.OnOneGameEnd;
    }
    private void OnOneGameEnd(bool iswin)
    {
        //LoadLevelSceneAsyncOperation(GameDataManager.
        //    Instance.levelManager.GetNowLevel());
        //LoadLevelSceneAsyncOperation(GameDataManager.
         //   Instance.levelManager.GetNextLevel());
    }

    public void Init()
    {
        nowSceneNameEnum = MyEnum.SceneNameEnum.MainScene;
        sceneChacedir = new Dictionary<SceneNameEnum, AsyncOperation>();

        this.ChangeSceneToStartScene();
    }

    public Dictionary<SceneNameEnum, AsyncOperation> sceneChacedir;
    public void LoadLevelSceneAsyncOperation(SceneNameEnum sceneNameEnum)
    {
        if (sceneChacedir.ContainsKey(sceneNameEnum))
            return;
        var x = SceneManager.LoadSceneAsync(sceneNameEnum.ToString(), LoadSceneMode.Additive);
        x.allowSceneActivation = false;
        sceneChacedir.Add(sceneNameEnum, x);
    }
    private void ChangeSceneToStartScene()
    {
        if (this.startSceneNameEnum == MyEnum.SceneNameEnum.None)
        {
            Debug.LogError("MySceneManage->ChangeSceneToStartScene:开始场景为 None");
            return;
        }
        bool hasLoadPersistentScene = false;
        bool hasLoadStartScene = false;
        for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
        {
            string name = SceneManager.GetSceneAt(i).name;
            if (name == MyEnum.SceneNameEnum.Persistence.ToString())
            {
                hasLoadPersistentScene = true;
            }
            else if (name == this.startSceneNameEnum.ToString())
            {
                hasLoadStartScene = true;
            }
            else
            {
                SceneManager.UnloadSceneAsync(name);
            }
        }
        if (!hasLoadPersistentScene)
        {
            SceneManager.LoadSceneAsync(MyEnum.SceneNameEnum.Persistence.ToString(), LoadSceneMode.Additive);
        }
        if (!hasLoadStartScene)
        {
            SceneManager.LoadSceneAsync(this.startSceneNameEnum.ToString(), LoadSceneMode.Additive);
        }
        this.nowSceneNameEnum = this.startSceneNameEnum;
        nowScene = SceneManager.GetSceneByName(startSceneNameEnum.ToString());


        //nowScene.name = GetNextSceneNameEnumSceneName(startSceneNameEnum);
    }
    public void ChangeScene(MyEnum.SceneNameEnum toScene, Action action = null)
    {
        StartCoroutine(ChangeSceneCoroutine(toScene, action));
    }
    private Scene nowScene;

    private IEnumerator ChangeSceneCoroutine(SceneNameEnum toScene, Action action = null)
    {
        if (toScene == this.nowSceneNameEnum)
        {
            Debug.LogWarning("MySceneManager->ChangeSceneCoroutine:要进入的场景和当前场景相同！！");
            //yield break;
        }
        AsyncOperation loadSceneAsyncOperation;
        if (sceneChacedir.TryGetValue(toScene, out loadSceneAsyncOperation))
        {
            sceneChacedir.Remove(toScene);
        }
        else
        {
          //  Debug.LogError("没有预加载："+toScene);

            loadSceneAsyncOperation = SceneManager.LoadSceneAsync
                (toScene.ToString(), LoadSceneMode.Additive);
        }
        loadSceneAsyncOperation.allowSceneActivation = true;
        bool hasSetNowScene = false;

        for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
        {
            if (!hasSetNowScene)
            {
                if (SceneManager.GetSceneAt(i).name == toScene.ToString())
                {
                    nowScene = SceneManager.GetSceneAt(i);
                    hasSetNowScene = true;
                    continue;
                }
            }
            if (SceneManager.GetSceneAt(i).name.StartsWith("Level"))
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }
        
        while(!nowScene.isLoaded)
        {
            yield return null;
        }
        yield return SceneManager.SetActiveScene(nowScene);
        action?.Invoke();
        //yield return new WaitForSeconds(0.2f);

        //yield return FadeManager.Instance.FadeTo0();
        this.nowSceneNameEnum = toScene;
        
        yield return null;
    }

  
}