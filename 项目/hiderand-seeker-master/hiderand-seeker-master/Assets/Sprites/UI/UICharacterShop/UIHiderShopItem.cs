using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHiderShopItem : UICharacterShopItem
{
    public override void Init(Character_SoData characterData)
    {
        base.Init(characterData);
        isHider = true;
    }
    public override void SetSuo()
    {
        
        base.SetSuo();
        isLocked = !GameDataManager.
            Instance.charactersManager.HiderJieSuoDic[characterSO_Data.name];
        lockImage.SetActive(isLocked);
    }
}
