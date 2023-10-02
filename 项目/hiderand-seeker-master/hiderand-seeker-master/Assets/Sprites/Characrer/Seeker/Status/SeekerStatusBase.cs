
using Newtonsoft.Json.Bson;
using SeekerStatus;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class SeekerStatusBase : ICharacterStatus
{
    public SeekerStatusControl statusControl;
    public SeekerThrowLayerStatusControl throwStatusControl;
    public Seeker seeker;
    public Rigidbody rigidbody => seeker.rigidbody__;
    public Animator animator => seeker.animator;
    public Transform transform => seeker.transform;

    public LayerMask seekForHiderLayerMask;
    protected float LookForHiderSpaceTime;

    
    public SeekerStatusBase(SeekerStatusControl statusControl)
    {
        
        this.statusControl = statusControl;
        seeker = statusControl.seeker;
        InitStatus();
    }
    public SeekerStatusBase(SeekerThrowLayerStatusControl throwStatusControl)
    {
        
        this.throwStatusControl = throwStatusControl;
        seeker = throwStatusControl.seeker;
        InitStatus();
    }
    public bool canSeekForHider;
    public virtual void InitStatus()
    {
        seekForHiderLayerMask =
            LayerMask.GetMask(GameDefine.Player_Layer) |
            LayerMask.GetMask(GameDefine.Men_Layer) |
            LayerMask.GetMask(GameDefine.Qiang_Layer) |
            LayerMask.GetMask(GameDefine.Waiqiang_Layer) |
            LayerMask.GetMask(GameDefine.NotCollisionWithPlayer_Layer
            );
        EventManager.Instance.BeforeTimeBeginFlowAction += this.OnBeforeTimeBeginflow;
        EventManager.Instance.TimeOutGameBeginAction += this.OnTimeOutGameBegin;
    }
    public virtual void OnOneGameEnd()
    {
        canSeekForHider = false;
        EventManager.Instance.BeforeTimeBeginFlowAction -= this.OnBeforeTimeBeginflow;
        EventManager.Instance.TimeOutGameBeginAction -= this.OnTimeOutGameBegin;
    }
    private void OnBeforeTimeBeginflow()
    {
        this.canSeekForHider = false;
    }
    private void OnTimeOutGameBegin()
    {
        this.canSeekForHider = true;
    }
    public virtual void OnBegin()
    {

    }
    public virtual void OnEnd()
    {

    }
    public virtual void OnUpdata()
    {

    }

    /// <summary>
    /// 寻找Hider
    /// </summary>
    public void LookForHider()
    {
        //return;
        if (!GameingMainManager.Instance.seekerCanAttack|| GameingMainManager.Instance.oneGameIsEnd
            || !canSeekForHider)
        {
            return;
        }
        this.LookForHiderSpaceTime -= Time.deltaTime;

        if (LookForHiderSpaceTime > 0) return;
        LookForHiderSpaceTime = seeker.LookForHiderSpaceTime;

        Vector3 vector = transform.position;
        Vector3 X_Ray_Position = new Vector3(vector.x, vector.y + 1f, vector.z);
        var hids = X_rayInspection.Instance.Sector_Inspection
             (X_Ray_Position, this.transform.forward, seeker.LookForHiderMaxDis,
             seeker.lookAccurate, seeker.lookAngle, seeker.LookForHiderSpaceTime, 
             seekForHiderLayerMask);
        foreach(var x in hids)
        {
            if (x.collider == null) continue;
            if (x.collider.transform.tag == GameDefine.HiderTag)
            {
                
                if (x.collider.gameObject.layer ==
                    LayerMask.GetMask(GameDefine.PlayerWuDiLayer))
                {
                    continue;
                }
                    
                    //Debug.Log("抓住了Hider:" + x.collider.name);
                statusControl.AttackHider(x.collider.gameObject);

                AudioManager.Instance.PlaySeekerAttack();
                //是AIInput立刻停止
                if(x.collider.GetComponent<Hider>().isAIInput)
                {
                    x.collider.GetComponent<Hider>().aiInput.navMeshAgent.speed = 0;
                }
                else
                {
                    x.collider.GetComponent<Hider>().
                        statusControl.SetStatus(MyEnum.HiderStatusEnum.BeenAttack);
                }

                canSeekForHider = false;
                return;


            }
        }
    }
    public string GetAnimatorNowPlaying()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            return clipInfo[0].clip.name;
        }
        return "";
        
    }
    
}