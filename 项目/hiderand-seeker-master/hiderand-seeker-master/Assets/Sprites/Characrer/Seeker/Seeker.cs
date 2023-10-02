using HiderStatus;
using MyEnum;
using SeekerStatus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Seeker : CharacterBase
{
    #region 变量
    public SeekerStatusControl statusControl;
    public SeekerThrowLayerStatusControl throwLayerStatusControl;
    public SeekerAIInput aiInput;
    private SeekerPlayerinput playerInput;


    public GameObject tanZhaoDeng;

    #region 寻找Hider参数
    /// <summary>
    /// 检测hider精度
    /// </summary>
    public int lookAccurate = 10;
    /// <summary>
    /// 检测Hider的角度
    /// </summary>
    public float lookAngle = 60;
    /// <summary>
    /// 查找Hider的间隔时间
    /// </summary>
    public float LookForHiderSpaceTime = 0.2f;
    /// <summary>
    /// 查找Hider的最大距离
    /// </summary>
    public float LookForHiderMaxDis = GameDefine.SeekerLookForHiderMaxDis;

    #endregion

    #endregion
    
    public override void Awake()
    {
        base.Awake();
        //StartCoroutine

        tanZhaoDeng = GameObject.Instantiate(Resloader.LoadGameObject(
            PathDefine.TanZhaoDengPrefabsPath));
        tanZhaoDeng.gameObject.SetActive(false);
    }
    public GameObject BfsFindInChild(Transform transform, string name)
    {
        if (transform.Find(name) != null)
        {
            return transform.Find(name).gameObject;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = BfsFindInChild(transform.GetChild(i), name);
            if (x != null) return x;
        }
        return null;
    }
    public override void Update()
    {
        base.Update();
        
        if (!isAfterInit) return;
        statusControl.Update();
        throwLayerStatusControl.Update();
        if (isAIInput) aiInput.UpData();
        else playerInput.UpData();

        if (gameisEnd) return;
        playerBuffcontrol.updata();

        if (TestManager.Instance.testMode)
        {
            if (isAIInput)
            {
                NowSpeed = (NowSpeed) / baseRunSpeed * TestManager.Instance.SeekerAIInputBaseSpeed;
                baseRunSpeed = TestManager.Instance.SeekerAIInputBaseSpeed;
            }
            else
            {
                NowSpeed = (NowSpeed) / baseRunSpeed * TestManager.Instance.SeekerPlayerInputBaseSpeed;
                baseRunSpeed = TestManager.Instance.SeekerPlayerInputBaseSpeed;

            }
        }

    }
    public override void OnOneGameEnd(bool iswin)
    {
        base.OnOneGameEnd(iswin);
        gameisEnd = true;
        CanNotMove();

        if (isAIInput)
        {
            aiInput.OnOneGameEnd();
        }
        else
        {
            playerInput.OnOneGameEnd();
            if(iswin)
            {
               var x=  EffectManager.Instance.CreateCaiHua();
                x.transform.position = transform.position;
                statusControl.SetStatus(SeekerStatusEnum.Win);

            }
            else
            {
                statusControl.SetStatus(SeekerStatusEnum.Fail);
            }

        }
        statusControl.OnOneGameEnd();
        throwLayerStatusControl.OnOneGameEnd();
        EventManager.Instance.TimeOutGameBeginAction -= this.OnTimeOutGameBegin;
    }



    private void CanNotMove()
    {
        isCanMove = false;
        this.InputXY = Vector2.zero;
    }

    #region 初始化人物
    public override void InitCharacter(string name, HiderAndSeekerInputMode mode)
    {
        base.InitCharacter(name, mode);
        tanZhaoDeng.transform.SetParent(transform, false);
        nowRunSpeed = baseRunSpeed;
        isAfterInit = true;
        EventManager.Instance.TimeOutGameBeginAction += this.OnTimeOutGameBegin;
        CanNotMove();
        gameisEnd = false;

        //Seeker 倒计时UI
        /*
        var ui = UIManager.Instance.Show<WorldUISeekerDaoJiShi>();
        ui.transform.SetParent(this.transform,false);
        ui.transform.localRotation =Quaternion.identity;
        */

        materialPath = GameDefine.SeekerResourcesPath + resourcesNameString + "/Materials/";

    }
    private void OnTimeOutGameBegin()
    {
        CanMove();
    }
    protected override void SetTag()
    {
        base.SetTag();
        this.tag = GameDefine.SeekerTag;
    }
    protected override void SetStatusControl()
    {
        base.SetStatusControl();
        statusControl = new SeekerStatusControl(this);
        throwLayerStatusControl = new SeekerThrowLayerStatusControl(this);
    }
    public override void SetModeAIInput()
    {
        base.SetModeAIInput();
        this.rigidbody__.isKinematic = true;
        this.baseRunSpeed = GameDefine.SeekerAIInputBaseSpeed;
        this.rotationSpeed = GameDefine.SeekerAIInputRoundSpeed;
        aiInput = new SeekerAIInput(this);

        StartCoroutine(PlayWhoIsThereIEnumerator());
    }
    IEnumerator PlayWhoIsThereIEnumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(MyRandom.RangeFloat(5, 15));
            if(!GameingMainManager.Instance.oneGameIsEnd)
            {
                AudioManager.Instance.PlayWhoIsThere();
            }
        }
    }
    public override void SetModePlayerInput()
    {
        base.SetModePlayerInput();
        this.baseRunSpeed = GameDefine.SeekerPlayerInputBaseSpeed;
        this.rotationSpeed = GameDefine.SeekerPlayerInputRoundSpeed;
        playerInput = new SeekerPlayerinput(this);
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
    internal void OnOneGameFail()
    {
        if(GameingMainManager.Instance.player_Identity_Enum==Player_Identity_Enum.Seeker)
        {
            GameingMainManager.Instance.seekerCanAttack = false;
        }
        CanNotMove();
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            float waitTime = 0;
            if (statusControl.nowStatusEnum == SeekerStatusEnum.Attack)
            {
                waitTime = 1.5f;
            }
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                if (!isAIInput)
                    statusControl.SetStatus(SeekerStatusEnum.Fail);
                else statusControl.SetStatus(SeekerStatusEnum.AIFail);

            }, waitTime);

        }, 0.3f);
         GameingMainManager.Instance.oneGameIsEnd = true;
    }
    public void OnOneGameFuHuo()
    {
        //Debug.Log("Seeker=>OnOneGameFuHuo");
        if (GameingMainManager.Instance.player_Identity_Enum == Player_Identity_Enum.Seeker)
        {
            GameingMainManager.Instance.seekerCanAttack = true;
        }
        canRoundToMainCamera = false;
        if (GameingMainManager.Instance.player_Identity_Enum == MyEnum.Player_Identity_Enum.Seeker)
        {
            var effect = EffectManager.Instance.CreateX_RaySaoMiao();
            effect.transform.SetParent(transform, false);
            ObjectPoolManager.Instance.SetOneObjectInPool(effect, 1f);
            X_RayBuff x_RayBuff = new X_RayBuff(this);
            playerBuffcontrol.AddOneBuff(x_RayBuff as GuangGaoBuffBase);
            EventManager.Instance.CallOnPlayerGetGuangGaoBuffAction(x_RayBuff );

            AudioManager.Instance.PlayX_Ray_Sound();
        }
        CanMove();
       
        statusControl.SetStatus(SeekerStatusEnum.Idle);
        animator.CrossFade("idle", 0.1f);

        GameingMainManager.Instance.oneGameIsEnd = false;


    }
    Coroutine faceToGameObjectcoroutine;
    internal void FaceToGameObject(GameObject gameObject)
    {
        if(faceToGameObjectcoroutine!=null)
        {
            StopCoroutine(faceToGameObjectcoroutine);
            faceToGameObjectcoroutine = null;
        }
        faceToGameObjectcoroutine= StartCoroutine(FaceToGameObjectIEnumerator(gameObject));

    }
    IEnumerator FaceToGameObjectIEnumerator(GameObject OthergameObject)
    {
        Vector3 targetDir = 
            OthergameObject.transform.position - transform.position;
        targetDir.y = 0;
        Quaternion quaternion = Quaternion.LookRotation
            (targetDir);

        Vector3 thisPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 otherPos = new Vector3(OthergameObject.transform.position.x
            , 0, OthergameObject.transform.position.z);

        float dis = Vector3.Distance(thisPos, otherPos);
        bool canZhuiJi=false;
        Vector3 vector = (otherPos - thisPos).normalized;
        
        Vector3 zhuiJiPos = thisPos;

        //float SeekerZhuiJiDistance = GameDefine.SeekerZhuiJiDistance;
        float SeekerZhuiJiDistance = GameDefine.SeekerZhuiJiDistance;

      //  Debug.Log("Dis:" + dis + "  ThisPos:" + thisPos + "OtherPos" + otherPos);

        Vector3 speedVector= Vector3.zero;
        if (dis >= SeekerZhuiJiDistance)
        {
            canZhuiJi = true;
            this.paobuTuoWei.SetActive(true);
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                this.paobuTuoWei.SetActive(false);
            }, GameDefine.SeekerZhuiJiTime);


            dis -= SeekerZhuiJiDistance;
            zhuiJiPos = thisPos + vector * dis;
            zhuiJiPos.y = transform.position.y;
            speedVector = dis / GameDefine.SeekerZhuiJiTime * vector;
        }

        

        float time = 0;
        while (time < GameDefine.SeekerZhuiJiTime)
        {
            time += Time.deltaTime;
            float p = time > GameDefine.SeekerZhuiJiTime
                ? time - GameDefine.SeekerZhuiJiTime : 0;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                quaternion, rotationSpeed * (Time.deltaTime - p));


            if (canZhuiJi)
            {
               // transform.position = Vector3.MoveTowards(transform.position,
               //   zhuiJiPos, dis * (Time.deltaTime - p));
                 transform.position += speedVector * (Time.deltaTime - p);
            }

            yield return null;
        }
        thisPos = new Vector3(transform.position.x, 0, transform.position.z);
        otherPos = new Vector3(OthergameObject.transform.position.x
            , 0, OthergameObject.transform.position.z);
       // Debug.Log("追击后距离：" + Vector3.Distance(thisPos, otherPos));
    }
    public void BeenRepel(Hider hider)
    {
        StartCoroutine(RepelSeekerIEnumerator(hider));
    }
    IEnumerator RepelSeekerIEnumerator(Hider hider)
    {
        float repelTime = 0.5f;
        float repelDis = GameDefine.SeekerRepelDis;
        Vector3 vector = (hider.transform.position
            - this.transform.position).normalized;
        Vector3 targetPos = this.transform.position - vector * repelDis;
        while (repelTime > 0)
        {
            repelTime -= Time.deltaTime;
            //Debug.Log("RepelSeekerIEnumerator");
            this.transform.position =
                Vector3.Lerp(this.transform.position,
                targetPos, Time.deltaTime / repelTime);
            yield return null;
        }
    }
}