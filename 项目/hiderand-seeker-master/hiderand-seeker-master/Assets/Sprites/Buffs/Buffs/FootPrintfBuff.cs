using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintfBuff : BuffBase
{
    static List<GameObject> cats;
    static List<GameObject> dogs;
    public Hider hider;
    public Seeker seeker;
    static FootPrintfBuff()
    {
        cats = new List<GameObject>();
        dogs = new List<GameObject>();
        for(int i=1;i<=GameDefine.NiCount;i++)
        {
            cats.Add(Resloader.LoadGameObject(PathDefine.CatZhuaziPath + i));
            dogs.Add(Resloader.LoadGameObject(PathDefine.DogZhuaziPath + i));
        }
    }
    
    int id;
    public FootPrintfBuff(int id) : base(GameDefine.FootPrintfBuffLastTime)
    {
        this.id = id;
    }
    /// <summary>
    /// 生成脚印的间隔时间
    /// </summary>
    private float spaceTime = 0.5f / 3f;
    //生成脚印的倒计时
    private float beforeTime=0f;

    private float len=0.5f;
    /// <summary>
    /// 是加还是减
    /// </summary>
    private int xishu=-1;
    public override void OnBuffUpdata()
    {
        base.OnBuffUpdata();
        if(hider!=null)
        {
            if (hider.statusControl.nowStatusEnum != MyEnum.HiderStatusEnum.Run)
                return;
            if (!X_rayInspection.Instance.RayToDown
                (hider.transform.position, -hider.transform.up, 0.5f,
                1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer))
                && !X_rayInspection.Instance.IsInXiePo(hider.transform.position,
                hider.transform.right, hider.transform.forward,- hider.transform.up,
                1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
            {
                return;
            }
        }
        else
        {
            if (seeker.statusControl.nowStatusEnum != MyEnum.SeekerStatusEnum.Run)
                return;
            if (!X_rayInspection.Instance.RayToDown
               (seeker.transform.position, -seeker.transform.up, 0.5f,
               1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer ))
               && !X_rayInspection.Instance.IsInXiePo(seeker.transform.position,
                seeker.transform.right, seeker.transform.forward, -seeker.transform.up,
                1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
            {
                return;
            }
        }
        beforeTime -= Time.deltaTime;
        if (beforeTime <= 0)
        {
            
            if (hider != null)
            {
                if (TestManager.Instance.testMode)
                {
                    if (hider.isAIInput)
                        beforeTime = spaceTime / (hider.NowSpeed / 4f);
                    else beforeTime = spaceTime / (hider.NowSpeed / 5f);
                }
                else
                    beforeTime = spaceTime/(hider.NowSpeed/hider.baseRunSpeed);
                var go = ObjectPoolManager.Instance.GetOneObjectFromPool(cats[id - 1]);
                go.transform.eulerAngles =
                    new Vector3(go.transform.eulerAngles.x,
                   hider.transform.eulerAngles.y,
                    go.transform.eulerAngles.z);
                Vector3 x = hider.transform.position +
                    xishu * hider.transform.right * len * 0.7f;
                go.transform.position = new Vector3(x.x, x.y - 0.18f, x.z);
                xishu *= -1;

               

                float depth = 0.1f;
                while(!X_rayInspection.Instance.RayToDown
                (hider.transform.position, -hider.transform.up, depth,
                1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
                {
                    depth += 0.1f;
                }

                //EffectManager.Instance.CreateOneCaiNiHou(id, x-(depth-0.1f)*hider.transform.up);


                ObjectPoolManager.Instance.SetOneObjectInPool(go, GameDefine.FootDisPlayTime);

            }
            else
            {
                if(TestManager.Instance.testMode)
                {
                    if(seeker.isAIInput)
                        beforeTime = spaceTime / (seeker.NowSpeed / 3f);
                    else beforeTime = spaceTime / (seeker.NowSpeed / 5f);
                }
                else 
                beforeTime = spaceTime / (seeker.NowSpeed / seeker.baseRunSpeed);
                var go = ObjectPoolManager.Instance.GetOneObjectFromPool(dogs[id - 1]);
                go.transform.eulerAngles =
                    new Vector3(go.transform.eulerAngles.x,
                   seeker.transform.eulerAngles.y,
                    go.transform.eulerAngles.z);
                Vector3 x = seeker.transform.position +
                    xishu * seeker.transform.right * len * 0.6f;
                go.transform.position = new Vector3(x.x, x.y - 0.18f, x.z);
                xishu *= -1;

                float depth = 0.05f;
                while (!X_rayInspection.Instance.RayToDown
                (seeker.transform.position, -seeker.transform.up, depth,
                1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
                {
                    depth += 0.05f;
                }
                //EffectManager.Instance.CreateOneCaiNiHou(id, x - (depth - 0.05f) * seeker.transform.up);
               

                ObjectPoolManager.Instance.SetOneObjectInPool(go, GameDefine.FootDisPlayTime);

            }
        }
    }

    
    public override void OnBuffBegin()
    {
        
        base.OnBuffBegin();
        

    }

    public override void OnBuffEnd()
    {
        base.OnBuffEnd();
    }


}
