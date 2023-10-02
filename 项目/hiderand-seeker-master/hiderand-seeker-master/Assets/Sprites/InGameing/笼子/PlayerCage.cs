
using UnityEngine;
using UnityEngine.UI;

public class PlayerCage : MonoBehaviour
{
    public Hider hider;
    public GameObject Canvas;
    public GameObject daoJiShiPanel;
    public Text daoJiShiText;
    public Image daoJiShiImage;
    private float daojishiTime = 3f;
    private float nowdaojishiTime = 1f;
    public void InitPlayerCage(Hider hider)
    {
        daojishiTime = GameDefine.HelpOtherDaojishiTime;
        this.hider = hider;
        nowdaojishiTime = daojishiTime;

    }
    private void OnEnable()
    {
        Canvas.GetComponent<Canvas>().worldCamera=Camera.main;
        daoJiShiPanel.SetActive(false);
    }
    private void OnDisable()
    {
        
    }
    private void LateUpdate()
    {
        if (Camera.main != null)
        {
            // 获取主摄像机的位置
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 toCameraDirection = cameraPosition - daoJiShiPanel.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(-toCameraDirection, Vector3.up);

            // 应用旋转到UI元素
            daoJiShiPanel.transform.rotation = lookRotation;
            // 让UI元素始终面向主摄像机
            //daoJiShiPanel.GetComponent<RectTransform>().LookAt(- cameraPosition, Vector3.up);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!this.hider.isAIInput)
        {
            return;
        }
        if (other.tag == GameDefine.HiderTag && !other.GetComponent<Hider>().isAIInput)
        {
            if (TestManager.Instance.testMode)
            {
                daojishiTime = TestManager.Instance.helpOtherDaojishiTime;
            }
            nowdaojishiTime = daojishiTime;
            daoJiShiPanel.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!this.hider.isAIInput)
        {
            return;
        }
        if (other == null) return;
        
        if (other.tag == GameDefine.HiderTag && !other.GetComponent<Hider>().isAIInput)
        {
            if(other.GetComponent<Hider>().HasBeenAttack||
                ( GameingMainManager.Instance.oneGameIsEnd
                &&GameingMainManager.Instance.gameingManager==null))
            {
                daoJiShiPanel.SetActive(false);
                return;
            }
            if (nowdaojishiTime > 0)
            {
                nowdaojishiTime -= Time.deltaTime;
            }
            nowdaojishiTime = Mathf.Max(nowdaojishiTime, 0);
            daoJiShiText.text = nowdaojishiTime.ToString("0.0");
            daoJiShiImage.fillAmount = (daojishiTime - nowdaojishiTime) / daojishiTime;
            if (nowdaojishiTime == 0)
            {
                nowdaojishiTime = daojishiTime;
                hider.GetHelp();
                EffectManager.Instance.CreateOneJinBi(hider.transform.position);
                AudioManager.Instance.PlayGetCoin();
                hider.GetMoney(GameDefine.PlayerHelpOtherMonty);
                ObjectPoolManager.Instance.SetOneObjectInPool(gameObject);
                hider.longZiObject = null;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!this.hider.isAIInput)
        {
            return;
        }
        if (other.tag == GameDefine.HiderTag && !other.GetComponent<Hider>().isAIInput)
        {
            nowdaojishiTime = daojishiTime;
            daoJiShiPanel.SetActive(false);
        }
    }
    private void Update()
    {
        if(TestManager.Instance.testMode)
        {
            daojishiTime = TestManager.Instance.helpOtherDaojishiTime;
        }
    }



}
