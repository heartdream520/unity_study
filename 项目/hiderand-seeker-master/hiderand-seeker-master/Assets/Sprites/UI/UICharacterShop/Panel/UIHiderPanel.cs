using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHiderPanel : UICharacterPanel
{
    [HideInInspector]
    public Dictionary<string,UIHiderShopItem> itemList;
    [HideInInspector]
    public UIHiderShopItem nowSelectedItem;

    public UICharacterShop uICharacterShop;
    private void Awake()
    {
        characterSO_List = SO_DataManager.Instance.hiderSo_Data.characterList;
    }
    
    private void Start()
    {
        itemList = new Dictionary<string, UIHiderShopItem>();
        for (int i = 0; i < characterSO_List.Count; i++)
        {
            var go = GameObject.Instantiate(uiItemPrefabs);
            var x = go.GetComponent<UIHiderShopItem>();
            
            x.Init(characterSO_List[i]);
            itemList.Add(characterSO_List[i].name, x);
            x.OnSelectedAction += this.OnItemSelected;
            go.transform.SetParent(rootTransform, false);

            go.name = "HiderShopItem_" + characterSO_List[i].name;
        }
        itemList[GameDataManager.Instance.charactersManager.SelectedHider_String].
           OnSelected();
    }

    private void OnItemSelected(UICanSelected selected)
    {
        if (nowSelectedItem == selected)
        {
            return;
        }
        if (nowSelectedItem != null)
        {
            nowSelectedItem.OnUnSelectd();
        }
        nowSelectedItem = selected as UIHiderShopItem;

        uICharacterShop.SetNowCharacterShopItem(selected as UICharacterShopItem);

        CharacterModleManager.Instance.
            SetActiveModle((selected as UICharacterShopItem).
            characterSO_Data.name);
    }
    public override void OnPanelSelected()
    {
        base.OnPanelSelected();
        if (nowSelectedItem != null)
        {
            uICharacterShop.SetNowCharacterShopItem(nowSelectedItem);

            CharacterModleManager.Instance.
                SetActiveModle((nowSelectedItem).
                characterSO_Data.name);
        }
       
    }
}
