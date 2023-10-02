using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DeliverMenBase : MonoBehaviour
{
    /// <summary>
    /// 特效
    /// </summary>
    public ParticleSystem ParticleSystem;
    public int id;
    public DeliverMenBase otherDeliverMen;
    private bool canDeliver;
    private bool seekerCanDeliver;
    //冷却时间
    public float lengqueTime;
    public float lastTime;
    private void Start()
    {
        EventManager.Instance.TimeOutGameBeginAction += this.OnTimeOutGameBegin;
        EventManager.Instance.BeforeTimeBeginFlowAction += this.OnBeforeTimeBeginFlow;
        canDeliver = false;
        seekerCanDeliver = false;
        lastTime = 0f;
    }



    private void Update()
    {
        if (lastTime > 0) lastTime -= Time.deltaTime;
    }
    private void OnDisable()
    {
        EventManager.Instance.TimeOutGameBeginAction -= this.OnTimeOutGameBegin;
        EventManager.Instance.BeforeTimeBeginFlowAction -= this.OnBeforeTimeBeginFlow;
        seekerCanDeliver = false;
    }
    private void OnTimeOutGameBegin()
    {
        seekerCanDeliver = true;
    }
    private void OnBeforeTimeBeginFlow()
    {
        canDeliver = true;
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (!canDeliver) return;
        if (lastTime > 0) return;
        if (other.tag == GameDefine.HiderTag || other.tag == GameDefine.SeekerTag)
        {
            if (other.tag == GameDefine.HiderTag)
            {
                if (other.GetComponent<Hider>().HasBeenAttack) return;
            }
            if (other.tag == GameDefine.SeekerTag)
            {
                if (!this.seekerCanDeliver) return;
            }
            lastTime = lengqueTime;

            //添加的
            if (otherDeliverMen == null) GetOther();


            var x = otherDeliverMen.transform.position;
            otherDeliverMen.SetOneEffect();
            if(other.GetComponent<CharacterBase>().isAIInput)
            {
                if(other.tag==GameDefine.SeekerTag)
                {
                    other.GetComponent<Seeker>().aiInput.navMeshAgent.enabled = false;
                }
                else if (other.tag == GameDefine.HiderTag)
                {
                    other.GetComponent<Hider>().aiInput.navMeshAgent.enabled = false;
                }
            }
            other.transform.position = new Vector3(x.x, x.y , x.z);
            if (other.GetComponent<CharacterBase>().isAIInput)
            {
                if (other.tag == GameDefine.SeekerTag)
                {
                    other.GetComponent<Seeker>().aiInput.navMeshAgent.enabled = true;
                }
                else if (other.tag == GameDefine.HiderTag)
                {
                    other.GetComponent<Hider>().aiInput.navMeshAgent.enabled = true;
                }
            }
            else
            {
                AudioManager.Instance.PlayChuanSong();
            }
            SetOneEffect();
            
            other.GetComponent<CharacterBase>().BeginChuanSong();
        }
    }
    public virtual void GetOther()
    {

    }
    public virtual void SetOneEffect()
    {
        lastTime = lengqueTime;
        EffectManager.Instance.CreateOneChuanSong(id, this.transform.position);
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            ParticleSystem.Stop();
            ParticleSystem.Clear();
            ParticleSystem.Play();
        }, 0);

    }
}
