
using UnityEngine;

public class UICharacterShop : UIWindows
{
    [HideInInspector]
    public UICharacterShopItem nowCharacterShopItem;
    public UICharacterShopGongNeng uICharacterShopGongNeng;
    public void SetNowCharacterShopItem(UICharacterShopItem item)
    {
        //Debug.Log("选择" + item.characterSO_Data.name);
        nowCharacterShopItem = item;
        uICharacterShopGongNeng.SetCharacterItemGongNeng(item);
        
    }
    public void OnChickCoinGetButton()
    {
        AudioManager.Instance.PlayOnChickButton();

        if (GameDataManager.Instance.moneyManager.Money
            >= nowCharacterShopItem.characterSO_Data.price)
        {
            GameDataManager.Instance.moneyManager.Money
                -= nowCharacterShopItem.characterSO_Data.price;
            UnLockOneCharacter();
        }
    }
    /// <summary>
    /// 看广告获取
    /// </summary>
    public void OnChickAdGetButton()
    {
        AudioManager.Instance.PlayOnChickButton();

        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Unlock_Skins_Shop, () =>
        {
            UnLockOneCharacter();
        });
    }
    public void OnChickSelectedButton()
    {
        AudioManager.Instance.PlayOnChickButton();

        if (nowCharacterShopItem.isHider)
        {
            GameDataManager.Instance.charactersManager.SelectedHider_String =
                nowCharacterShopItem.characterSO_Data.name;
        }
        else
        {
            GameDataManager.Instance.charactersManager.SelectedSeeker_String =
               nowCharacterShopItem.characterSO_Data.name;
        }
        uICharacterShopGongNeng.SetHasSelectedObject();

        EventManager.Instance.CallOnSelectedCharacterAction();
    }
    private void UnLockOneCharacter()
    {
        nowCharacterShopItem.SetUnLock();
        uICharacterShopGongNeng.SetUnLockItem();
        if (nowCharacterShopItem.isHider)
        {
            GameDataManager.Instance.charactersManager.
                HiderJieSuoDic[nowCharacterShopItem.characterSO_Data.name] = true;
        }
        else
        {
            GameDataManager.Instance.charactersManager.
                SeekerJieSuoDic[nowCharacterShopItem.characterSO_Data.name] = true;
        }


    }



}
