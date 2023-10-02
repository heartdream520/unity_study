
using UnityEngine;

public class SpeedPropBuffObject : AdvertisingPropsBase
{

    public override void DoHiderBuff(Hider hider)
    {
        base.DoHiderBuff(hider);
        if (hider.isAIInput) return;

        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Gameing_SpeedBuff, () =>
        {
            SpeedBuff buff = new SpeedBuff(hider);
            hider.playerBuffcontrol.AddOneBuff((GuangGaoBuffBase)buff);

            ReviceItself(GameDefine.SpeedBuffLastTime);
            EventManager.Instance.CallOnPlayerGetGuangGaoBuffAction(buff);
        });
      


    }

    public override void DoSeekerBuff(Seeker seeker)
    {
        base.DoSeekerBuff(seeker);
        if (seeker.isAIInput) return;

        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Gameing_SpeedBuff, () =>
        {
            SpeedBuff buff = new SpeedBuff(seeker);
            seeker.playerBuffcontrol.AddOneBuff((GuangGaoBuffBase)buff);

            ReviceItself(GameDefine.SpeedBuffLastTime);
            EventManager.Instance.CallOnPlayerGetGuangGaoBuffAction(buff);
        });
       


    }
}
