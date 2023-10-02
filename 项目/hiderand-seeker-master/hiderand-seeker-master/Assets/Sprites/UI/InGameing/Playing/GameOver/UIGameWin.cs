using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGameWin : MonoBehaviour
{
    public UIGamePlaying gamePlaying;
    public UIGameOverStarPanel gameoverStar;

    public UIGameOvereCharacterJinDuPanel characterJinDu;

    public UIGameOverCoinPanel coinPanel;

    public GameObject X5XCoinButton;

    private void Start()
    {
        gameoverStar.SetStarFrowXToY(0, 3);
        if (GameDataManager.Instance.charactersManager.GetOneCharacterJinDuString() != "")
        {
            characterJinDu.SetJinDu(GameDataManager.
                Instance.charactersManager.CharacterJinDu + GameDefine.WinOneGameJieSuoCharacterJinDu);

        }
        else characterJinDu.gameObject.SetActive(false);
        coinPanel.SetCoinToNum(GameingMainManager.Instance.gameingMoneyManager.money);
    }
    public void OnChickReplayLevelButton()
    {
        //AudioManager.Instance.PlayOnChickButton();

        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            gamePlaying.OnChickClose();
        }, 0.3f);
        InterstitialAdManager.Instance.PlayAd(InterstitialAdEnum.I_settlement_Successful_Win_Replay);
        GameingMainManager.Instance.BeginOneNewGame();

    }
    public void OnChickNextLevelutton()
    {
        // AudioManager.Instance.PlayOnChickButton();

        GameAnalyticsManager.Instance.SendGameNext();

        GameDataManager.Instance.levelManager.NowLevel++;
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            gamePlaying.OnChickClose();
        }, 0.3f);
        InterstitialAdManager.Instance.PlayAd(InterstitialAdEnum.I_settlement_Successful_Win_Next);
        GameingMainManager.Instance.BeginOneNewGame();
    }
    /// <summary>
    /// 当按下5XMoney按钮
    /// </summary>
    public void OnChick5XMoneyButton()
    {
        AudioManager.Instance.PlayOnChickButton();

        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Coin5XReward, () =>
        {
            X5XCoinButton.SetActive(false);
            coinPanel.SetCoinToNum
                (GameingMainManager.Instance.gameingMoneyManager.money * 5);
        });
    }

}
