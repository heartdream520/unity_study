using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterShopItem : UICanSelected
{
    [HideInInspector]
    public bool isHider;
    public Image characterImage;
    public GameObject lockImage;
    [HideInInspector]
    public Character_SoData characterSO_Data;
    [HideInInspector]
    public bool isLocked;
    public virtual void Init(Character_SoData characterData )
    {
        this.characterSO_Data = characterData;
        characterImage.sprite = characterData.shopImageSprite;
        SetSuo();
    }
    public virtual void SetSuo()
    {

    }
    public void SetUnLock()
    {
        isLocked = false;
        lockImage.SetActive(false);
    }
}
