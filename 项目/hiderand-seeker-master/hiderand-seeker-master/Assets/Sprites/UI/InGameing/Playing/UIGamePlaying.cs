using MyEnum;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlaying : UIWindows
{
    public Text beforeGameStartTimeText;
    public GameObject beforeGameStartTimePanel;
    public Text hasBeenAttackedHiderCountText;
    public Text hasGetMoneyText;
    private float nowBeforGameStartTime = -1;
   
    [HideInInspector]
    public MyEnum.Player_Identity_Enum player_Identity_Enum;


    public GameObject GameWinPanel;
    public GameObject GameFailPanel;
    [HideInInspector]
    public int hasGetMoneyNumber;
    [HideInInspector]
    public int nowHasBeenAttacked = 0;
    public Transform nowHasBeenAttackedPanel;
    private List<Toggle> hasBeenAttackedToggleList;
    public GameObject hasBeenAttackTogglePrefabs;
    public GameObject hasBeenAttackToggleAddedPrefabs;
    public GameingDaojiShi gameingDaojiShi;

    public UIGameingBuff uiGameingBuff;


    public List<GameObject> noPlayingUIObjectList;

    public override void InitUI(UIManager.UIElement uIElement = null)
    {
        base.InitUI(uIElement);


        nowBeforGameStartTime = -1;
        player_Identity_Enum = GameingMainManager.Instance.player_Identity_Enum;

        this.GameWinPanel.SetActive(false);
        this.GameFailPanel.SetActive(false);
        hasBeenAttackedHiderCountText.gameObject.SetActive(false);
        beforeGameStartTimePanel.gameObject.SetActive(true);
        gameingDaojiShi.gameObject.SetActive(false);

        hasGetMoneyNumber = 0;


        SetHasBeenAttackHiderPanel();
        SetHasBeenAttackedHiderCount(0);
        SetHasGetMoney(0);
        uiGameingBuff.gameObject.SetActive(false);

        EventManager.Instance.BeforeTimeBeginFlowAction += this.OnBeforeTimeBeginflow;
        EventManager.Instance.TimeOutGameBeginAction += this.OnTimeOutGameBeginAction;
        EventManager.Instance.OneHiderBeenAttackAction += this.OnOneHiderBeenAttack;
        EventManager.Instance.OneHiderBeenHelpAction += this.OnOneHiderBeenHelp;


        EventManager.Instance.OneGameEndAction += this.OnOneGameEnd;
        EventManager.Instance.PlayerGetMoneyAction += this.OnPlayerGetMoney;

        EventManager.Instance.BeforGameBeginTimeChangeAction += this.SetBeforeGameStartTimeText;

        EventManager.Instance.OneGameFailAction += OnOneGameFail;


        EventManager.Instance.OnPlayerGetGuangGaoBuffAction += OnPlayerGetGuangGaoBuff;

    }



    public override void OnEnd()
    {
        base.OnEnd();
        EventManager.Instance.BeforeTimeBeginFlowAction -= this.OnBeforeTimeBeginflow;
        EventManager.Instance.TimeOutGameBeginAction -= this.OnTimeOutGameBeginAction;
        EventManager.Instance.OneHiderBeenAttackAction -= this.OnOneHiderBeenAttack;
        EventManager.Instance.OneHiderBeenHelpAction -= this.OnOneHiderBeenHelp;

        EventManager.Instance.OneGameEndAction -= this.OnOneGameEnd;
        EventManager.Instance.PlayerGetMoneyAction -= this.OnPlayerGetMoney;

        EventManager.Instance.BeforGameBeginTimeChangeAction -= this.SetBeforeGameStartTimeText;
        EventManager.Instance.OneGameFailAction -= OnOneGameFail;
        EventManager.Instance.OnPlayerGetGuangGaoBuffAction -= OnPlayerGetGuangGaoBuff;
    }
    /// <summary>
    ///  设置已经被攻击的hiderPanel
    /// </summary>
    private void SetHasBeenAttackHiderPanel()
    {
        nowHasBeenAttackedPanel.gameObject.SetActive(false);
        nowHasBeenAttacked = 0;
        hasBeenAttackedToggleList = new List<Toggle>();
        if (player_Identity_Enum == Player_Identity_Enum.Hider)
        {
            for (int i = 0; i < GameDefine.GameHiderCount; i++)
            {
                var go = GameObject.Instantiate(hasBeenAttackTogglePrefabs);
                hasBeenAttackedToggleList.Add(go.GetComponent<Toggle>());
                go.transform.SetParent(nowHasBeenAttackedPanel,false);
            }
        }
        else
        {
            for (int i = 0; i < GameDefine.GameHiderCount
                - GameDefine.GameAddAttackCount; i++)
            {
                var go = GameObject.Instantiate(hasBeenAttackTogglePrefabs);
                hasBeenAttackedToggleList.Add(go.GetComponent<Toggle>());
                go.transform.SetParent(nowHasBeenAttackedPanel, false);

            }
            for (int i = 0; i < GameDefine.GameAddAttackCount; i++)
            {
                var go = GameObject.Instantiate(hasBeenAttackToggleAddedPrefabs);
                hasBeenAttackedToggleList.Add(go.GetComponent<Toggle>());
                go.transform.SetParent(nowHasBeenAttackedPanel, false);

            }
        }
    }






    #region 设置游戏开始前倒计时时间
    private void OnBeforeTimeBeginflow()
    {
        this.beforeGameStartTimePanel.gameObject.SetActive(true);
    }
    private void OnTimeOutGameBeginAction()
    {
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            beforeGameStartTimePanel.gameObject.SetActive(false);
        }, 0.5f);

        gameingDaojiShi.gameObject.SetActive(true);
        gameingDaojiShi.BeginDaoJiShi();
        nowHasBeenAttackedPanel.gameObject.SetActive(true);

        //hasBeenAttackedHiderCountText.gameObject.SetActive(true);
    }

    public void SetBeforeGameStartTimeText(float time)
    {
        if (nowBeforGameStartTime == -1)
        {
            nowBeforGameStartTime = time;
        }
        else
        {
            if (nowBeforGameStartTime - time >= 1f)
            {
                nowBeforGameStartTime = Mathf.RoundToInt(time);
            }
        }
        beforeGameStartTimeText.text = $"{Mathf.RoundToInt(nowBeforGameStartTime)}";
    }
    #endregion

    #region 角色被攻击
    private void OnOneHiderBeenAttack(HiderAndSeekerInputMode mode)
    {
        SetHasBeenAttackedHiderCount(++nowHasBeenAttacked);

    }
    private void OnOneHiderBeenHelp()
    {
        SetHasBeenAttackedHiderCount(--nowHasBeenAttacked);
    }

    public void SetHasBeenAttackedHiderCount(int x)
    {
        for (int i = 0; i < x; i++)
        {
            hasBeenAttackedToggleList[i].isOn = true;
        }
        for (int i = x; i < GameDefine.GameHiderCount; i++)
        {
            hasBeenAttackedToggleList[i].isOn = false;
        }
        //this.hasBeenAttackedHiderCountText.text = $"已经捉到的人数:{x}";
    }
    #endregion

    private void OnOneGameEnd(bool iswin)
    {
        string characterName;
        if (player_Identity_Enum == Player_Identity_Enum.Hider)
        {
            characterName = GameDataManager.Instance.charactersManager.SelectedHider_String;
        }
        else
        {
            characterName = GameDataManager.Instance.charactersManager.selectedSeeker_String;
        }
        if (iswin)
        {
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                OneGameEndCharacterModleManager.Instance.SetActiveModleWithStatus(
                   characterName, "win");
                this.GameWinPanel.SetActive(true);

                TalkingDataManager.Instance.SendLevelWin();

                InterstitialAdManager.Instance.PlayAd(InterstitialAdEnum.I_Enter_settlement_Successful);

            }, 2f);
        }
        else
        {
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                OneGameEndCharacterModleManager.Instance.SetActiveModleWithStatus(
                  characterName, "fail");

                TalkingDataManager.Instance.SendLevelFail();

                if (GameFailPanel != null)
                    this.GameFailPanel.GetComponent<UIGameFail>().SetNotFirstFail();
            }, 2f);
        }
        

    }

    private void OnOneGameFail()
    {
        string characterName;
        if (player_Identity_Enum == Player_Identity_Enum.Hider)
        {
            characterName = GameDataManager.Instance.charactersManager.SelectedHider_String;
        }
        else
        {
            characterName = GameDataManager.Instance.charactersManager.selectedSeeker_String;
        }
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            OneGameEndCharacterModleManager.Instance.SetActiveModleWithStatus(
              characterName, "fail");
            this.GameFailPanel.SetActive(true);
            this.GameFailPanel.GetComponent<UIGameFail>().SetFirstFail();

        }, 2f);
    }
    private void OnPlayerGetMoney(int money)
    {
        hasGetMoneyNumber += money;
        SetHasGetMoney(hasGetMoneyNumber);
    }

    public void SetHasGetMoney(int money)
    {
        this.hasGetMoneyText.text = money.ToString();
    }


    private void OnPlayerGetGuangGaoBuff(GuangGaoBuffBase buffBase)
    {
        uiGameingBuff.gameObject.SetActive(true);
        uiGameingBuff.SetBuff(buffBase);
    }

    public void OnChickSetGameingButton()
    {
        UIManager.Instance.Show<UISettingGameing>();
    }
    private void Update()
    {
    }

}
