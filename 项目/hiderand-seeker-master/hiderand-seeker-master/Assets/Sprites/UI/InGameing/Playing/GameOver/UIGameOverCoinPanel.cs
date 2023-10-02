using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverCoinPanel : MonoBehaviour
{
    public Text coinText;
    int nowMoney;
    Coroutine coroutine;
    internal void SetCoinToNum(int money)
    {
        GameDataManager.Instance.moneyManager.Money += money - nowMoney;
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(SetMoneyIEnumerator(money));
        
    }
    IEnumerator SetMoneyIEnumerator(int x)
    {
        float res = x - nowMoney;
        res += 100;
        while(nowMoney<x)
        {
            int add = (int)(res * Time.deltaTime);
            nowMoney += add;
            nowMoney = Mathf.Min(x, nowMoney);
            this.coinText.text = nowMoney.ToString();
            yield return new WaitForEndOfFrame();
        }
    }
}
