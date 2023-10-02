using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameingTimeManager 
{
    private float beforGameStartTime;
    private bool gameStart;

    private UIGamePlaying uiGamePlayering;
    public InGameingTimeManager()
    {
        Init();
        
    }
    public void Init()
    {
        beforGameStartTime = GameDefine.BeforeGameStartTime;
        gameStart = false;

        AudioManager.Instance.PlayGameBeginDaoJiShiSound();


        EventManager.Instance.CallBeforeTimeBeginflowAction();

        uiGamePlayering = UIManager.Instance.Show<UIGamePlaying>();
       
        EventManager.Instance.CallBeforGameBeginTimeChangeAction(beforGameStartTime);
    }

    internal void Updata()
    {
        if(!gameStart)
        {
            beforGameStartTime -= Time.deltaTime;
            beforGameStartTime = Mathf.Max(0, beforGameStartTime);
            EventManager.Instance.CallBeforGameBeginTimeChangeAction(beforGameStartTime);
            if (beforGameStartTime<=0)
            {
                EventManager.Instance.CallTimeOutGameBeginAction();
                gameStart = true;
                AudioManager.Instance.soundAudioSource.Stop();
                AudioManager.Instance.PlayDaoJiShiEndGameBegin();
                
                InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
                {
                    AudioManager.Instance.PlaySeekerXiao();
                }, 0.5f);
            }
        }
    }
}
