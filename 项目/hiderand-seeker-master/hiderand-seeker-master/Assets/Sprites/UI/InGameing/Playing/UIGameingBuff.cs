using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameingBuff : MonoBehaviour
{
    public Image bgImage;
    public Image barImage;
    public Sprite[] sprites;

    float buffLastTime; 
    internal void SetBuff(GuangGaoBuffBase buffBase)
    {

        if(buffBase is GoThroughtWallsBuff)
        {
            bgImage.sprite = sprites[0];
            buffLastTime = GameDefine.GoThroughtWallsBuffLastTime;
        }
        else if(buffBase is SpeedBuff)
        {
            bgImage.sprite = sprites[1];
            buffLastTime = GameDefine.SpeedBuffLastTime;

        }
        else if(buffBase is WuDiBuff)
        {
            bgImage.sprite = sprites[2];
            buffLastTime = GameDefine.WuDiBuffLastTime;

        }
        else if(buffBase is X_RayBuff)
        {
            bgImage.sprite = sprites[3];
            buffLastTime = GameDefine.X_RayBuffLastTime;

        }
        StopAllCoroutines();
        StartCoroutine(SetBuffBarIEnumerator());

    }
    IEnumerator SetBuffBarIEnumerator()
    {
        barImage.fillAmount = 1;
        float nowLasttime = buffLastTime;
        while (nowLasttime > 0)
        {
            nowLasttime -= Time.deltaTime;
            if (nowLasttime < 0) nowLasttime = 0;
            barImage.fillAmount = nowLasttime / buffLastTime;
            yield return new WaitForEndOfFrame();

        }
        this.gameObject.SetActive(false);
    }

}
