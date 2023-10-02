
using System;
using UnityEngine;

public class GameingMoneyManager
{
    public int money = 0;
    public GameingMoneyManager()
    {
        EventManager.Instance.PlayerGetMoneyAction += this.OnPlayerGetMoney;
    }



    public void OnGameEnd()
    {
        EventManager.Instance.PlayerGetMoneyAction -= this.OnPlayerGetMoney;
    }
    private void OnPlayerGetMoney(int money)
    {
        this.money += money;
       
    }
}