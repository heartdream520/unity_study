using GameAnalyticsSDK;
using MainScene;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBeginManager : MonoSingleton<GameBeginManager>
{
    public override void OnAwake()
    {
        base.OnAwake();

        UIMain.Instance.Loading();

        MySceneManager.Instance.Init();
        DebugHelper.Instance.Init();
        //JsonDataManager.Instance.Load();
        GameDataManager.Instance.Init();
        GameingMainManager.Instance.Init();
        UIManager.Instance.Init();
        //GameDataManager.Instance.Init();
        MaterialsManager.Instance.Init();
        PlayerObjectManager.Instance.Init();

        Application.quitting += this.OnGameQuit;

      //  var x= UIManager.Instance.Show<UICharacterShop>();
      //  x.OnChickClose();

        //GameingMainManager.Instance.BeginOneNewGame();
        MySceneManager.Instance.LoadLevelSceneAsyncOperation(
           GameDataManager.Instance.levelManager.GetNowLevel());

        

    }

    public override void OnStart()
    {
        GameAnalytics.Initialize();  //≥ı ºªØGameAnalytics
    }


    // Start is called before the first frame update

    private void OnGameQuit()
    {
        MaterialsManager.Instance.OnDestory();
        PlayerObjectManager.Instance.OnDestory();
    }
}
