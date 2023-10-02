
using UnityEngine;

public class WuDiPropBuffObject : AdvertisingPropsBase
{

    public override void DoHiderBuff(Hider hider)
    {   
        base.DoHiderBuff(hider);
        if (hider.isAIInput) return;

        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Gameing_InvisibilityBuff, () =>
        {
            WuDiBuff buff = new WuDiBuff(hider);
            hider.playerBuffcontrol.AddOneBuff((GuangGaoBuffBase)buff);

            ReviceItself(GameDefine.WuDiBuffLastTime);
            EventManager.Instance.CallOnPlayerGetGuangGaoBuffAction(buff);


        });
       
    }
 
    public override void Start()
    {
        base.Start();
        EventManager.Instance.BeforeTimeBeginFlowAction += DestoryItSelf;
    }
    private void OnDestroy()
    {
        EventManager.Instance.BeforeTimeBeginFlowAction -= DestoryItSelf;

    }
    public override void DestoryItSelf()
    {
        base.DestoryItSelf();
        if (GameingMainManager.Instance.player_Identity_Enum
            == MyEnum.Player_Identity_Enum.Seeker)
            Destroy(gameObject);
    }
}