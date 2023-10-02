
using UnityEngine;

public class GoThroughtWallsPropBuffObject : AdvertisingPropsBase
{

    public override void DoHiderBuff(Hider hider)
    {
        base.DoHiderBuff(hider);
        if (hider.isAIInput) return;


        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Gameing_ThroughWallsBuff, () =>
        {
            GoThroughtWallsBuff buff = new GoThroughtWallsBuff(hider);
            hider.playerBuffcontrol.AddOneBuff((GuangGaoBuffBase)buff);

            ReviceItself(GameDefine.GoThroughtWallsBuffLastTime);

            EventManager.Instance.CallOnPlayerGetGuangGaoBuffAction(buff);
        });
        
    }

    public override void DoSeekerBuff(Seeker seeker)
    {
        base.DoSeekerBuff(seeker);
        if (seeker.isAIInput) return;

        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Gameing_ThroughWallsBuff, () =>
        {
            GoThroughtWallsBuff buff = new GoThroughtWallsBuff(seeker);
            seeker.playerBuffcontrol.AddOneBuff((GuangGaoBuffBase)buff);

            ReviceItself(GameDefine.GoThroughtWallsBuffLastTime);
            EventManager.Instance.CallOnPlayerGetGuangGaoBuffAction(buff);
        });
       


    }
    
}