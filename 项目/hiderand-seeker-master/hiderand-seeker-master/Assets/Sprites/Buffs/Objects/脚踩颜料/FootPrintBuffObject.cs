using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ω≈”°BuffŒÔÃÂ
/// </summary>
public class FootPrintBuffObject : BuffObjectBase
{
    public float jiangeTime = 0.2f;
    private float lastTime = 0.2f;
    public int id;
    public override void DoHiderBuff(Hider hider)
    {
        base.DoHiderBuff(hider);
        FootPrintfBuff buff = new FootPrintfBuff(id);
        buff.hider = hider;
        if (hider.playerBuffcontrol != null)
            hider.playerBuffcontrol.AddOneBuff(buff);
    }

    public override void DoSeekerBuff(Seeker seeker)
    {
        base.DoSeekerBuff(seeker);
        FootPrintfBuff buff = new FootPrintfBuff(id);
        buff.seeker = seeker;
        if (seeker.playerBuffcontrol != null)
            seeker.playerBuffcontrol.AddOneBuff(buff);

    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        SetBuff(other.gameObject);
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        SetBuff(other.gameObject);
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (other.tag != GameDefine.HiderTag && other.tag != GameDefine.SeekerTag)
        {
            return;
        }
            if (other.tag == GameDefine.HiderTag)
        {
            if (other.GetComponent<Hider>().statusControl.nowStatusEnum != MyEnum.HiderStatusEnum.Run)
                return;
        }
        else if (other.tag == GameDefine.SeekerTag)
        {
            if (other.GetComponent<Seeker>().statusControl.nowStatusEnum != MyEnum.SeekerStatusEnum.Run)
                return;
        }
        lastTime -= Time.deltaTime;
        
        if(lastTime<=0)
        {
            lastTime = jiangeTime;
            EffectManager.Instance.CreateOneCaiNi(id, other.transform.position);
        }
    }
}
