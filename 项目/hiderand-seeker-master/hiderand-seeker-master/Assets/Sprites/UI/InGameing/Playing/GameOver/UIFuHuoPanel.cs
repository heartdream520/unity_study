using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFuHuoPanel : MonoBehaviour
{
    public Image daojishiImage;
    public Text daojishiText;
    private float time;
    private float resTime;
    private int nowShuzi = 5;
    Coroutine coroutine;

    public GameObject notFuHuoPanel;
    private void Start()
    {
        daojishiImage.fillAmount = 1;
        coroutine = StartCoroutine(StartDaoJiShiIEnumerator());
    }
    IEnumerator StartDaoJiShiIEnumerator()
    {
        daojishiText.text = nowShuzi.ToString();
        resTime = 1f;
        while (true)
        {
            resTime -= Time.deltaTime;
            if (resTime <= 0)
            {
                resTime += 1;
                nowShuzi--;
            }

            daojishiText.text = nowShuzi.ToString();
            daojishiImage.fillAmount -= Time.deltaTime / 5f;
            if (daojishiImage.fillAmount <= 0)
            {
                InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
                {
                    OnChickFuHuoButton();

                }, 0.3f);
                yield break;
            }
            yield return null;
        }
    }
    public void OnChickFuHuoButton()
    {

        FuHuo();
    }
    public void OnChickNoFuHuoButton()
    {
        notFuHuoPanel.SetActive(true);
        this.gameObject.SetActive(false);
        if (coroutine != null)
            StopCoroutine(coroutine);
        Debug.LogWarning("UIFuHuoPanel->CallOneGameEndAction(false) ");

        EventManager.Instance.CallOneGameEndAction(false);

    }
    /// <summary>
    /// 看广告然后复活的逻辑 ,并添加保护罩的Buff
    /// </summary>
    public void FuHuo()
    {
        bool adPlaySuccess = false; ;
        if (GameingMainManager.Instance.player_Identity_Enum == MyEnum.Player_Identity_Enum.Hider)
            RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Relive_Hider, () =>
            {
                EventManager.Instance.CallOneGameFuHuoAction();
                //notFuHuoPanel.SetActive(true);
                this.gameObject.SetActive(false);
                //UIManager.Instance.TryGetNowActiveUI<UIGamePlaying>().OnChickClose();

            }, () =>
            {
                if (coroutine != null)
                    StopCoroutine(coroutine);
                coroutine = null;
                adPlaySuccess = true;
            });
        else
        {
            RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Relive_Seeker, () =>
            {
                EventManager.Instance.CallOneGameFuHuoAction();
                //notFuHuoPanel.SetActive(true);
                this.gameObject.SetActive(false);
                //UIManager.Instance.TryGetNowActiveUI<UIGamePlaying>().OnChickClose();

            }, () =>
            {
                if (coroutine != null)
                    StopCoroutine(coroutine);
                coroutine = null;
                adPlaySuccess = true;
            });
        }
        if (!adPlaySuccess && daojishiImage.fillAmount <= 0)
        {
            OnChickNoFuHuoButton();
        }
    }

}
