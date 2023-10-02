using MyEnum;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameingMainManager : MonoSingletonManager<GameingMainManager>
{
    [HideInInspector]
    public Player_Identity_Enum player_Identity_Enum;
    [HideInInspector]
    public SceneNameEnum levelEnum;
    public GameingManagerBase gameingManager;
    InGameingTimeManager timeManager = null;
    public Material gameSceneWallMaterial;
    public Material gameSceneCityMaterial;

    public GameingMoneyManager gameingMoneyManager;



    public bool GameIsBegin => gameingManager != null
        && (gameingManager is GameingHideManager || gameingManager is GameingSeekManager);
    public GameMapCenter gameMapCenter;

    public bool oneGameIsEnd;
    public GameMapCenter GameMapCenter
    {
        get
        {
            if (this.gameMapCenter == null)
            {

                Debug.LogError("这个地图中没有设置世界中心！！！");


            }
            return gameMapCenter;
        }
        set
        {
           // Debug.Log("设置地图中心");
            this.gameMapCenter = value;
        }
    }

    /// <summary>
    /// 正在攻击中
    /// </summary>
    public bool attackIng { get; internal set; }

    public bool seekerCanAttack;


    /// <summary>
    /// 玩家通过看广告获得了速度Buff；
    /// </summary>
    public bool hasGetSpeedBuff = false;
    public void Init()
    {
        EventManager.Instance.OneGameEndAction += this.OnOneGameEnd;
        EventManager.Instance.OneGameFailAction += this.OnOneGameFail;
        EventManager.Instance.OneGameFuHuoAction += this.OnOneGameFuHuo;
        EventManager.Instance.OnSelectedCharacterAction += this.OnSelectedCharacter;
    }



    private void OnDisable()
    {
        EventManager.Instance.OneGameEndAction -= this.OnOneGameEnd;
        EventManager.Instance.OneGameFailAction -= this.OnOneGameFail;
        EventManager.Instance.OneGameFuHuoAction -= this.OnOneGameFuHuo;
        EventManager.Instance.OnSelectedCharacterAction -= this.OnSelectedCharacter;


    }



    /// <summary>
    /// 游戏是否已经复活过
    /// </summary>
    public bool hasFailFuHuo;

    private void OnOneGameFail()
    {
        
        hasFailFuHuo = true;
        gameingManager.OnOneGameFail();


    }
    private void OnOneGameFuHuo()
    {
        if(gameingManager!=null)
        gameingManager.OnOneGameFuHuo();
    }

    public void OnOneGameEnd(bool iswin)
    {
        oneGameIsEnd = true;
        if(gameingManager!=null)
        gameingManager.OnOneGameEnd(iswin);
        gameingManager = null;
        timeManager = null;
        gameingMoneyManager.OnGameEnd();
    }
    /// <summary>
    /// 进入游戏关卡但是玩家还没有选择角色
    /// </summary>
    public void BeginOneNewGame(Action BeforeShowUISelectedIdentityAction=null)
    {

       


        seekerCanAttack = true;
        hasFailFuHuo = false;
        oneGameIsEnd = false;
        GetLevelInformation();
        LoadGame(BeforeShowUISelectedIdentityAction);

    }
    //获取游戏信息
    private void GetLevelInformation()
    {
        //this.player_Identity_Enum = player_Identity_Enum;

        levelEnum = GameDataManager.Instance.levelManager.GetNowLevel();
    }
    private void OnSelectedCharacter()
    {
        if(gameingManager!=null)
        {
            gameingManager.DeleteAllCharacter();
            gameingManager = new GameingManagerBase();
            gameingManager.InitGame();
        }
    }
    private void LoadGame(Action BeforeShowUISelectedIdentityAction)
    {
        gameingManager = new GameingManagerBase();
        //SetGameSceneColor();
        //先进入游戏场景
       

        MySceneManager.Instance.ChangeScene(this.levelEnum, ()=>
        {
            gameingManager.InitGame();
            //AfterPlayerSelectedIdentity(MyEnum.Player_Identity_Enum.Hider);
            BeforeShowUISelectedIdentityAction?.Invoke();


            UIManager.Instance.Show<UISelectedIdentity>();

            

        });
    }
    public void AfterPlayerSelectedIdentity(MyEnum.Player_Identity_Enum player_Identity_Enum)
    {
        List<Hider> hiders = gameingManager.hiders;
        Seeker seeker = gameingManager.seeker;
        switch (player_Identity_Enum)
        {
            case Player_Identity_Enum.Seeker:
                gameingManager = new GameingSeekManager(hiders, seeker);
                break;
            case Player_Identity_Enum.Hider:
                gameingManager = new GameingHideManager(hiders, seeker);
                break;
            default:
                Debug.Log("GameingMainManager->AfterPlayerSelectedIdentity player_Identity_Enum出错");
                break;
        }
        this.player_Identity_Enum = player_Identity_Enum;
        gameingManager.InitGame();
        timeManager = new InGameingTimeManager();
        gameingMoneyManager = new GameingMoneyManager();

        if (hasGetSpeedBuff)
        {
            hasGetSpeedBuff = false;
            SpeedBuff buff;
            if (player_Identity_Enum == Player_Identity_Enum.Hider)
            {
                buff = new SpeedBuff(gameingManager.hiders[0]);
                gameingManager.hiders[0].playerBuffcontrol.
                    AddOneBuff((GuangGaoBuffBase)buff);
            }
            else
            {
                buff = new SpeedBuff(gameingManager.seeker);
                gameingManager.seeker.playerBuffcontrol.
                   AddOneBuff((GuangGaoBuffBase)buff);
            }
            EventManager.Instance.CallOnPlayerGetGuangGaoBuffAction(buff);
        }

        GameAnalyticsManager.Instance.SendGameBegin();

        TenjinManager.Instance.SendFirstInFirstLevel();
        
        TalkingDataManager.Instance.SendPlayerLevelUnSame();
        TalkingDataManager.Instance.SendPlayerLevelCanSame();

    }
    /// <summary>
    /// 设置游戏场景颜色
    /// </summary>
    private void SetGameSceneColor()
    {

        this.gameSceneWallMaterial.color = RangeOneColor();
        this.gameSceneCityMaterial.color = RangeOneColor();
    }
    private Color RangeOneColor()
    {
        // 使用Random.ColorHSV函数生成随机HSV颜色
        float hue = UnityEngine.Random.Range(0.5f, 1f);
        float saturation = UnityEngine.Random.Range(0.5f, 1f);
        float value = UnityEngine.Random.Range(0.5f, 1f);
        Color randomColor = Color.HSVToRGB(hue, saturation, value);

        // 设置颜色的不透明度为1，即完全不透明
        randomColor.a = 1f;

        return randomColor;
    }
    public void Update()
    {
        if (gameingManager != null)
        {
            gameingManager.UpData();
        }
        if(timeManager!=null)
        {
            timeManager.Updata();
        }

    }

    internal int GetHasBeenAttackHiderCount()
    {
        int count = 0;
        for(int i=0;i<gameingManager.hiders.Count;i++)
        {
            if (gameingManager.hiders[i].HasBeenAttack)
                count++;
        }
        return count;
    }
}