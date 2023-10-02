using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class AdvertisingPropsBase : BuffObjectBase
{
    
    /// <summary>
    /// 代表是什么Buff的图片
    /// </summary>
    public GameObject imageObject;
    /// <summary>
    /// 图片上下移动的幅度
    /// </summary>
    public float imageMoveFudu=0.5f;
    /// <summary>
    /// 图片上下移动的速度
    /// </summary>
    public float imageMoveSpeed=1f;

    public GameObject jinduCanvas;
    public Image jinduImage;
    public Text jinduText;

    private float imageBeginPos_Y;
    private Vector3 imageObjectPos => imageObject.transform.position;
    private float imageObjectPos_Y => imageObject.transform.position.y;

    private bool hasBeenGet;

    float jinduTime;
    float JinduTime
    {
        get { return jinduTime; }
        set
        {
            jinduTime = value;
            jinduText.text = jinduTime.ToString("0.0");
            jinduImage.fillAmount = (GameDefine.AdvertisingPropsBuffJinDuTime - jinduTime) 
                / GameDefine.AdvertisingPropsBuffJinDuTime;
        }
    }
    public override void InitBuffObject()
    {
        
        base.InitBuffObject();
        JinduTime = GameDefine.AdvertisingPropsBuffJinDuTime;

    }
    private void OnEnable()
    {
        hasBeenGet = false;
    }
    public override void Start()
    {
        base.Start();
        jinduCanvas.SetActive(false);
        imageBeginPos_Y = imageObjectPos.y;
        StartCoroutine(UpdateImagePosCoroutine());

        JinduTime = GameDefine.AdvertisingPropsBuffJinDuTime;
    }
    public virtual void Update()
    {

    }
    private IEnumerator UpdateImagePosCoroutine()
    {
        yield break;
        /*
        while (true)
        {
            if (Mathf.Abs(imageBeginPos_Y - imageObjectPos_Y) >= this.imageMoveFudu)
            {
                imageMoveSpeed *= -1;
            }
            imageObject.transform.position = new Vector3(
                imageObjectPos.x,
                imageObjectPos_Y + imageMoveSpeed * Time.deltaTime,
                imageObjectPos.z);
            yield return new WaitForEndOfFrame();

        }
        */
    }
    public override void DoHiderBuff(Hider hider)
    {
        base.DoHiderBuff(hider);

    }

    public override void DoSeekerBuff(Seeker seeker)
    {
        base.DoSeekerBuff(seeker);
    }

    /// <summary>
    /// 销毁并在一段时间之后进行创建
    /// </summary>
    public void ReviceItself(float time=-1)
    {
        if (time == -1)
            ReviveObjectManager.Instance.ReviveOneObjectAfterSecend
                (this.gameObject, GameDefine.AdvertisingPropsReviceTime);
        else
        {
            ReviveObjectManager.Instance.ReviveOneObjectAfterSecend
                (this.gameObject, time);
        }
    }
    public virtual void DestoryItSelf()
    {

    }


    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(Check(other))
        {
            jinduCanvas.SetActive(true);
            JinduTime = GameDefine.AdvertisingPropsBuffJinDuTime;

            hasBeenGet = false;
        }
    }
    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (Check(other))
        {

            JinduTime -= Time.deltaTime;
            if (JinduTime <= 0 && !hasBeenGet)
            {
                hasBeenGet = true;
                SetBuff(other.gameObject);
                jinduCanvas.SetActive(false);
            }
        }

    }
    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (Check(other))
        {
            jinduCanvas.SetActive(false);
        }
    }


    private bool Check(Collider other)
    {
        CharacterBase characterBase;
        if (other.gameObject.TryGetComponent(out characterBase))
        {
            if (!characterBase.isAIInput)
                return true;
        }
        return false;
    }
}
