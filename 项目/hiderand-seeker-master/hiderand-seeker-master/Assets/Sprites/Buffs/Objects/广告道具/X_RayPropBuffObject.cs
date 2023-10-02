
using UnityEngine;

public class X_RayPropBuffObject : AdvertisingPropsBase
{
  

    public override void DoSeekerBuff(Seeker seeker)
    {
        base.DoSeekerBuff(seeker);
        if (seeker.isAIInput) return;


        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Gameing_XRayBuff, () =>
        {
            X_RayBuff buff = new X_RayBuff(seeker);
            seeker.playerBuffcontrol.AddOneBuff((GuangGaoBuffBase)buff);

            AudioManager.Instance.PlayX_Ray_Sound();

            var effect = EffectManager.Instance.CreateX_RaySaoMiao();
            effect.transform.SetParent(seeker.transform, false);
            ObjectPoolManager.Instance.SetOneObjectInPool(effect, 1f);

            ReviceItself(GameDefine.X_RayBuffLastTime);
            EventManager.Instance.CallOnPlayerGetGuangGaoBuffAction(buff);


        });
       

        //ReviceItself();

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
            == MyEnum.Player_Identity_Enum.Hider)
            Destroy(gameObject);
    }


}