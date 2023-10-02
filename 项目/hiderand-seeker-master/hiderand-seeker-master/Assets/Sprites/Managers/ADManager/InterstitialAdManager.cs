using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InterstitialAdEnum
{
    I_Enter_settlement_Successful,
    I_settlement_Successful_Win_Next,
    I_settlement_Successful_Win_Replay,
    I_settlement_Successful_Fail_Replay,
}
public class InterstitialAdManager : Singleton<InterstitialAdManager>
{
    public void PlayAd(InterstitialAdEnum interstitialAdEnum)
    {
        if (TestManager.Instance.noAds)
        {
            return;
        }
        MaxManager.Instance.Placement = interstitialAdEnum.ToString();

        MaxManager.Instance.ShowInterstitial();

        //Debug.Log("AdManager ≥¢ ‘≤•∑≈≤Â∆¡π„∏Ê");
    }
}
