using HiderStatus;
using MyEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Hider : CharacterBase
{

    #region 变量
    public HiderStatusControl statusControl;
    public HiderAIInput aiInput;
    public HiderPlayerInput playerInput;
    public HiderThrowLayerStatusControl throwLayerStatusControl;
    public bool HasBeenAttack
    {
        get
        {
            return this.gameObject.layer == LayerMask.NameToLayer(GameDefine.CanNotAttack_Layer);
        }
    }
    /// <summary>
    /// 笼子预制体
    /// </summary>
    public GameObject playerCageGameObject;
    public GameObject longZiObject;

    #endregion



    public override void Awake()
    {
        base.Awake();
        playerCageGameObject = Resloader.LoadGameObject(PathDefine.PlayerCageObjectPath);
    }
    public override void Update()
    {
        base.Update();
        if (TestManager.Instance.testMode)
        {
            if (isAIInput)
            {
                NowSpeed = NowSpeed / baseRunSpeed * TestManager.Instance.HiderAIInputBaseSpeed;

                baseRunSpeed = TestManager.Instance.HiderAIInputBaseSpeed;
            }
            else
            {
                NowSpeed = NowSpeed / baseRunSpeed * TestManager.Instance.HiderPlayerInputBaseSpeed;

                baseRunSpeed = TestManager.Instance.HiderPlayerInputBaseSpeed;
                Debug.Log(NowSpeed);

            }
        }
        if (!isAfterInit) return;
        statusControl.Update();
        throwLayerStatusControl.Update();
        if (isAIInput) aiInput.UpData();
        else playerInput.UpData();
        if (gameisEnd) return;
        playerBuffcontrol.updata();

       

    }
    public override void OnOneGameEnd(bool iswin)
    {
        base.OnOneGameEnd(iswin);
        gameisEnd = true;
        //不能进行移动
        CanNotMove();

        if (isAIInput)
        {
            aiInput.OnOneGameEnd();
        }
        else
        {
            playerInput.OnOneGameEnd();
            if (iswin)
            {
                var x = EffectManager.Instance.CreateCaiHua();
                x.transform.position = transform.position;

                statusControl.SetStatus(HiderStatusEnum.Win);

            }
            else
            {
                statusControl.SetStatus(HiderStatusEnum.Fail);
            }

        }
        statusControl.OnOneGameEnd();
        throwLayerStatusControl.OnOneGameEnd();


    }
    #region 受到攻击后
    internal void AfterGetAttack()
    {
        if (isAIInput)
            EventManager.Instance.CallOneHiderBeenAttackAction(MyEnum.HiderAndSeekerInputMode.AIInput);
        else
        {
            EventManager.Instance.CallOneHiderBeenAttackAction(MyEnum.HiderAndSeekerInputMode.PlayerInput);
        }
        statusControl.SetStatus(MyEnum.HiderStatusEnum.BeenAttack);
    }
    public void GetHelp()
    {
        statusControl.SetStatus(MyEnum.HiderStatusEnum.Idle);
        EventManager.Instance.CallOneHiderBeenHelpAction();
    }

    public void CanNotMove()
    {
        isCanMove = false;
        this.InputXY = Vector2.zero;
    }
    #endregion

    #region 初始化人物
    public override void InitCharacter(string name, HiderAndSeekerInputMode mode)
    {
        base.InitCharacter(name, mode);

        nowRunSpeed = baseRunSpeed;
        CanMove();
        isAfterInit = true;
        gameisEnd = false;
        materialPath = GameDefine.HiderResourcesPath+resourcesNameString+ "/Materials/";
    }
    protected override void SetTag()
    {
        base.SetTag();
        this.tag = GameDefine.HiderTag;
    }

    protected override void SetStatusControl()
    {
        base.SetStatusControl();
        statusControl = new HiderStatusControl(this);
        throwLayerStatusControl = new HiderThrowLayerStatusControl(this);
    }
    public override void SetModeAIInput()
    {
        base.SetModeAIInput();
        this.rigidbody__.isKinematic = true;
        this.baseRunSpeed = GameDefine.HiderAIInputBaseSpeed;
        this.rotationSpeed = GameDefine.HiderAIInputRoundSpeed;
        aiInput = new HiderAIInput(this);

        
    }

    public override void SetModePlayerInput()
    {
        base.SetModePlayerInput();

        this.baseRunSpeed = GameDefine.HiderPlayerInputBaseSpeed;
        this.rotationSpeed = GameDefine.HiderPlayerInputRoundSpeed;
        playerInput = new HiderPlayerInput(this);

        
    }
    #endregion

    #region 碰撞回调
    private void OnCollisionEnter(Collision collision)
    {
        if (!isAfterInit) return;
        if (isAIInput)
            aiInput.OnCollisionEnter(collision);
        else playerInput.OnCollisionEnter(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!isAfterInit) return;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!isAfterInit) return;
        if (isAIInput)
            aiInput.OnCollisionExit(collision);
        else playerInput.OnCollisionExit(collision);
    }


    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (statusControl != null)
        {
            statusControl.OnTriggerEnter(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (statusControl != null)
        {
            statusControl.OnTriggerStay(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (statusControl != null)
        {
            statusControl.OnTriggerExit(other);
        }
    }

    public void SetMaterQuanTouMing()
    {
        if (HasBeenAttack) return;
        MaterialsManager.Instance.ChangeObjectMaterial(gameObject, material_QuanTouMing);
       // MaterialsManager.Instance.ChangeObjectMaterial(gameObject, material_Base);
        if (yinYingObject != null) yinYingObject.SetActive(false);

        isZang = false;
    }

    internal void OnOneGameFail()
    {
        
        CanNotMove();
        GameingMainManager.Instance.oneGameIsEnd = true;
        if (!isAIInput)
            statusControl.SetStatus(HiderStatusEnum.Fail);

        else
        {
            if (!HasBeenAttack)
                statusControl.SetStatus(HiderStatusEnum.AIFail);
        }
        if (GameingMainManager.Instance.player_Identity_Enum == MyEnum.Player_Identity_Enum.Hider)
            GameingMainManager.Instance.seekerCanAttack=false;
    }
    public void OnOneGameFuHuo()
    {
        CanMove();

        if (!(isAIInput && HasBeenAttack))
            statusControl.SetStatus(HiderStatusEnum.Idle);
        animator.CrossFade("idle", 0.1f);
        if (GameingMainManager.Instance.player_Identity_Enum ==
            MyEnum.Player_Identity_Enum.Hider)
            SetMaterBase();
        canRoundToMainCamera = false;
        if (!isAIInput)
        {
            WuDiBuff wuDiBuff = new WuDiBuff(this);
            playerBuffcontrol.AddOneBuff(wuDiBuff as GuangGaoBuffBase);
            EventManager.Instance.CallOneHiderBeenHelpAction();

            var cry = MyTools.BfsGetObjectPosNameGameObject(gameObject, "Cry");
            cry.SetActive(false);
            EventManager.Instance.CallOnPlayerGetGuangGaoBuffAction(wuDiBuff);

            if(longZiObject!=null)
            {
                ObjectPoolManager.Instance.SetOneObjectInPool(longZiObject);
                longZiObject = null;
            }
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                GameingMainManager.Instance.seekerCanAttack = true;
            }, 1f);
            
        }

        GameingMainManager.Instance.oneGameIsEnd = false;
    }
}
