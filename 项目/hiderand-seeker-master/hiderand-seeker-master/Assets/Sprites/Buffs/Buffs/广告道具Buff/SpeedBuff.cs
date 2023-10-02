


using UnityEngine;

public class SpeedBuff : GuangGaoBuffBase
{

    public CharacterBase characterBase__;
    public SpeedBuff(CharacterBase characterBase) : base(GameDefine.SpeedBuffLastTime)
    {
        this.characterBase__ = characterBase;
    }
    private GameObject jiaSuEffectObject;
    private float beginSpeed;
    public override void OnBuffBegin()
    {
        base.OnBuffBegin();
        beginSpeed = characterBase__.NowSpeed;
        characterBase__.NowSpeed *= GameDefine.SpeedBuffUpLv;
        jiaSuEffectObject = EffectManager.Instance.CreateJiaSuDeYan();
        jiaSuEffectObject.transform.SetParent(characterBase__.transform, false);


    }

    public override void OnBuffEnd()
    {
        base.OnBuffEnd();
        characterBase__.NowSpeed = beginSpeed;
        ObjectPoolManager.Instance.SetOneObjectInPool(jiaSuEffectObject);
        jiaSuEffectObject.SetActive(false);

    }

    public override void OnBuffUpdata()
    {
        base.OnBuffUpdata();
        //jiaSuEffectObject.transform.localPosition = Vector3.zero - characterBase__.transform.forward * 2f;
        if (characterBase__ is Hider)
        {
            if ((characterBase__ as Hider).statusControl.nowStatusEnum == MyEnum.HiderStatusEnum.Run
                && (characterBase__ as Hider).HasBeenAttack == false)
            {
                jiaSuEffectObject.SetActive(true);
            }
            else jiaSuEffectObject.SetActive(false);
        }
        if (characterBase__ is Seeker)
        {
            if ((characterBase__ as Seeker).statusControl.nowStatusEnum == MyEnum.SeekerStatusEnum.Run)
            {
                jiaSuEffectObject.SetActive(true);
            }
            else jiaSuEffectObject.SetActive(false);
        }
    }
}