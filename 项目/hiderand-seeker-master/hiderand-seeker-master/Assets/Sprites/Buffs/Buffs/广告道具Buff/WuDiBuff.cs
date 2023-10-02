

using UnityEngine;

public class WuDiBuff : GuangGaoBuffBase
{
    public Hider hider;
    public WuDiBuff(Hider hider) : base(GameDefine.WuDiBuffLastTime)
    {
        this.hider = hider;
    }
    int beforeLayer;
    private GameObject baoHuZhaoEffectObject;
    public override void OnBuffBegin()
    {
        base.OnBuffBegin();
        beforeLayer = hider.gameObject.layer;
        hider.gameObject.layer = LayerMask.NameToLayer(GameDefine.PlayerWuDiLayer);
        baoHuZhaoEffectObject = EffectManager.Instance.CreateBaoHuZhao();
        baoHuZhaoEffectObject.transform.SetParent(hider.transform, false);
    }

    public override void OnBuffEnd()
    {
        base.OnBuffEnd();
        hider.gameObject.layer = beforeLayer;
        ObjectPoolManager.Instance.SetOneObjectInPool(baoHuZhaoEffectObject);
    }

    public override void OnBuffUpdata()
    {
        base.OnBuffUpdata();
        hider.gameObject.layer = LayerMask.NameToLayer(GameDefine.PlayerWuDiLayer);
    }
}