using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine
{
    public const int GameHiderCount = 5;
    public const int GameSeekerCount = 1;

    /// <summary>
    /// ����Ĳ�׽��
    /// </summary>
    public static int GameAddAttackCount = 2;

    public const int PlayerMass = 10000;
    public const float BeforeGameStartTime = 5f;
    public const float NavMeshAgent_Radius = 0.7f;

    public const float SeekerLookForHiderMaxDis = 5f;



    public const float HiderPlayerInputBaseSpeed = 5f;
    public const float HiderAIInputBaseSpeed = 4f;
    /// <summary>
    /// ת���ٶ�
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

    //��ӡBuff�ĳ���ʱ��
    public static float FootPrintfBuffLastTime = 5f;
    //��ӡ��ʧ��ʱ��
    public static float FootDisPlayTime = 2.3f;
    //�ٶ�Buff�ĳ���ʱ��
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
    /// ���ϵ������������
    /// </summary>
    public static int NiCount = 5;

    /// <summary>
    /// ���������ȴʱ��
    /// </summary>
    public static float ChuanSongLengQueTime = 2f;

    /// <summary>
    /// seeker�ϵ�������
    /// </summary>
    public static string SeekerCageNameString = "Seeker�ϵ�����";

    /// <summary>
    /// �ܲ���Ч�ĳ���ʱ��
    /// </summary>
    public static float PaoBuEffectLastTime = 0.8f;
    public static float YouYongEffectLastTime = 0.8f;

    public static int PlayerMess = 10000;

    public static string WaterTag = "Water";

    public static int NotIsLevelSceneCnt = 2;


    /// <summary>
    /// ��͸�����ʺ�׺
    /// </summary>
    public static string MaterialBanTouMingHouZhui = "_0.5";
    /// <summary>
    /// ȫ͸�����ʺ�׺
    /// </summary>
    public static string MaterialQuanTouMingHouZhui = "_0";
    public static string MaterialX_RayHouZhui = "_XRay";
    public static string MaterialZangHouZhui = "_Zang";

    public static string HiderResourcesPath = "Characters/Hiders/";
    public static string SeekerResourcesPath = "Characters/Seekers/";

    /// <summary>
    /// �ײ���Ӱ������
    /// </summary>
    public static string YinYingNameString = "Plane";

    public static string AnimatorThrowLayerName = "Ͷ����";

    /// <summary>
    /// �����ߵ����ɼ��
    /// </summary>
    public static float AdvertisingPropsReviceTime =5f;
    /// <summary>
    /// Ͷ��������ɼ��
    /// </summary>
    public static float ThrowObjectReviveTime = 5f;

    /// <summary>
    /// ���Ͷ��������λ������
    /// </summary>
    public static string PlayerThrowObjectPosNameString = "Ͷ����";

    public static float ZangMaterialBuffLastTime = 5f;

    public static int ThrowToOtherGetMoney = 50;

    /// <summary>
    /// ��Ͷ�����೤ʱ�����Ͷ��
    /// </summary>
    public static float ThrowPropObjectMinexWaitTime = 0.5f;

    /// <summary>
    /// һ����Ϸ��ʱ��
    /// </summary>
    public static float OneGameTime = 30f;

    public static float HelpOtherDaojishiTime = 3f;

    /// <summary>
    /// һ����Ϸ��ʤ����һ����ɫ�Ľ���ֵ���Ӷ���
    /// </summary>
    public static float WinOneGameJieSuoCharacterJinDu = 0.2f;

  

    public static float MusicVolume = 0.3f;

   


    /// <summary>
    /// ������������ӳ��ֵ�ʱ��
    /// </summary>
    public static float PlayerCagePlayTime = 0.986f;
    /// <summary>
    /// Seeker���˾���
    /// </summary>
    public static float SeekerRepelDis = 2f;
    /// <summary>
    /// Seeker׷������
    /// </summary>
    public static float SeekerZhuiJiDistance = 3.5f;
    /// <summary>
    /// Seeker׷��ʱ��
    /// </summary>
    public static float SeekerZhuiJiTime = 1.02f;

    public static float InterstitialAdCD = 40f;

    public static float AdvertisingPropsBuffJinDuTime = 1.5f;
}