using MyEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameingSeekManager : GameingManagerBase
{
    public int hasBeenAttackHiderCount;
    public GameingSeekManager(List<Hider> hiders, Seeker seeker)
    {
        GameMapCenter = GameingMainManager.Instance.GameMapCenter;
        cameraControl =
            GameObject.FindGameObjectWithTag(GameDefine.MainCameraTag).
            AddComponent<CameraControl>();
        this.hiders = hiders;
        this.seeker = seeker;
        hasBeenAttackHiderCount = 0;

        InitEvent();
    }

    private void InitEvent()
    {
        EventManager.Instance.OneHiderBeenAttackAction += this.OnOneHiderBeenAttack;
        EventManager.Instance.OneHiderBeenHelpAction += this.OnOneHiderBeenHelp;
        EventManager.Instance.TimeOutGameBeginAction += OnTimeOutGameBegin;
    }



    public override void OnOneGameEnd(bool iswin)
    {
        base.OnOneGameEnd(iswin);
        EventManager.Instance.OneHiderBeenAttackAction -= this.OnOneHiderBeenAttack;
        EventManager.Instance.OneHiderBeenHelpAction -= this.OnOneHiderBeenHelp;
        EventManager.Instance.TimeOutGameBeginAction -= OnTimeOutGameBegin;

    }
    private void OnTimeOutGameBegin()
    {
        foreach (var x in this.hiders)
        {
            x.SetMaterQuanTouMing();
        }
    }
    private void OnOneHiderBeenAttack(HiderAndSeekerInputMode mode)
    {
        hasBeenAttackHiderCount++;
        if(hasBeenAttackHiderCount==GameDefine.GameHiderCount)
        {
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {

                Debug.LogWarning("GameingSeekManager->CallOneGameEndAction(true)");
                EventManager.Instance.CallOneGameEndAction(true);
            }, 0f);
           
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
        cameraControl.InitCameraAfterSelectedIdentity(MyEnum.Player_Identity_Enum.Seeker,seeker.gameObject);

        seeker.InitCharacter("Seeker_Player_Input", MyEnum.HiderAndSeekerInputMode.PlayerInput);

        for(int i=0;i<GameDefine.GameHiderCount;i++)
        {
            hiders[i].InitCharacter("Hider_AIInput_" + (i + 1).ToString(), MyEnum.HiderAndSeekerInputMode.AIInput);
        }

    }

    public override void UpData()
    {
        base.UpData();
    }


}
