using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterShopGongNeng : MonoBehaviour
{
    /// <summary>
    /// 花金币购买
    /// </summary>
    public Button coinGetButton;
    /// <summary>
    /// Ad购买
    /// </summary>
    public Button adGetButton;

    public Button selectedButton;
    public GameObject haveSelectedObject;
    /// <summary>
    /// 价钱Text
    /// </summary>
    public Text priceText;

    private CharactersManager charactersManager => GameDataManager.Instance.charactersManager;

    private void Awake()
    {
        SetNone();
    }
    public void SetLockItem(int price)
    {
        coinGetButton.gameObject.SetActive(true);
        adGetButton.gameObject.SetActive(true);
        selectedButton.gameObject.SetActive(false);
        //设置价钱
        if (price < 1000)
            priceText.text = price.ToString();
        else
        {
            if (price % 1000 != 0)
            {
                float p = ((float)price) / 1000f;
                priceText.text = (p).ToString("0.0") + "K";
            }
            else
                priceText.text = (price / 1000).ToString() + "K";
        }
        haveSelectedObject.SetActive(false);
    }
    public void SetUnLockItem()
    {
        coinGetButton.gameObject.SetActive(false);
        adGetButton.gameObject.SetActive(false);
        selectedButton.gameObject.SetActive(true);
        haveSelectedObject.SetActive(false);

    }

    public void SetNone()
    {
        coinGetButton.gameObject.SetActive(false);
        adGetButton.gameObject.SetActive(false);
        selectedButton.gameObject.SetActive(false);
        haveSelectedObject.SetActive(false);
    }
    public void SetHasSelectedObject()
    {
        coinGetButton.gameObject.SetActive(false);
        adGetButton.gameObject.SetActive(false);
        selectedButton.gameObject.SetActive(false);
        haveSelectedObject.SetActive(true);
    }
    internal void SetCharacterItemGongNeng(UICharacterShopItem item)
    {
        
        if (item.isHider && charactersManager.SelectedHider_String
            == item.characterSO_Data.name)
        {
            SetHasSelectedObject();
        }
        else if (!item.isHider && charactersManager.SelectedSeeker_String
            == item.characterSO_Data.name)
        {
            SetHasSelectedObject();
        }
        else 
        if (item.isLocked)
        {
            SetLockItem(item.characterSO_Data.price);
        }
        else SetUnLockItem();
    }
}
