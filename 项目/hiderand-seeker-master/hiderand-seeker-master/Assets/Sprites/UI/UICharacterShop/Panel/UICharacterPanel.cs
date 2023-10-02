using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterPanel : UIPanelBase
{
    public GameObject uiItemPrefabs;
    public Transform rootTransform;
    [HideInInspector]
    public List<Character_SoData> characterSO_List;
    public override void OnPanelNotSelected()
    {
        base.OnPanelNotSelected();

    }

    public override void OnPanelSelected()
    {
        base.OnPanelSelected();

    }
}
