using UnityEngine;

public class BanTouMingMaterialBuff : MaterialBuffBase
{
    public BanTouMingMaterialBuff(Hider hider, float lastTime) : base(hider, lastTime)
    {

    }public BanTouMingMaterialBuff(Seeker seeker, float lastTime) : base(seeker, lastTime)
    {

    }

    public override void OnHiderBuffBegin()
    {
        base.OnHiderBuffBegin();
        hider.SetMaterBanTouMing();
    }

    public override void OnHiderBuffEnd()
    {
        base.OnHiderBuffEnd();
        hider.SetMaterBase();
    }

    public override void OnSeekerBuffBegin()
    {
        base.OnSeekerBuffBegin();
        seeker.SetMaterBanTouMing();
    }

    public override void OnSeekerBuffEnd()
    {
        base.OnSeekerBuffEnd();
        seeker.SetMaterBase();
    }
}