using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine
{
    public const int GameHiderCount = 5;
    public const int GameSeekerCount = 1;

    /// <summary>
    /// 额外的捕捉数
    /// </summary>
    public static int GameAddAttackCount = 2;

    public const int PlayerMass = 10000;
    public const float BeforeGameStartTime = 5f;
    public const float NavMeshAgent_Radius = 0.7f;

    public const float SeekerLookForHiderMaxDis = 5f;



    public const float HiderPlayerInputBaseSpeed = 5f;
    public const float HiderAIInputBaseSpeed = 4f;
    /// <summary>
    /// 转弯速度
    /// </summary>
    public const float HiderAIInputRoundSpeed = 5f;
    public const float HiderPlayerInputRoundSpeed = 20f;

    public const float SeekerPlayerInputBaseSpeed = 5f;
    public const float SeekerAIInputBaseSpeed = 3f;
    public const float SeekerAIInputRoundSpeed = 5f;
    public const float SeekerPlayerInputRoundSpeed = 20f;

    public const float NavMeshAgentSpeed = 0.0001f;



    public const string Men_Layer = "Men";
    public const string NotCollisionWithPlayer_Layer = "NotCollisionWithPlayer";
    public const string CanNotAttack_Layer = "CanNotAttack";
    public const string Player_Layer = "Player";
    public const string Qiang_Layer = "Qiang";
    public const string Waiqiang_Layer = "Waiqiang";
    public const string Diban_Layer = "Diban";
    public static string PlayerWuDiLayer = "PlayerWuDi";


    public const string HiderTag = "Hider";
    public const string SeekerTag = "Seeker";
    public const string MainCameraTag = "MainCamera";
    public const string GameMapCenterTag = "GameMapCenter";
    public const string HiderCruisePointTag = "HiderCruisePoint";
    public const string DiBanTag = "diban";

    //脚印Buff的持续时间
    public static float FootPrintfBuffLastTime = 5f;
    //脚印消失的时间
    public static float FootDisPlayTime = 2.3f;
    //速度Buff的持续时间
    public static float SpeedBuffLastTime = 10f;
    public static float SpeedBuffUpLv = 1.5f;
    public static float GoThroughtWallsBuffLastTime = 10f;
    public static float WuDiBuffLastTime = 10f;
    public static float X_RayBuffLastTime = 10f;



    public static int Coin_01_Money = 10;
    public static int Coin_02_Money = 50;
    public static int Coin_03_Money = 50;
    public static int PlayerHelpOtherMonty = 50;

    /// <summary>
    /// 地上的泥的种类数量
    /// </summary>
    public static int NiCount = 5;

    /// <summary>
    /// 传送阵的冷却时间
    /// </summary>
    public static float ChuanSongLengQueTime = 2f;

    /// <summary>
    /// seeker上的笼子名
    /// </summary>
    public static string SeekerCageNameString = "Seeker上的笼子";

    /// <summary>
    /// 跑步特效的持续时间
    /// </summary>
    public static float PaoBuEffectLastTime = 0.8f;
    public static float YouYongEffectLastTime = 0.8f;

    public static int PlayerMess = 10000;

    public static string WaterTag = "Water";

    public static int NotIsLevelSceneCnt = 2;


    /// <summary>
    /// 半透明材质后缀
    /// </summary>
    public static string MaterialBanTouMingHouZhui = "_0.5";
    /// <summary>
    /// 全透明材质后缀
    /// </summary>
    public static string MaterialQuanTouMingHouZhui = "_0";
    public static string MaterialX_RayHouZhui = "_XRay";
    public static string MaterialZangHouZhui = "_Zang";

    public static string HiderResourcesPath = "Characters/Hiders/";
    public static string SeekerResourcesPath = "Characters/Seekers/";

    /// <summary>
    /// 底部阴影的名称
    /// </summary>
    public static string YinYingNameString = "Plane";

    public static string AnimatorThrowLayerName = "投掷物";

    /// <summary>
    /// 广告道具的生成间隔
    /// </summary>
    public static float AdvertisingPropsReviceTime =5f;
    /// <summary>
    /// 投掷物的生成间隔
    /// </summary>
    public static float ThrowObjectReviveTime = 5f;

    /// <summary>
    /// 玩家投掷物生成位置名称
    /// </summary>
    public static string PlayerThrowObjectPosNameString = "投掷物";

    public static float ZangMaterialBuffLastTime = 5f;

    public static int ThrowToOtherGetMoney = 50;

    /// <summary>
    /// 捡到投掷物后多长时间可以投出
    /// </summary>
    public static float ThrowPropObjectMinexWaitTime = 0.5f;

    /// <summary>
    /// 一场游戏的时间
    /// </summary>
    public static float OneGameTime = 30f;

    public static float HelpOtherDaojishiTime = 3f;

    /// <summary>
    /// 一场游戏获胜解锁一个角色的进度值增加多少
    /// </summary>
    public static float WinOneGameJieSuoCharacterJinDu = 0.2f;

  

    public static float MusicVolume = 0.3f;

   


    /// <summary>
    /// 攻击后玩家笼子出现的时间
    /// </summary>
    public static float PlayerCagePlayTime = 0.986f;
    /// <summary>
    /// Seeker击退距离
    /// </summary>
    public static float SeekerRepelDis = 2f;
    /// <summary>
    /// Seeker追击距离
    /// </summary>
    public static float SeekerZhuiJiDistance = 3.5f;
    /// <summary>
    /// Seeker追击时间
    /// </summary>
    public static float SeekerZhuiJiTime = 1.02f;

    public static float InterstitialAdCD = 40f;

    public static float AdvertisingPropsBuffJinDuTime = 1.5f;
}