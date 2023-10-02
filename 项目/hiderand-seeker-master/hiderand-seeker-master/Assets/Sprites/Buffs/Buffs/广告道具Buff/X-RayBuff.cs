


using System.Collections.Generic;

public class X_RayBuff : GuangGaoBuffBase
{
    public Seeker seeker;
    public X_RayBuff(Seeker seeker) : base(GameDefine.X_RayBuffLastTime)
    {
        this.seeker = seeker;
    }
    public override void OnBuffBegin()
    {
        base.OnBuffBegin();
        foreach (var x in GameingMainManager.Instance.gameingManager.hiders)
        {
            if (x.HasBeenAttack) continue;
            X_RayMaterialBuff buff = new X_RayMaterialBuff(x);
            
            x.playerBuffcontrol.AddOneBuff(buff as MaterialBuffBase);
        }
    }

    public override void OnBuffEnd()
    {
        base.OnBuffEnd();
        foreach (var x in GameingMainManager.Instance.gameingManager.hiders)
        {
            x.playerBuffcontrol.OnOneBuffEnd<MaterialBuffBase>();
        }
    }

    public override void OnBuffUpdata()
    {
        base.OnBuffUpdata();
    }
}