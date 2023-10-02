using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerThrowProp : MonoBehaviour
{
    public ThrowJianCe throwJianCe;
    /// <summary>
    /// 要扔向的玩家
    /// </summary>
    [HideInInspector]
    public CharacterBase throwToCharacterBase;

    /// <summary>
    /// 持有的人的CharacterBase
    /// </summary>
    public CharacterBase getCharacterBase;
    public void Init(CharacterBase characterBase)
    {
        getCharacterBase = characterBase;
        this.transform.localEulerAngles = Vector3.zero;
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            if(transform!=null)
            this.transform.localEulerAngles = Vector3.zero;
        }, 0.1f);
    }
    /// <summary>
    /// 投掷物模型物体
    /// </summary>
    public GameObject throwModleObject;


    public Vector3 roundSpeed = new Vector3(200, 200, 200);
    public Vector3 targetPos; // 要追踪的目标物体
    public Vector3 pos => transform.position;
    private float shuiPingSpeed;
    private float shuZhiJiaSpeed;
    private float shuZhiBeginSpeed;


    /// <summary>
    /// 距离和高度的系数
    /// </summary>
    public float wight_HightXishu = 0.5f;
    private float shuyZhiMaxGaoDu;
    public float ZhongLiXuShu = 2f;
    public float MinexHight = 1f;



    private Vector3 baginScale;

    public float waitTime;
    /// <summary>
    /// 水平移动方向
    /// </summary>
    private Vector3 shuiPingDir;

    private GameObject throwRengGuang;
    Coroutine coroutine;
    private void Awake()
    {
        baginScale = this.transform.localScale;
        shuZhiJiaSpeed = 9.85f * ZhongLiXuShu;
    }
    private void OnEnable()
    {
        isDestory = false;
        throwJianCe.gameObject.SetActive(true);
        waitTime = GameDefine.ThrowPropObjectMinexWaitTime;
    }
    private void Start()
    {


    }
    private void Update()
    {

        if (waitTime > 0) waitTime -= Time.deltaTime;
    }
    private void OnDisable()
    {
        throwToCharacterBase = null;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
    float moveTime;
    public void ThrowToCharcater(CharacterBase throwToCharacterBase)
    {

        if (!getCharacterBase.isAIInput)
        {
            AudioManager.Instance.PlayThrowTouZhiWu();
        }

        throwJianCe.gameObject.SetActive(false);
        this.throwToCharacterBase = throwToCharacterBase;
        coroutine = StartCoroutine(SimulateProjectile());
    }

    private IEnumerator SimulateProjectile()
    {
        

        throwRengGuang = EffectManager.Instance.CreateThrowRengGuang();
        throwRengGuang.transform.SetParent(this.transform,false);


        while (waitTime > 0)
        {
            yield return null;
        }
        this.transform.SetParent(GameingMainManager.Instance.gameMapCenter.transform);
        this.transform.localScale = baginScale;
        JiSuanLuJing();
        Vector3 beforeWeiZhi = throwToCharacterBase.transform.position; ;
        while (moveTime > 0)
        {




            moveTime -= Time.deltaTime;
            if (throwToCharacterBase.gameObject == null) yield break;
            this.transform.position += throwToCharacterBase.transform.position - beforeWeiZhi;
            beforeWeiZhi = throwToCharacterBase.transform.position;

            this.transform.position += shuiPingDir * Time.deltaTime * shuiPingSpeed +
                shuZhiBeginSpeed * Time.deltaTime * Vector3.up;
            shuZhiBeginSpeed -= Time.deltaTime * shuZhiJiaSpeed;



            //Debug.Log(Time.deltaTime + "    " + this.transform.position + "   " + shuZhiBeginSpeed);
            transform.Rotate(roundSpeed * Time.deltaTime);


            yield return null;
        }
        DestoryItSelf();
    }
    private void JiSuanLuJing()
    {
        targetPos = throwToCharacterBase.transform.position;
        Vector3 thisPos = transform.position;
        shuiPingDir = new Vector3(targetPos.x - thisPos.x, 0, targetPos.z - thisPos.z);
        float xLength = shuiPingDir.magnitude;
        shuiPingDir = shuiPingDir.normalized;

        shuyZhiMaxGaoDu = xLength * wight_HightXishu;
        shuyZhiMaxGaoDu = Mathf.Max(MinexHight, shuyZhiMaxGaoDu);
        shuZhiBeginSpeed = Mathf.Pow(2f * shuyZhiMaxGaoDu * shuZhiJiaSpeed, 0.5f);

        moveTime = shuZhiBeginSpeed / shuZhiJiaSpeed * 2;

        shuiPingSpeed = xLength / moveTime;
    }
    public bool isDestory = false;
    private void OnTriggerEnter(Collider other)
    {
        Chick(other);
    }
    public void Chick(Collider other)
    {
        if (isDestory) return;
        if (throwToCharacterBase is Seeker)
        {
            if (other.gameObject.tag != GameDefine.SeekerTag) return;
            Seeker seeker = other.GetComponent<Seeker>();
            seeker.playerBuffcontrol.AddOneBuff(new ZangMaterialBuff(seeker) as MaterialBuffBase);

            EffectManager.Instance.CreateOneJinBi(getCharacterBase.transform.position);

            if (!getCharacterBase.isAIInput)
            {
                getCharacterBase.GetMoney(GameDefine.ThrowToOtherGetMoney);

                AudioManager.Instance.PlayThrowZaZhong();
                InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
                {
                    AudioManager.Instance.PlayZangZhongXiao();
                }, 0.1f);
            }
            seeker.BeenRepel(getCharacterBase as Hider);
            DestoryItSelf();
        }
        if (throwToCharacterBase is Hider)
        {
            if (other.gameObject.tag != GameDefine.HiderTag) return;

            Hider hider = other.GetComponent<Hider>();
            if (hider.HasBeenAttack) return;
            hider.playerBuffcontrol.AddOneBuff
                (new ZangMaterialBuff(hider) as MaterialBuffBase);

            EffectManager.Instance.CreateOneJinBi(getCharacterBase.transform.position);

            if (!getCharacterBase.isAIInput)
            {
                getCharacterBase.GetMoney(GameDefine.ThrowToOtherGetMoney);

                AudioManager.Instance.PlayThrowZaZhong();

                InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
                {
                    AudioManager.Instance.PlayZangZhongXiao();
                }, 0.1f);

            }

            DestoryItSelf();
        }
    }

    public void DestoryItSelf()
    {
        isDestory = true;
        //设置特效：
        var go = EffectManager.Instance.CreateThrowJianShe();
        go.transform.position = this.transform.position;
        ObjectPoolManager.Instance.SetOneObjectInPool(go, 2f);
        //添加入对象池
        ObjectPoolManager.Instance.SetOneObjectInPool(this.gameObject);
        ObjectPoolManager.Instance.SetOneObjectInPool(throwRengGuang);

    }
}
