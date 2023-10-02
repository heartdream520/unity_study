using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingPanel : MonoBehaviour
{
    public Slider slider;
    private void OnEnable()
    {
        //Debug.Log("UILoadingPanel=>OnEnable");
    }
    private void Awake()
    {
        StartCoroutine(LoadingIEnumerator());
    }
    private float jindu;
    public float loadingTime = 3f;

    IEnumerator LoadingIEnumerator()
    {
        jindu = 0;
        yield return new WaitForSeconds(0.5f);
        float speed = 1 / loadingTime;
        while (jindu < 1f)
        {
            jindu += (MyRandom.RangeFloat(-0.3f, 0.3f) + speed) * Time.deltaTime;
            Mathf.Clamp01(jindu);
            slider.value = jindu;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);

        AudioManager.Instance.PlayMainBackMusic();

        if (!TestManager.Instance.testMode)
            BannerAdManager.Instance.PlayBanner();


        TalkingDataManager.Instance.SendPlayFirstLoadingEnd();
    }

}
