using MyEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GameingHideManager : GameingManagerBase
{
    public int hasBeenAttackHiderCount;

    


    public GameingHideManager(List<Hider> hiders,Seeker seeker)
    {
        GameMapCenter = GameingMainManager.Instance.GameMapCenter;
        cameraControl =
            GameObject.FindGameObjectWithTag(GameDefine.MainCameraTag).
            AddComponent<CameraControl>();
        this.hiders = hiders;
       this. seeker = seeker;
        hasBeenAttackHiderCount = 0;

        InitEvent();
        //money = 0;


    }

    private void InitEvent()
    {
        EventManager.Instance.OneHiderBeenAttackAction += this.OnOneHiderBeenAttack;
        EventManager.Instance.OneHiderBeenHelpAction += this.OnOneHiderBeenHelp;

    }
    public override void OnOneGameEnd(bool iswin)
    {
        base.OnOneGameEnd(iswin);
        EventManager.Instance.OneHiderBeenAttackAction -= this.OnOneHiderBeenAttack;
        EventManager.Instance.OneHiderBeenHelpAction -= this.OnOneHiderBeenHelp;


    }



    /// <summary>
    /// 当一个角色被攻击之后的行为；
    /// </summary>
    /// <param name="mode"></param>
    private void OnOneHiderBeenAttack(HiderAndSeekerInputMode mode)
    {
        hasBeenAttackHiderCount++;
        if (mode == MyEnum.HiderAndSeekerInputMode.PlayerInput)
        {
            //EventManager.Instance.CallOneGameEndAction(false);
            EventManager.Instance.CallOneGameFailAction();
        }
    }
    private void OnOneHiderBeenHelp()
    {
        hasBeenAttackHiderCount--;
    }
    /// <summary>
    /// 初始化人物
    /// </summary>
    public override void InitGame()
    {
        //base.InitGame();
        cameraControl.InitCameraAfterSelectedIdentity(MyEnum.Player_Identity_Enum.Hider, this.hiders[0].gameObject);
        seeker.InitCharacter("Seeker_AIInput", MyEnum.HiderAndSeekerInputMode.AIInput);

        hiders[0].InitCharacter("Hider_PlayerInput", MyEnum.HiderAndSeekerInputMode.PlayerInput);

        for (int i = 1; i < GameDefine.GameHiderCount; i++)
        {
            hiders[i].InitCharacter("Hider_AIInput_" + (i).ToString(), MyEnum.HiderAndSeekerInputMode.AIInput);
        }
        //var go= CreateOne_HiderWith_PlayerInput();
        //CreateOne_SeekerWith_AIInput();
        //cameraControl.InitCamera(MyEnum.Player_Identity_Enum.Hider, go);
        // this.cameraControl.SetMode(MyEnum.Player_Identity_Enum.Hider);
        //this.cameraControl.SetPlayerGameObject(go);
    }



    public override void UpData()
    {
        base.UpData();
    }
}
