using GameAnalyticsSDK.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingDataManager : MonoSingleton<TalkingDataManager>
{
    public string placement;

    public string inSertAdSources { get; internal set; }
    public string rewardAdSources { get; internal set; }

    void Start()
    {
        //TalkingDataSDK.SetVerboseLogDisable();
        //TalkingDataSDK.BackgroundSessionEnabled();
       // TalkingDataSDK.BackgroundSessionEnabled();
        //TalkingDataSDK.Init("88B8077E94B44C359B2358DD000524BA", "GooglePlay", "custom");

        // 在应用启动后调用 TalkingDataPlugin.SessionStarted
        TalkingDataPlugin.SessionStarted("88B8077E94B44C359B2358DD000524BA", "GooglePlay");
        //TalkingDataPlugin.SetExceptionReportEnabled(true);
    }

    /// <summary>
    /// 第一次游戏初始化完成（单人单次）
    /// </summary>
    public void SendPlayFirstLoadingEnd()
    {
        int x = PlayerPrefs.GetInt("TalkingPlayFirstLoadingEnd", -1);
        if (x == -1)
        {
       //     Debug.Log("TdLog->SendPlayFirstLoadingEnd");
            PlayerPrefs.SetInt("TalkingPlayFirstLoadingEnd", 1);



            Dictionary<string, object> dic = new Dictionary<string, object>();
            TalkingDataPlugin.TrackEventWithParameters("Game_enter", "game progress", dic);
            //TalkingDataSDK.OnEvent("Game_enter", dic);
        //    Debug.Log("TdLog->SendPlayFirstLoadingEnd");

        }
        else
        {
     //       Debug.Log("TdLog->NotSendPlayFirstLoadingEnd");
        }
    }
    /// <summary>
    /// 关卡开始去重 （单人单关单次）
    /// </summary>
    public void SendPlayerLevelUnSame()
    {
        int level = GetLevel();
        int x = PlayerPrefs.GetInt("TalkingPlayerLevelUnSame" + level, -1);
        if (x == -1)
        {
    //        Debug.Log($"TdLog->SendPlayerLevelUnSame  Level:{level} Role:{GameingMainManager.Instance.player_Identity_Enum.ToString()};");

            PlayerPrefs.SetInt("TalkingPlayerLevelUnSame" + level, 1);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Level", level);
            dic.Add("Role", GameingMainManager.Instance.player_Identity_Enum.ToString());
            TalkingDataPlugin.TrackEventWithParameters("Level_start_once", "game progress", dic);

          //  Debug.Log($"TdLog->SendPlayerLevelUnSame  Level:{level} Role:{GameingMainManager.Instance.player_Identity_Enum.ToString()};");

            // TalkingDataSDK.OnEvent("Level_start_once", dic);

        }
        else
        {
    //        Debug.Log($"TdLog->NotSendPlayerLevelUnSame  Level:{level} Role:{GameingMainManager.Instance.player_Identity_Enum.ToString()};");

        }
    }
    public void SendPlayerLevelCanSame()
    {

        int level = GetLevel();
 //       Debug.Log($"TdLog->SendPlayerLevelCanSame  Level:{level} Role:{GameingMainManager.Instance.player_Identity_Enum.ToString()};");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("Role", GameingMainManager.Instance.player_Identity_Enum.ToString());
        TalkingDataPlugin.TrackEventWithParameters("Level_start", "game progress", dic);

   //     Debug.Log($"TdLog->SendPlayerLevelCanSame  Level:{level} Role:{GameingMainManager.Instance.player_Identity_Enum.ToString()};");

        // TalkingDataSDK.OnEvent("Level_start", dic);


    }
    public void SendLevelFail()
    {

        int level = GetLevel();
    //    Debug.Log($"TdLog->SendLevelFail  Level:{level} Role:{GameingMainManager.Instance.player_Identity_Enum.ToString()};");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("Role", GameingMainManager.Instance.player_Identity_Enum.ToString());
       TalkingDataPlugin.TrackEventWithParameters("Level_Fail", "game progress", dic);

    //    Debug.Log($"TdLog->SendLevelFail  Level:{level} Role:{GameingMainManager.Instance.player_Identity_Enum.ToString()};");

        // TalkingDataSDK.OnEvent("Level_Fail", dic);


    }
    public void SendLevelWin()
    {
        int level = GetLevel();
    //    Debug.Log($"TdLog->SendLevelWin  Level:{level} Role:{GameingMainManager.Instance.player_Identity_Enum.ToString()};");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("Role", GameingMainManager.Instance.player_Identity_Enum.ToString());
        TalkingDataPlugin.TrackEventWithParameters("Level_complete", "game progress", dic);
       // TalkingDataSDK.OnEvent("Level_complete", dic);

   //     Debug.Log($"TdLog->SendLevelWin  Level:{level} Role:{GameingMainManager.Instance.player_Identity_Enum.ToString()};");

    }
    public void Send_IN_TryBegin(string ErrorCode)
    {
        int level = GetLevel();
    //    Debug.Log($"TdLog->Send_IN_TryBegin  Level:{level} ErrorCode:{ErrorCode} placement:{"IN_TryBegin_" + placement}");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("ErrorCode", ErrorCode);
        TalkingDataPlugin.TrackEventWithParameters("IN_TryBegin_" + placement, "InterstitialAd", dic);
   //     Debug.Log($"TdLog->Send_IN_TryBegin  Level:{level} ErrorCode:{ErrorCode} placement:{"IN_TryBegin_" + placement}");

        // TalkingDataSDK.OnEvent("IN_TryBegin_" + placement, dic);


    }
    public void Send_IN_Err(string ErrorCode)
    {
        int level = GetLevel();
   //     Debug.Log($"TdLog->Send_IN_Err  Level:{level} ErrorCode:{ErrorCode} placement:{"IN_Err_" + placement}");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("ErrorCode", ErrorCode);
       TalkingDataPlugin.TrackEventWithParameters("IN_Err_" + placement, "InterstitialAd", dic);
        // TalkingDataSDK.OnEvent("IN_Err_" + placement, dic);

   //     Debug.Log($"TdLog->Send_IN_Err  Level:{level} ErrorCode:{ErrorCode} placement:{"IN_Err_" + placement}");



    }
    public void Send_IN_Begin_(string ErrorCode)
    {
        int level = GetLevel();
   //     Debug.Log($"TdLog->Send_IN_Begin_  Level:{level} ErrorCode:{ErrorCode} placement:{"IN_Begin_" + placement}");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("ErrorCode", ErrorCode);
        TalkingDataPlugin.TrackEventWithParameters("IN_Begin_" + placement, "InterstitialAd", dic);
        //  TalkingDataSDK.OnEvent("IN_Begin_" + placement, dic);
    //    Debug.Log($"TdLog->Send_IN_Begin_  Level:{level} ErrorCode:{ErrorCode} placement:{"IN_Begin_" + placement}");



    }
    public void Send_IN_End_(string ErrorCode)
    {
        int level = GetLevel();
   //     Debug.Log($"TdLog->Send_IN_End_  Level:{level} ErrorCode:{ErrorCode} placement:{"IN_End_" + placement}");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("ErrorCode", ErrorCode);
        TalkingDataPlugin.TrackEventWithParameters("IN_End_" + placement, "InterstitialAd", dic);
        // TalkingDataSDK.OnEvent("IN_End_" + placement, dic);
    //    Debug.Log($"TdLog->Send_IN_End_  Level:{level} ErrorCode:{ErrorCode} placement:{"IN_End_" + placement}");



    }
    public void Send_RV_TryBegin_(string ErrorCode)
    {
        int level = GetLevel();
    //    Debug.Log($"TdLog->Send_RV_TryBegin_  Level:{level} ErrorCode:{ErrorCode} placement:{"RV_TryBegin_" + placement}");

        Dictionary<string, object> dic = new Dictionary<string, object>();

  //      Debug.Log($"TdLog->Send_RV_TryBegin_  01");

       dic.Add("Level", level);
 //       Debug.Log($"TdLog->Send_RV_TryBegin_  02");
        dic.Add("ErrorCode", ErrorCode);
      //  Debug.Log($"TdLog->Send_RV_TryBegin_  03");
        TalkingDataPlugin.TrackEventWithParameters("RV_TryBegin_" + placement, " RewardedAd", dic);
      //  Debug.Log($"TdLog->Send_RV_TryBegin_  04");
        // TalkingDataSDK.OnEvent("RV_TryBegin_" + placement, dic);
      //  Debug.Log($"TdLog->Send_RV_TryBegin_  Level:{level} ErrorCode:{ErrorCode} placement:{"RV_TryBegin_" + placement}");



    }
    public void Send_RV_Err_(string ErrorCode)
    {
        int level = GetLevel();
    //    Debug.Log($"TdLog->Send_RV_Err_  Level:{level} ErrorCode:{ErrorCode} placement:{"RV_Err_" + placement}");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("ErrorCode", ErrorCode);
        TalkingDataPlugin.TrackEventWithParameters("RV_Err_" + placement, " RewardedAd", dic);
        // TalkingDataSDK.OnEvent("RV_Err_" + placement, dic);

     //   Debug.Log($"TdLog->Send_RV_Err_  Level:{level} ErrorCode:{ErrorCode} placement:{"RV_Err_" + placement}");



    }
    public void Send_RV_Begin_(string ErrorCode)
    {
        int level = GetLevel();
   //     Debug.Log($"TdLog->Send_RV_Begin_  Level:{level} ErrorCode:{ErrorCode} placement:{"RV_Begin_" + placement}");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("ErrorCode", ErrorCode);
         TalkingDataPlugin.TrackEventWithParameters("RV_Begin_" + placement, " RewardedAd", dic);
        //TalkingDataSDK.OnEvent("RV_Begin_" + placement, dic);

    //    Debug.Log($"TdLog->Send_RV_Begin_  Level:{level} ErrorCode:{ErrorCode} placement:{"RV_Begin_" + placement}");
    }
    public void Send_RV_End_(string ErrorCode)
    {
        int level = GetLevel();
    //    Debug.Log($"TdLog->Send_RV_End_  Level:{level} ErrorCode:{ErrorCode} placement:{"RV_End_" + placement}");

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("Level", level);
        dic.Add("ErrorCode", ErrorCode);
         TalkingDataPlugin.TrackEventWithParameters("RV_End_" + placement, " RewardedAd", dic);
        // TalkingDataSDK.OnEvent("RV_End_" + placement, dic);
//        Debug.Log($"TdLog->Send_RV_End_  Level:{level} ErrorCode:{ErrorCode} placement:{"RV_End_" + placement}");


    }
    private int GetLevel()
    {
        return GameDataManager.Instance.levelManager.GetNowLevelInt();
    }
}