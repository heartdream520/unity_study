using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum RewardedAdEnum
{
    RV_Unlock_Skins_Shop,
    RV_Unlock_Skins_Level,
    RV_BeforeGameStart_SpeedBuff,
    RV_Gameing_SpeedBuff,
    RV_Gameing_InvisibilityBuff,
    RV_Gameing_ThroughWallsBuff,
    RV_Gameing_XRayBuff,
    RV_JumpLevel_Seeker,
    RV_Relive_Seeker,
    RV_JumpLevel_Hider,
    RV_Relive_Hider,
    RV_Coin5XReward,
}
public class RewardedAdManager : Singleton<RewardedAdManager>
{
    bool canTryPlay=true;
    public void PlayAd(RewardedAdEnum rewardedAdEnum,Action reward, Action playSuccess=null)
    {
        if(TestManager.Instance.noAds)
        {
            playSuccess?.Invoke();
            reward?.Invoke();
            return;
        }

       if(canTryPlay)
        {
            Debug.Log("AdManager 尝试播放激励广告");

            MaxManager.Instance.Placement = rewardedAdEnum.ToString();

            MaxManager.Instance.ShowRewardedAd(reward, playSuccess);
            canTryPlay = false;
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                canTryPlay = true;
            }, 1.1f);
        }
       else
        {
            Debug.Log("AdManager 不能尝试播放激励广告");
        }
        
    }

}
