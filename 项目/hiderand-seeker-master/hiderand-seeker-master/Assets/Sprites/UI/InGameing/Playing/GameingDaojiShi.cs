using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class GameingDaojiShi : MonoBehaviour
{
    public UIGamePlaying uIGamePlaying;
    public Text daojishiText;
    Coroutine coroutine;
    private void Start()
    {
        EventManager.Instance.OneGameEndAction += this.OnOneGameEnd;
        EventManager.Instance.OneGameFailAction += this.OnOneGameFail;
        EventManager.Instance.OneGameFuHuoAction += this.OnOneGameFuHuo;
    }



    private void OnDestroy()
    {
        EventManager.Instance.OneGameEndAction -= this.OnOneGameEnd;
        EventManager.Instance.OneGameFailAction -= this.OnOneGameFail;
        EventManager.Instance.OneGameFuHuoAction -= this.OnOneGameFuHuo;

    }
    private bool stopJiShi=false;
    private void OnOneGameEnd(bool iswin)
    {
        StopAllCoroutines();
        stopJiShi = true;
    }


    private void OnOneGameFail()
    {
        StopAllCoroutines();
        stopJiShi = true;
    }
    private void OnOneGameFuHuo()
    {
        StopAllCoroutines();
        if (GameingMainManager.Instance.player_Identity_Enum == MyEnum.Player_Identity_Enum.Hider)
        {
            StopAllCoroutines();
            nowBeforGameStartTime = -1;
            StartCoroutine(DaojishiIEnumerator());
            stopJiShi = false;
            
        }
        else
        {
            StopAllCoroutines();
            nowBeforGameStartTime = -1;
            time = 15f;
            StartCoroutine(DaojishiIEnumerator());
            stopJiShi = false;
        }
    }
    public void BeginDaoJiShi()
    {
        time = GameDefine.OneGameTime;
        coroutine = StartCoroutine(DaojishiIEnumerator());
    }
    public float time;
    public float nowBeforGameStartTime = -1;
    
    IEnumerator DaojishiIEnumerator()
    {
        bool hasPlayGameEndAudio = false;
        SetTime((int)time);

       

        while(time>0)
        {
            if (TestManager.Instance.testMode)
            {
                if (TestManager.Instance.noSuccessTime)
                    time = 30f;
            }
            if (stopJiShi)
            {
                yield return null;
            }
            time -= Time.deltaTime;
            if (!hasPlayGameEndAudio && time <= 5f && time > 4f)
            {
                hasPlayGameEndAudio = true;
                AudioManager.Instance.PlayGameEndDaoJiShiSound();
            }
            if (nowBeforGameStartTime == -1)
            {
                nowBeforGameStartTime = time;
            }
            else
            {
                if (nowBeforGameStartTime - time >= 1f)
                {
                    nowBeforGameStartTime = Mathf.RoundToInt(time);
                }
            }
            SetTime((int)nowBeforGameStartTime);
            yield return null;
        }
        if (uIGamePlaying.player_Identity_Enum == MyEnum.Player_Identity_Enum.Hider)
        {

            EventManager.Instance.CallOneGameEndAction(true);
        }
        else
        {
            if (uIGamePlaying.nowHasBeenAttacked >=
                GameDefine.GameHiderCount - GameDefine.GameAddAttackCount)
            {

                EventManager.Instance.CallOneGameEndAction(true);
            }
            else EventManager.Instance.CallOneGameFailAction();
        }
    }
    private void SetTime(int time)
    {
        daojishiText.text = "00:" + time.ToString("00");
    }
}
