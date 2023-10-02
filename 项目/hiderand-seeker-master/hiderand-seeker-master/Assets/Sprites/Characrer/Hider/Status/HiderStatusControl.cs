using UnityEngine;
using MyEnum;
using UnityEngine.Windows;
using System.Collections.Generic;
using HiderStatus;

public class HiderStatusControl
{
    private Dictionary<MyEnum.HiderStatusEnum, HiderStatusBase> statusDic;
    public Hider hider;
    public HiderStatusBase nowStatus=null;
    public HiderStatusEnum nowStatusEnum;

    /// <summary>
    /// 是否在水中
    /// </summary>
    public bool isInWater;
    public int inWaterCount;
    Transform transform => hider.transform;
    public HiderStatusControl(Hider hider)
    {
        isInWater = false;
        this.hider = hider;
        Init();
    }
    private void Init()
    {
        nowStatusEnum = HiderStatusEnum.None;
        statusDic = new Dictionary<HiderStatusEnum, HiderStatusBase>
        {
            { HiderStatusEnum.Idle, new IdleStatus(this) },
            { HiderStatusEnum.Run, new RunStatus(this) },
            { HiderStatusEnum.BeenAttack, new BeenAttackStatus(this) },
            { HiderStatusEnum.Swin, new SwinStatus(this) },
            { HiderStatusEnum.Win, new WinStatus(this) },
            { HiderStatusEnum.Fail, new FailStatus(this) },
            { HiderStatusEnum.AIFail, new AIFailStatus(this) },
        };
        this.SetStatus(HiderStatusEnum.Idle);
    }
    public void OnOneGameEnd()
    {
        foreach(var x in this.statusDic.Values)
        {
            x.OnOneGameEnd();
        }
    }
    internal void Update()
    {

        Vector2 vector = hider.InputXY;
        if (nowStatusEnum != MyEnum.HiderStatusEnum.BeenAttack
            && nowStatusEnum != HiderStatusEnum.Fail
            && nowStatusEnum != HiderStatusEnum.AIFail
            && nowStatusEnum != HiderStatusEnum.Win)
        {
            if (vector.magnitude >= 0.5f)
            {
                if (!isInWater)
                    SetStatus(MyEnum.HiderStatusEnum.Run);
                else
                    SetStatus(MyEnum.HiderStatusEnum.Swin);
            }
            else
            {
                SetStatus(MyEnum.HiderStatusEnum.Idle);
            }
        }

        nowStatus.OnUpdata();

    }
    public void SetStatus(HiderStatusEnum statusEnum)
    {
        if (nowStatus != null && nowStatusEnum == statusEnum)
        {
            return;
        }
        if (nowStatus != null)
            nowStatus.OnEnd();
        nowStatus = this.statusDic[statusEnum];
        nowStatus.OnBegin();
        nowStatusEnum = statusEnum;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameDefine.WaterTag)
        {
            inWaterCount++;
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
            --inWaterCount;
            if (inWaterCount == 0)
                this.isInWater = false;

        }
    }
}