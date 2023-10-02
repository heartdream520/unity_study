using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectedPanel : MonoBehaviour
{
    public List<UICanSelected> uICanSelectedList;
    public List<UIPanelBase> uIPanelBaseList;

    public virtual void Start()
    {
        for(int i=0;i< uICanSelectedList.Count;i++)
        {
            uICanSelectedList[i].OnSelectedAction += this.OnuICanSelected;
        }
        uICanSelectedList[0].OnSelected();
    }

    private void OnuICanSelected(UICanSelected selected)
    {
        for(int i=0;i< uICanSelectedList.Count;i++)
        {
            if (uICanSelectedList[i]== selected)
            {
                SetPanel(i);
            }
        }
    }

    private int selectedPanelId = -1;
    private void SetPanel(int i)
    {
        if (i == selectedPanelId) return;
        if (selectedPanelId != -1)
        {
            uIPanelBaseList[selectedPanelId].OnPanelNotSelected();
            uICanSelectedList[selectedPanelId].OnUnSelectd();
        }
        selectedPanelId = i;
        uIPanelBaseList[selectedPanelId].OnPanelSelected();
    }
}