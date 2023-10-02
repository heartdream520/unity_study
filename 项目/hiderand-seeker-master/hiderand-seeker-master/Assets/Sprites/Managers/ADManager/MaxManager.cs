using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using static MaxSdkCallbacks;

public class MaxManager : MonoSingleton<MaxManager>
{
    enum AdPlayStatus
    {
        None,Success,Fail
    }

    private AdPlayStatus InAdPlayStatus;
    private AdPlayStatus ReAdPlayStatus;

    public Color bannerAdsBgColor { get; private set; }
    private bool adIsEnd;
    private string placement;
    public string Placement
    {
        get
        {
            return placement;
        }
        set
        {
            placement = value;
            GameAnalyticsManager.Instance.placement = placement;
            TalkingDataManager.Instance.placement = placement;
        }
    }
    private string inSertAdSources;
    public string InSertAdSources
    {
        get
        {
            return inSertAdSources;
        }
        set
        {
            inSertAdSources = value;
            GameAnalyticsManager.Instance.inSertAdSources = inSertAdSources;
            TalkingDataManager.Instance.inSertAdSources = inSertAdSources;
        }
    }
    private string rewardAdSources;
    public string RewardAdSources
    {
        get
        {
            return rewardAdSources;
        }
        set
        {
            rewardAdSources = value;
            GameAnalyticsManager.Instance.rewardAdSources = rewardAdSources;
            TalkingDataManager.Instance.rewardAdSources = rewardAdSources;
        }
    }
    private void Awake()
    {
       
    }

#if UNITY_IOS
string InterstitialAdUnitId = "f917e1f2a48df578";
#else // UNITY_ANDROID
    string InterstitialAdUnitId = "f917e1f2a48df578";
#endif
    int InterstitialRetryAttempt;


#if UNITY_IOS
string RewardedAdUnitId = "ecac1fabfefcc279";
#else // UNITY_ANDROID
    string RewardedAdUnitId = "ecac1fabfefcc279";
#endif
    int RewardedRetryAttempt;


#if UNITY_IOS
string bannerAdUnitId = "1d2be55efbebc2a1"; // Retrieve the ID from your account
#else // UNITY_ANDROID
    string bannerAdUnitId = "1d2be55efbebc2a1"; // Retrieve the ID from your account
#endif
    private string InterstitialAd_ErrorCode="";
    private string RewardedAd_ErrorCode="";

    // Start is called before the first frame update
    void Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads

            InitializeBannerAds();
            InitializeInterstitialAds();
            InitializeRewardedAds();

            Debug.Log("AppLovin SDK is initialized, start loading ads");
        };

        MaxSdk.SetSdkKey("_nIDtARQ2QQGw1zbkXrZ-NC3M2UFVsEUdWiUh9wlZK0TM" +
            "-44VvdUHrXdt1uMt6rUdT-Jhu2FD2kZ0pdWiG_-5W");
        MaxSdk.SetUserId("USER_ID");
        List<string> strings = new List<string>
        {
            "f917e1f2a48df578",
            "ecac1fabfefcc279",
            "1d2be55efbebc2a1",
        };
        //MaxSdk.InitializeSdk(strings.ToArray());
        MaxSdk.InitializeSdk();


       // StartCoroutine(AdLoadJudgeIEnumerator());

    }
    IEnumerator AdLoadJudgeIEnumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (!MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
            {
                LoadRewardedAd();
            }
            if (!MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
            {
                LoadInterstitialAd();
            }
        }
    }
   public  Text logText;
    private void OnLogMessageThread(string condition, string stackTrace, LogType type)
    {
        if (logText != null)
        {
            logText.text += "\n";

            logText.text += condition;

        }
    }
    public void ShowMediationDebugger()
    {
        MaxSdk.ShowMediationDebugger();
    }

    public void ClearDeBug()
    {
        logText.text = "";
    }


    #region �������
    float InterstitialAdCD = GameDefine.InterstitialAdCD;
    public void InitializeInterstitialAds()
    {
        // ��Ӳ�ҳʽ�����سɹ��¼��������
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;

        // ��Ӳ�ҳʽ������ʧ���¼��������
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;

        // ��Ӳ�ҳʽ�����ʾ�¼��������
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;

        // ��Ӳ�ҳʽ������¼��������
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;

        // ��Ӳ�ҳʽ��������¼��������
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;

        // ��Ӳ�ҳʽ�����ʾʧ���¼��������
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;


        // Load the first interstitial
        LoadInterstitialAd();
    }

    private void LoadInterstitialAd()
    {
        MaxSdk.LoadInterstitial(InterstitialAdUnitId);
    }

    /// <summary>
    /// ���ز������ǰ
    /// </summary>
    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'
        InterstitialAd_ErrorCode = "0";
        // Reset retry attempt
        InterstitialRetryAttempt = 0;
        InSertAdSources = adInfo.NetworkName;

    }
    /// <summary>
    /// ���ز������ʧ��
    /// </summary>
    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)
        InterstitialAd_ErrorCode = errorInfo.ToString();
        InterstitialRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, InterstitialRetryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    /// <summary>
    /// ������濪ʼ����
    /// </summary>
    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        
        InterstitialAdCD = GameDefine.InterstitialAdCD;

        StopTime();
        InAdPlayStatus = AdPlayStatus.Success;
        Debug.Log("������沥�ųɹ��ص�");
    }
    /// <summary>
    /// ������沥��ʧ��
    /// </summary>
    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        LoadInterstitialAd();
        InAdPlayStatus = AdPlayStatus.Fail;
        InterstitialAd_ErrorCode = errorInfo.ToString();

        Debug.Log("������沥��ʧ�ܻص�");
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("�������Hide");
        NotStopTime();
        InterstitialAdCD = GameDefine.InterstitialAdCD;
        // Interstitial ad is hidden. Pre-load the next ad.
        adIsEnd = true;
        LoadInterstitialAd();
    }

    #endregion

    #region �������


    private Action rewardedAction;
    private Action rewardAdPlaySuccess;
    public void InitializeRewardedAds()
    {
        // Attach callback
        // ��Ӽ��������سɹ��¼��������
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;

        // ��Ӽ���������ʧ���¼��������
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;

        // ��Ӽ��������ʾ�¼��������
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;

        // ��Ӽ���������¼��������
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;

        // ��Ӽ����������֧���¼��������
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;

        // ��Ӽ�����������¼��������
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;

        // ��Ӽ��������ʾʧ���¼��������
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;

        // ��Ӽ�������յ������¼��������
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;


        // Load the first rewarded ad
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(RewardedAdUnitId);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        RewardedRetryAttempt = 0;
        RewardedAd_ErrorCode = "0";

        rewardAdSources = adInfo.NetworkName;
       // Debug.LogError("rewardAdSources->" + rewardAdSources);
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        RewardedRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, RewardedRetryAttempt));
        RewardedAd_ErrorCode = errorInfo.ToString();
        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        
        StopTime();
        rewardAdPlaySuccess?.Invoke();
        rewardAdPlaySuccess = null;
        ReAdPlayStatus = AdPlayStatus.Success;

        Debug.Log("������沥�ųɹ��ص�");

    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        RewardedAd_ErrorCode = errorInfo.ToString();
        LoadInterstitialAd();
        ReAdPlayStatus = AdPlayStatus.Fail;
        Debug.Log("������沥��ʧ�ܻص�");


    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

        NotStopTime();
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
        adIsEnd = true;

        Debug.Log("�������Hide");
    }

    /// <summary>
    /// ��һ�ý���
    /// </summary>
    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        if(InterstitialAdCD<20f)
        {
            InterstitialAdCD = 20f;
        }
        rewardedAction?.Invoke();
        rewardedAction = null;
        // The rewarded ad displayed and the user should receive the reward.
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Ad revenue paid. Use this callback to track user revenue.
    }


    #endregion


    #region ������


    public void InitializeBannerAds()
    {
        // Adaptive banners are sized based on device width for positions that stretch full width (TopCenter and BottomCenter).
        // You may use the utility method `MaxSdkUtils.GetAdaptiveBannerHeight()` to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        MaxSdk.SetBannerExtraParameter(bannerAdUnitId, "adaptive_banner", "true");

        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, bannerAdsBgColor);

        // ��Ӻ�������سɹ��¼��������
        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;

        // ��Ӻ��������ʧ���¼��������
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;

        // ��Ӻ��������¼��������
        MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;

        // ��Ӻ���������֧���¼��������
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;

        // ��Ӻ�������չ�¼��������
        MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;

        // ��Ӻ������۵��¼��������
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;

    }

    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) { }

    private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    #endregion
   
    /// <summary>
    /// ��ʾ�������
    /// </summary>
    public void ShowInterstitial()
    {
        if (InterstitialAdCD > 0)
        {
            Debug.Log("InterstitialAdCD>0  " + InterstitialAdCD);
            return;
        }
        InAdPlayStatus = AdPlayStatus.None;
        TalkingDataManager.Instance.Send_IN_TryBegin(InterstitialAd_ErrorCode);
        // Ȼ������Ҫʱ������Ƿ�������
        if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
        {

            InterstitialAdCD = GameDefine.InterstitialAdCD;
            adIsEnd = false;
            InvokeManager.Instance.InvokeOneActionWhen(() =>
            {
                
                TalkingDataManager.Instance.Send_IN_End_(InterstitialAd_ErrorCode);

            }, () =>
            {
           //     Debug.Log("Send_IN_End_->adIsEnd" + adIsEnd);
                return adIsEnd == true;
            });

            TalkingDataManager.Instance.Send_IN_Begin_(InterstitialAd_ErrorCode);
            
            GameAnalyticsManager.Instance.SendShowInterstitialVideo();
           
            //Debug.Log("GameAnalyticsManager AdManager -> ���Ų������ɹ�");
            // �������Ѽ��أ���ʾ��
            MaxSdk.ShowInterstitial(InterstitialAdUnitId);
            

            //Debug.Log("GameAnalyticsManager AdManager -> ���Ų������ɹ� ֮������");

        }
        else
        {
            TalkingDataManager.Instance.Send_IN_Err(InterstitialAd_ErrorCode);
            GameAnalyticsManager.Instance.SendFailShowInterstitialVideo();
            //Debug.Log("GameAnalyticsManager AdManager -> ���Ų������ʧ��");
            InAdPlayStatus = AdPlayStatus.Fail;
            LoadInterstitialAd();
           // Debug.Log("GameAnalyticsManager AdManager -> ���Ų������ʧ�� ֮������");

        }
    }
    /// <summary>
    /// ��ʾ�������
    /// </summary>
    public void ShowRewardedAd(Action action, Action rewardAdPlaySuccess=null)
    {
     //   Debug.Log("ShowRewardedAd -> 00");
        ReAdPlayStatus = AdPlayStatus.None;

      //  TalkingDataManager.Instance.Send_IN_TryBegin(InterstitialAd_ErrorCode);
        TalkingDataManager.Instance.Send_RV_TryBegin_(RewardedAd_ErrorCode);
        //Debug.Log("ShowRewardedAd -> 01");
        ReAdPlayStatus = AdPlayStatus.None;
       // Debug.Log("ShowRewardedAd -> 02");

        if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
        {
         //   Debug.Log("ShowRewardedAd -> 03");

            rewardedAction = action;
          //  Debug.Log("ShowRewardedAd -> 04");

            this.rewardAdPlaySuccess = rewardAdPlaySuccess;
          //  Debug.Log("ShowRewardedAd -> 05");

            adIsEnd = false;
          //  Debug.Log("ShowRewardedAd -> 08");

            InvokeManager.Instance.InvokeOneActionWhen(() =>
            {
           //     Debug.Log("ShowRewardedAd -> 09");

                TalkingDataManager.Instance.Send_RV_End_(RewardedAd_ErrorCode);
            //    Debug.Log("ShowRewardedAd -> 10");

            }, () =>
            {

                return adIsEnd == true;
            });

            TalkingDataManager.Instance.Send_RV_Begin_(RewardedAd_ErrorCode);
          //  Debug.Log("ShowRewardedAd -> 06");

            GameAnalyticsManager.Instance.SendShowRewardedVideo();
          //  Debug.Log("ShowRewardedAd -> 07");

           
          //  Debug.Log("ShowRewardedAd -> 11");

          //  Debug.Log("GameAnalyticsManager AdManager -> ���ż������ɹ�");
            MaxSdk.ShowRewardedAd(RewardedAdUnitId);
          //  Debug.Log("ShowRewardedAd -> 12");

          //  Debug.Log("GameAnalyticsManager AdManager -> ���ż������ɹ�  ֮������");

        }
        else
        {
          //  Debug.Log("ShowRewardedAd -> 13");

            TalkingDataManager.Instance.Send_RV_Err_(RewardedAd_ErrorCode);
          //  Debug.Log("ShowRewardedAd -> 14");

            GameAnalyticsManager.Instance.SendFailShowRewardedVideo();
         //   Debug.Log("ShowRewardedAd -> 15");

          //  Debug.Log("GameAnalyticsManager AdManager -> ���ż������ʧ��");
            ReAdPlayStatus = AdPlayStatus.Fail;
            LoadRewardedAd();
        //    Debug.Log("ShowRewardedAd -> 16");

          //  Debug.Log("GameAnalyticsManager AdManager -> ���ż������ʧ��  ֮������");


        }
    }
    private void Update()
    {
        InterstitialAdCD -= Time.deltaTime;
        Debug.Log("InterstitialAdCD->" + InterstitialAdCD);
    }
    /// <summary>
    /// ��ʾ�������
    /// </summary>
    public void ShowBanner()
    {
        MaxSdk.ShowBanner(bannerAdUnitId);
    }
    public void HideBanner()
    {
        MaxSdk.HideBanner(bannerAdUnitId);
    }
    public void DestroyBanner()
    {
        MaxSdk.DestroyBanner(bannerAdUnitId);
    }

    private void StopTime()
    {
        Time.timeScale = 0;

        AudioManager.Instance.StopAudio();
    }
    private void NotStopTime()
    {
        Time.timeScale = 1;

        AudioManager.Instance.UndStopAudio();
    }
}

