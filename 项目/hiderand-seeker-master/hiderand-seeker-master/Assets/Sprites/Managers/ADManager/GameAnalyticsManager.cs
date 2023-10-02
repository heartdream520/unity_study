using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsManager : MonoSingleton<GameAnalyticsManager>
{
    [HideInInspector]
    public string placement;

    public string inSertAdSources { get; internal set; }
    public string rewardAdSources { get; internal set; }

    private void Start()
    {
        GameAnalytics.SetCustomId("myCustomUserId");

        //MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += HandleRewardedAdClosed;


        
    }
    public void SendGameBegin()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,
              GameDataManager.Instance.levelManager.GetAdLevel());
    }
    public void SendGameNext()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,
            GameDataManager.Instance.levelManager.GetAdLevel());
    }
    public void SendShowRewardedVideo()
    {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo,
            "admob", placement);
      //  Debug.Log("GameAnalyticsManager->SendShowRewardedVideo");

    }
    public void SendFailShowRewardedVideo()
    {
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.RewardedVideo,
            "admob", placement);
      //  Debug.Log("GameAnalyticsManager->SendFailShowRewardedVideo");

    }
    public void SendShowInterstitialVideo()
    {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial,
            "admob", placement);
      //  Debug.Log("GameAnalyticsManager->SendShowInterstitialVideo");

    }
    public void SendFailShowInterstitialVideo()
    {
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial,
            "admob", placement);
       // Debug.Log("GameAnalyticsManager->SendFailShowInterstitialVideo");
    }
    private void HandleRewardedAdClosed(string arg1, MaxSdkBase.AdInfo info)
    {
        if (placement != null)
        {
            // send ad event without tracking elapsedTime
            GameAnalytics.NewAdEvent(GAAdAction.Show,
                GAAdType.RewardedVideo, "admob", placement);

            placement = null;
        }
    }


}