


using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class GoThroughtWallsBuff : GuangGaoBuffBase
{
    public Hider hider;
    public Seeker seeker;
    BanTouMingMaterialBuff banTouMingMaterialBuff;
    public GoThroughtWallsBuff(Hider hider) : base(GameDefine.GoThroughtWallsBuffLastTime)
    {
        this.hider = hider;
    }
    public GoThroughtWallsBuff(Seeker seeker) : base(GameDefine.GoThroughtWallsBuffLastTime)
    {
        this.seeker = seeker;
    }
    private Dictionary<GameObject, int> gameObjectBeforeLayer;
    public override void OnBuffBegin()
    {
        base.OnBuffBegin();
        gameObjectBeforeLayer = new Dictionary<GameObject, int>();
        SetLayeInGameObjectAndChilder
            (GameingMainManager.Instance.GameMapCenter.GoThroughtWallssObjectParent,
            GameDefine.NotCollisionWithPlayer_Layer);

        if(hider!=null)
        {
            banTouMingMaterialBuff = new BanTouMingMaterialBuff(hider,
                GameDefine.GoThroughtWallsBuffLastTime);
            hider.playerBuffcontrol.AddOneBuff(banTouMingMaterialBuff as MaterialBuffBase);
        }
        else
        {
            banTouMingMaterialBuff = new BanTouMingMaterialBuff(seeker,
                GameDefine.GoThroughtWallsBuffLastTime);
            seeker.playerBuffcontrol.AddOneBuff(banTouMingMaterialBuff as MaterialBuffBase);
        }
    }

    public override void OnBuffEnd()
    {
        base.OnBuffEnd();
        HuiFuBeforeLayer();
        if (hider != null)
        {
            hider.playerBuffcontrol.OnOneBuffEnd<MaterialBuffBase>();
        }
        if (seeker != null)
        {
            seeker.playerBuffcontrol.OnOneBuffEnd<MaterialBuffBase>();
        }
    }

    private void HuiFuBeforeLayer()
    {
        foreach(var x in this.gameObjectBeforeLayer)
        {
            x.Key.layer = x.Value;
        }
    }

    private void SetLayeInGameObjectAndChilder(GameObject go, string LayerString)
    {
        int layer = LayerMask.NameToLayer(LayerString);
        Queue<GameObject> q = new Queue<GameObject>();
        q.Enqueue(go);
        while (q.Count() != 0)
        {
            
            var x = q.Dequeue();
            gameObjectBeforeLayer.Add(x, x.layer);
            x.layer = layer;
            for (int i = 0; i < x.transform.childCount; i++)
            {
                q.Enqueue(x.transform.GetChild(i).gameObject);
            }
        }

    }
}