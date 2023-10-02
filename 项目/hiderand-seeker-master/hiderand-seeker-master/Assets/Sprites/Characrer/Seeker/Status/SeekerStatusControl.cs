using UnityEngine;
using MyEnum;
using System.Collections.Generic;
using SeekerStatus;
using Unity.VisualScripting;

public class SeekerStatusControl
{
    private Dictionary<SeekerStatusEnum, SeekerStatusBase> statusDic;
    public Seeker seeker;
    public SeekerStatusBase nowStatus = null;
    public SeekerStatusEnum nowStatusEnum;
    public SeekerStatusControl(Seeker seeker)
    {
        this.seeker = seeker;
        Init();
        isInWater = false;
    }
    /// <summary>
    /// 是否在水中
    /// </summary>
    public bool isInWater;
    private void Init()
    {
        nowStatusEnum = SeekerStatusEnum.None;
        statusDic = new Dictionary<SeekerStatusEnum, SeekerStatusBase>
        {
            { SeekerStatusEnum.Idle, new IdleStatus(this) },
            { SeekerStatusEnum.Run, new RunStatus(this) },
            { SeekerStatusEnum.Attack, new AttackStatus(this) },
            { SeekerStatusEnum.Swin, new SwinStatus(this) },
            { SeekerStatusEnum.HoldHead, new HoldHeadStatus(this) },
            { SeekerStatusEnum.Win, new WinStatus(this) },
            { SeekerStatusEnum.Fail, new FailStatus(this) },
            { SeekerStatusEnum.AIFail, new AIFailStatus(this) },
        };

        EventManager.Instance.BeforeTimeBeginFlowAction += OnBeforeTimeBeginFlow;
        EventManager.Instance.TimeOutGameBeginAction += this.OnTimeOutGameBegin;
        this.SetStatus(SeekerStatusEnum.Idle);


    }

    private void OnBeforeTimeBeginFlow()
    {
        this.SetStatus(SeekerStatusEnum.HoldHead);
    }

    public void OnOneGameEnd()
    {
        foreach (var x in this.statusDic.Values)
        {
            x.OnOneGameEnd();
        }
        EventManager.Instance.TimeOutGameBeginAction -= this.OnTimeOutGameBegin;
        EventManager.Instance.BeforeTimeBeginFlowAction -= OnBeforeTimeBeginFlow;
    }

    private void OnTimeOutGameBegin()
    {
        this.SetStatus(SeekerStatusEnum.Idle);
    }

    internal void Update()
    {
        //Debug.LogWarning(this.nowStatusEnum);
        nowStatus.OnUpdata();
        if (nowStatusEnum != SeekerStatusEnum.Attack &&
            nowStatusEnum != SeekerStatusEnum.HoldHead &&
              nowStatusEnum != SeekerStatusEnum.Fail &&
              nowStatusEnum != SeekerStatusEnum.AIFail &&
              nowStatusEnum != SeekerStatusEnum.Win)
        {
            Vector2 vector = seeker.InputXY;
            if (vector.magnitude >= 0.5f)
            {
                //Debug.LogWarning("SetStatus(MyEnum.SeekerStatusEnum.Run);");
                if (isInWater)
                {
                    SetStatus(MyEnum.SeekerStatusEnum.Swin);
                }
                else
                    SetStatus(MyEnum.SeekerStatusEnum.Run);
            }
            else
            {
                //Debug.LogWarning(" SetStatus(MyEnum.SeekerStatusEnum.Idle);");
                SetStatus(MyEnum.SeekerStatusEnum.Idle);
            }
        }



    }
    public void SetStatus(SeekerStatusEnum statusEnum)
    {
        if (nowStatus != null && nowStatusEnum == statusEnum)
        {
            return;
        }
        if (statusEnum == SeekerStatusEnum.Win || statusEnum == SeekerStatusEnum.Fail)
        {
            InvokeManager.Instance.InvokeOneActionWhen(() =>
            {
                ToSetStatus(statusEnum);
            }, () =>
            {
                return !GameingMainManager.Instance.attackIng;
            });
        }
        else
            ToSetStatus(statusEnum);
    }
    private  void ToSetStatus(SeekerStatusEnum statusEnum)
    {
        if (nowStatus != null)
            nowStatus.OnEnd();

        nowStatus = this.statusDic[statusEnum];
        nowStatus.OnBegin();
        nowStatusEnum = statusEnum;
    }
    internal void AttackHider(GameObject gameObject)
    {
        this.SetStatus(SeekerStatusEnum.Attack);
        (nowStatus as AttackStatus).AttackHider(gameObject);

        seeker.FaceToGameObject(gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameDefine.WaterTag)
        {
            this.isInWater = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == GameDefine.WaterTag)
        {
            this.isInWater = false;

        }
    }
}