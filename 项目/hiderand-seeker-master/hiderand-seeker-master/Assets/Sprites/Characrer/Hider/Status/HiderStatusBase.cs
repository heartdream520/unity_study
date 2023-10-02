
using HiderStatus;
using UnityEngine;

public abstract class HiderStatusBase: ICharacterStatus
{
    public HiderStatusControl statusControl;
    public HiderThrowLayerStatusControl throwStatusControl;
    public Hider hider;
    public Rigidbody rigidbody => hider.rigidbody__;
    public Animator animator => hider.animator;
    public Transform hiderTransform => hider.transform;

    public HiderStatusBase(HiderStatusControl statusControl)
    {
        this.statusControl = statusControl;
        hider = statusControl.hider;
        InitStatus();
    }
    public HiderStatusBase(HiderThrowLayerStatusControl statusControl)
    {
        this.throwStatusControl = statusControl;
        hider = statusControl.hider;
        InitStatus();
    }
    /// <summary>
    /// 初始化状态信息
    /// </summary>
    public virtual void InitStatus()
    {

    }
    public virtual void OnOneGameEnd()
    {

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
}
