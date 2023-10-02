using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameFail : MonoBehaviour
{
    /// <summary>
    /// 复活Panel
    /// </summary>
    public GameObject fuhuoPanel;
    public GameObject NotFuhuoPanel;
    public UIGamePlaying gamePlaying;
    public void SetFirstFail()
    {
        fuhuoPanel.SetActive(true);
        NotFuhuoPanel.SetActive(false);
    }
    public void SetNotFirstFail()
    {
        fuhuoPanel.SetActive(false);
        NotFuhuoPanel.SetActive(true);
    }
    /// <summary>
    /// 重新开始Button
    /// </summary>
    public void OnChickReplayLevelButton()
    {
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            gamePlaying.OnChickClose();
        }, 0.3f);
        GameingMainManager.Instance.BeginOneNewGame();
        InterstitialAdManager.Instance.PlayAd(InterstitialAdEnum.I_settlement_Successful_Fail_Replay);
    }
    /// <summary>
    /// 跳关Button
    /// </summary>
    public void OnChickTiaoGuoLevelutton()
    {

        GameDataManager.Instance.levelManager.NowLevel++;
       
        if (GameingMainManager.Instance.player_Identity_Enum == MyEnum.Player_Identity_Enum.Hider)
            RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_JumpLevel_Hider, () =>
            {
                GameingMainManager.Instance.BeginOneNewGame();
                InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
                {
                    gamePlaying.OnChickClose();
                }, 0.3f);

            });
        else
        {
            RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_JumpLevel_Seeker, () =>
            {
                GameingMainManager.Instance.BeginOneNewGame();
                InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
                {
                    gamePlaying.OnChickClose();
                }, 0.3f);

            });
        }
      




    }

}
