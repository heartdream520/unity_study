using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterBase : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rigidbody__;
    public PlayerBuffControl playerBuffcontrol;
    public bool isAIInput;
    /// <summary>
    /// ��ʼ�ܲ�ʱ����β
    /// </summary>
    public GameObject paobuTuoWei;
    /// <summary>
    /// ��Ӿʱ����β
    /// </summary>
    public GameObject youyongTuoWei;

    //��ǰ�ܲ��ٶ�
    protected float nowRunSpeed;
    //�����ܲ��ٶ�
    public float baseRunSpeed = 5f;
    //ת���ٶ�
    public float rotationSpeed = 20f;

    public bool canRoundToMainCamera;
    public float NowSpeed
    {
        get
        {
            return nowRunSpeed;
        }
        set
        {
            nowRunSpeed = value;
        }
    }

    public Vector3 faceDir => this.transform.forward;
    //�����XY����
    public Vector2 inputXY = Vector2.zero;
    public Vector2 InputXY
    {
        get
        {
            if (!isCanMove)
                return Vector2.zero;
            return inputXY;
        }
        set
        {

            this.inputXY = value;
        }
    }
    protected bool gameisEnd;
    /// <summary>
    /// �Ƿ��Ѿ���ʼ����Ͽ��Խ���UpDate
    /// </summary>
    protected bool isAfterInit = false;
    public bool isCanMove;



    public string resourcesNameString;

    public string materialPath;
    public string material_Base => materialPath + resourcesNameString;
    public string material_BanTouMing => material_Base + GameDefine.MaterialBanTouMingHouZhui;
    
    public string material_QuanTouMing => material_Base + GameDefine.MaterialQuanTouMingHouZhui;
    public string material_X_Ray => material_Base + GameDefine.MaterialX_RayHouZhui;
    public string material_Zang => material_Base + GameDefine.MaterialZangHouZhui;

    public GameObject yinYingObject;


    public bool isZang;

    public virtual void Awake()
    {
        rigidbody__ = this.transform.GetComponent<Rigidbody>();
        rigidbody__.mass = GameDefine.PlayerMess;

        animator = MyTools.BfsGetComponent<Animator>(gameObject);

        rigidbody__.mass = GameDefine.PlayerMass;
        //rigidbody__.gravityScale = GameDefine.RigidBodyGravityScale;

        yinYingObject = transform.Find(GameDefine.YinYingNameString).gameObject;

    }
    public virtual void Update()
    {
        if (!isAfterInit) return;
        if (IsInXiePo())
        {
            rigidbody__.useGravity = false;
        }
        else rigidbody__.useGravity = true;

        if (canRoundToMainCamera)
        {
            // ����Ŀ�곯�����������ת�Ƕ�
            Vector3 targetDirection = Camera.main.transform.position
                - transform.position;
            targetDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            // ƽ���ؽ���ɫ��ת��Ŀ��Ƕ�
            transform.rotation =
                 Quaternion.Lerp(transform.rotation,
                 targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
    public void InitBase(Vector3 position, float rotation, Transform parent,string resourcesNameString)
    {
        this.transform.parent = parent;
        this.transform.position = position;
        Vector3 angles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(angles.x, rotation, angles.z);
        SetTag();
        this.gameObject.layer = LayerMask.NameToLayer(GameDefine.Player_Layer);
        SetStatusControl();

        paobuTuoWei = GameObject.Instantiate(Resloader.LoadGameObject(PathDefine.PlayerRunYanEffectPath));
        paobuTuoWei.SetActive(false);
        paobuTuoWei.transform.SetParent(transform, false);
        paobuTuoWei.GetComponent<TuoWei>().InitTuoWei(transform);

        youyongTuoWei = GameObject.Instantiate(Resloader.LoadGameObject(PathDefine.PlayerYouYongEffectPath));
        youyongTuoWei.SetActive(false);
        youyongTuoWei.transform.SetParent(transform, false);
        youyongTuoWei.GetComponent<TuoWei>().InitTuoWei(transform);



        rigidbody__.isKinematic = true;
        this.resourcesNameString = resourcesNameString;

        //Debug.Log(resourcesNameString);
}
    public virtual void InitCharacter(string name, MyEnum.HiderAndSeekerInputMode mode)
    {
        this.name = name;
        //������״̬���ƣ���ΪInput������Ҫ����״̬����


        if (mode == MyEnum.HiderAndSeekerInputMode.AIInput)
        {
            SetModeAIInput();
            isAIInput = true;
        }
        else
        {
            SetModePlayerInput();
            isAIInput = false;

        }
        playerBuffcontrol = new PlayerBuffControl(this);



    }
    public virtual void SetModePlayerInput()
    {
        rigidbody__.isKinematic = false;
    }
    public virtual void SetModeAIInput()
    {
        rigidbody__.isKinematic = true;
    }
    protected virtual void SetTag()
    {

    }
    protected virtual void SetStatusControl()
    {

    }
    public virtual void OnOneGameEnd(bool iswin)
    {
        isCanMove = false;
        if (playerBuffcontrol != null)
            playerBuffcontrol.OnOneGameEnd();


    }
    public virtual void CanMove()
    {
        isCanMove = true;
    }
    public void GetMoney(int money,Action action=null)
    {
       // Debug.Log("��һ�ý��:" + money);
        EventManager.Instance.CallOnPlayerGetMoneyAction(money);
        action?.Invoke();
    }

    /// <summary>
    /// ����ʼ���͵�ʱ����������
    /// </summary>
    public void BeginChuanSong()
    {
        this.paobuTuoWei.SetActive(false);
    }
    /// <summary>
    /// �ж��Ƿ���б����
    /// </summary>
    /// <returns></returns>
    public bool IsInXiePo()
    {
        //if (name != "Hider_PlayerInput") return false;
        RaycastHit hit;
        Debug.DrawLine(transform.position + transform.up * 0.2f,
                transform.position + transform.up * 0.2f + transform.forward * 1.5f, Color.green);

        if (!Physics.Raycast(transform.position + transform.up * 0.2f, transform.forward,
           out hit, 1.5f, 1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
            if (!Physics.Raycast(transform.position, -transform.up,
               out hit, 1.5f, 1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
                if (!Physics.Raycast(transform.position + transform.up * 0.1f, transform.forward,
            out hit, 1.5f, 1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
                    if (!Physics.Raycast(transform.position, transform.forward,
                    out hit, 1.5f, 1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
                    {


                        return false;
                        //û����������w
                    }
        var x = Vector3.Angle(transform.up, hit.normal);

        if (x == 0) return false;


        if(x>5f)
        {
            //Debug.Log(name + "  ��б����");
            return true;
        }

        return false;
    }

    public virtual void SetMaterBanTouMing()
    {
        MaterialsManager.Instance.ChangeObjectMaterial
               (gameObject, material_BanTouMing);
        if (yinYingObject != null) yinYingObject.SetActive(true);

        isZang = false;
    }
    public virtual void SetMaterX_Ray()
    {
        MaterialsManager.Instance.ChangeObjectMaterial
               (gameObject, material_X_Ray);
        Debug.Log(name + "  -> SetMaterX_Ray()");
        if (yinYingObject != null) yinYingObject.SetActive(true);

        isZang = false;
    }
    public virtual void SetMaterZang()
    {
        MaterialsManager.Instance.ChangeObjectMaterial
               (gameObject, material_Zang);
        if (yinYingObject != null) yinYingObject.SetActive(true);

        isZang = true;
    }
    public virtual void SetMaterBase()
    {
        MaterialsManager.Instance.ChangeObjectMaterial
               (gameObject, material_Base);
        if (yinYingObject != null) yinYingObject.SetActive(true);

        isZang = false;
    }
}