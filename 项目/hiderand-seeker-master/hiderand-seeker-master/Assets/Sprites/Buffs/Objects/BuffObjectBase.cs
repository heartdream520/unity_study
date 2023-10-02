
using System;
using UnityEngine;

public class BuffObjectBase : MonoBehaviour
{
    public virtual void Start()
    {
        InitBuffObject();
    }
    /// <summary>
    /// ��ʼ��buffBase ��Start�е���
    /// </summary>
    public virtual void InitBuffObject()
    {

    }
    /// <summary>
    /// ��ĳ����������Buff
    /// </summary>
    /// <param name="gameObject"></param>
    public virtual void SetBuff(GameObject gameObject)
    {
        if (!GameingMainManager.Instance.GameIsBegin) return;
        if (gameObject.tag == GameDefine.HiderTag || gameObject.tag == GameDefine.SeekerTag)
        {
            var x = gameObject.GetComponent<CharacterBase>();
            if (x is Hider)
            {
                DoHiderBuff(x as Hider);
            }
            else if (x is Seeker)
            {
                DoSeekerBuff(x as Seeker);
            }
        }
    }

    public virtual void DoHiderBuff(Hider hider)
    {
    }
    public virtual void DoSeekerBuff(Seeker seeker)
    {
    }
    public virtual void OnTriggerExit(Collider other)
    {
        
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        
    }
    public virtual void OnTriggerStay(Collider other)
    {
        
    }


}
