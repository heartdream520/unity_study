using MyEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public Action BeforeTimeBeginFlowAction;
    public void CallBeforeTimeBeginflowAction()
    {
        BeforeTimeBeginFlowAction?.Invoke();
    }
    public Action<float> BeforGameBeginTimeChangeAction;
    public void CallBeforGameBeginTimeChangeAction(float nowTime)
    {
        BeforGameBeginTimeChangeAction?.Invoke(nowTime);
    }
    /// <summary>
    /// 倒计时结束，游戏开始
    /// </summary>
    public Action TimeOutGameBeginAction;
    public void CallTimeOutGameBeginAction()
    {
        TimeOutGameBeginAction?.Invoke();
    }
    /// <summary>
    /// 游戏结束事件
    /// </summary>
    public Action<bool> OneGameEndAction;
    public void CallOneGameEndAction(bool iswin)
    {
        //if (GameingMainManager.Instance.oneGameIsEnd) return;
        OneGameEndAction?.Invoke(iswin);
        //InvokeManager.Instance.InvokeOneActiveAfterSeconds(()=>OneGameEndAction?.Invoke(),1f);
    }

    public Action OneGameFailAction;
    public void CallOneGameFailAction()
    {
        if (GameingMainManager.Instance.hasFailFuHuo)
            OneGameEndAction?.Invoke(false);
        else
            OneGameFailAction?.Invoke();
    }
    public Action OneGameFuHuoAction;
    public void CallOneGameFuHuoAction()
    {
        OneGameFuHuoAction?.Invoke();
    }


    /// <summary>
    /// Hider 被攻击时的事件
    /// </summary>
    public Action<MyEnum.HiderAndSeekerInputMode> OneHiderBeenAttackAction;
    public void CallOneHiderBeenAttackAction(HiderAndSeekerInputMode hiderMode)
    {
        OneHiderBeenAttackAction?.Invoke(hiderMode);
    }
    public Action OneHiderBeenHelpAction;
    public void CallOneHiderBeenHelpAction()
    {
        OneHiderBeenHelpAction?.Invoke();
    }
    public Action<int> PlayerGetMoneyAction;
    public void CallOnPlayerGetMoneyAction(int money)
    {
        PlayerGetMoneyAction?.Invoke(money);
    }

    public Action<int> OnCoinCountChangeAction;
    public void CallOnCoinCountChange(int money)
    {
        OnCoinCountChangeAction?.Invoke(money);
    }

    public Action<string> OnSelectedHiderChangeAction;
    public void CallOnSelectedHiderChangeAction(string selectedName)
    {
        OnSelectedHiderChangeAction?.Invoke(selectedName);
    }

    /// <summary>
    /// 当玩家获得Buff事件
    /// </summary>
    public Action<GuangGaoBuffBase> OnPlayerGetGuangGaoBuffAction;
    public void CallOnPlayerGetGuangGaoBuffAction(GuangGaoBuffBase buff)
    {
        OnPlayerGetGuangGaoBuffAction?.Invoke(buff);
    }

    /// <summary>
    /// 当选择角色时的事件
    /// </summary>
    public Action OnSelectedCharacterAction;
    public void CallOnSelectedCharacterAction()
    {
        OnSelectedCharacterAction?.Invoke();
    }
}