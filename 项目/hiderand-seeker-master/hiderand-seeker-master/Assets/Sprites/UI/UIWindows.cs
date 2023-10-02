using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnum;

public class UIWindows : MonoBehaviour
{

    public UIManager.UIElement uIElement;
    public Action OnYesAction;
    public Action OnNoAction;
    public Action OnCloseAction;
    /// <summary>
    /// 当不是第一次被初始化时,UIElement为null
    /// </summary>
    public virtual void InitUI(UIManager.UIElement uIElement = null)
    {
        if (uIElement != null) this.uIElement = uIElement;
        OnCloseAction += this.OnClose;
        OnYesAction += this.OnYesClose;
        OnNoAction += this.OnNoClose;
    }
    public virtual void OnEnd()
    {
        OnCloseAction -= this.OnClose;
        OnYesAction -= this.OnYesClose;
        OnNoAction -= this.OnNoClose;
    }

    protected virtual void OnClose()
    {

    }
    protected virtual void OnNoClose()
    {

    }
    protected virtual void OnYesClose()
    {

    }
    public void OnChickYes()
    {

        OnChickCloseUI(UICloseType.Yes);
    }
    public void OnChickNo()
    {

        OnChickCloseUI(UICloseType.No);
    }
    public void OnChickClose()
    {

        OnChickCloseUI(UICloseType.None);
    }
    public void OnChickCloseUI(UICloseType type = UICloseType.None)
    {
        AudioManager.Instance.PlayOnChickButton();

        UIManager.Instance.OnCloseUI(this.uIElement, type);

    }
}
