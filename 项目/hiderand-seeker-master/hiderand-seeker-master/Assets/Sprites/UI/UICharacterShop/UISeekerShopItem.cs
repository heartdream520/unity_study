using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISeekerShopItem : UICharacterShopItem
{
    public override void Init(Character_SoData characterData)
    {
        base.Init(characterData);
        isHider = false;
    }
    public override void SetSuo()
    {
        base.SetSuo();
        isLocked = !GameDataManager.
            Instance.charactersManager.SeekerJieSuoDic[characterSO_Data.name];
        lockImage.SetActive(isLocked);
    }
  
}
