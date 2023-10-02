using UnityEngine;

public class ZangMaterialBuff : MaterialBuffBase
{
    public ZangMaterialBuff(Hider hider) : base(hider,  GameDefine.ZangMaterialBuffLastTime)
    {

    }
    public ZangMaterialBuff(Seeker seeker) : base(seeker, GameDefine.ZangMaterialBuffLastTime)
    {

    }

    public override void OnHiderBuffBegin()
    {
        base.OnHiderBuffBegin();
        hider.SetMaterZang();
    }

    public override void OnHiderBuffEnd()
    {

        base.OnHiderBuffEnd();
        float waitTime = 0;
        if (hider.HasBeenAttack)
        {
            waitTime = MyRandom.RangeFloat(2, 4);
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                hider.SetMaterBase();
            }, waitTime);
        }
        else
            hider.SetMaterQuanTouMing();


    }

    public override void OnSeekerBuffBegin()
    {
        base.OnSeekerBuffBegin();
        seeker.SetMaterZang();
    }

    public override void OnSeekerBuffEnd()
    {
        base.OnSeekerBuffEnd();
        seeker.SetMaterBase();
    }
}