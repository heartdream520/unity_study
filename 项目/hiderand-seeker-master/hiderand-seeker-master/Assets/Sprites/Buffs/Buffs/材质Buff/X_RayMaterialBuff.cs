using UnityEngine;

public class X_RayMaterialBuff : MaterialBuffBase
{
    public X_RayMaterialBuff(Hider hider) : base(hider, GameDefine.X_RayBuffLastTime)
    {

    }
    
    public override void OnHiderBuffBegin()
    {
        base.OnHiderBuffBegin();
        Debug.Log(hider.name + " X_RayMaterialBuff -> OnHiderBuffBegin()");
        hider.SetMaterX_Ray();
    }

    public override void OnHiderBuffEnd()
    {
        base.OnHiderBuffEnd();
        Debug.Log(hider.name + " X_RayMaterialBuff -> OnHiderBuffEnd()");
        hider.SetMaterQuanTouMing();
    }
}