using MainScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectedIdentity : UIWindows
{
    public Text levelText;
    public Button getSpeedBuffButton;

    public override void InitUI(UIManager.UIElement uIElement = null)
    {
        base.InitUI(uIElement);
        levelText.text = "Level  "+ GameDataManager.Instance.levelManager.NowLevel;

    }

    /// <summary>
    /// No ÎªÑ¡ÔñSeeker
    /// </summary>
    protected override void OnNoClose()
    {
        base.OnNoClose();
        GameingMainManager.Instance.AfterPlayerSelectedIdentity(MyEnum.Player_Identity_Enum.Seeker);
    }

    /// <summary>
    /// Yes Ñ¡ÔñHider
    /// </summary>
    protected override void OnYesClose()
    {
        base.OnYesClose();
        GameingMainManager.Instance.AfterPlayerSelectedIdentity(MyEnum.Player_Identity_Enum.Hider);
    }
    public void OnChickSpeedBuffSeekerButton()
    {
        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_BeforeGameStart_SpeedBuff, () =>
        {
            getSpeedBuffButton.gameObject.SetActive(false);
            GameingMainManager.Instance.hasGetSpeedBuff = true;

        });
       
    }
    public void OnChickBackButton()
    {
        AudioManager.Instance.PlayOnChickButton();

        UIMain.Instance.gameObject.SetActive(true);
    }

    public void OnChickCharacterShopButton()
    {
        AudioManager.Instance.PlayOnChickButton();
        UIManager.Instance.Show<UICharacterShop>();
    }


}
