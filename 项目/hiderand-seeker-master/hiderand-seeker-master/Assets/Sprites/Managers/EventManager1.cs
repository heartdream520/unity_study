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
    /// ����ʱ��������Ϸ��ʼ
    /// </summary>
    public Action TimeOutGameBeginAction;
    public void CallTimeOutGameBeginAction()
    {
        TimeOutGameBeginAction?.Invoke();
    }
    /// <summary>
    /// ��Ϸ�����¼�
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
    /// Hider ������ʱ���¼�
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
    /// ����һ��Buff�¼�
    /// </summary>
    public Action<GuangGaoBuffBase> OnPlayerGetGuangGaoBuffAction;
    public void CallOnPlayerGetGuangGaoBuffAction(GuangGaoBuffBase buff)
    {
        OnPlayerGetGuangGaoBuffAction?.Invoke(buff);
    }

    /// <summary>
    /// ��ѡ���ɫʱ���¼�
    /// </summary>
    public Action OnSelectedCharacterAction;
    public void CallOnSelectedCharacterAction()
    {
        OnSelectedCharacterAction?.Invoke();
    }
}