using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameData
{
    public class MoneyManager : GameDadaBase
    {
        private int money;

        public int Money
        {
            get
            {
                
                return money;
            }
            set
            {
                //Debug.Log("金钱变化：" + (value - money).ToString()+"现有金钱:"+money);
                if(value<0)
                {
                    Debug.LogError("金钱减为负数");
                }
                GameDataInSaveToFileManager.Instance.SaveAllData();
                money = value;
                EventManager.Instance.CallOnCoinCountChange(this.money);
            }
        }
        public override void Init()
        {
            base.Init();
        }
        internal void OnEnable()
        {
            EventManager.Instance.OneGameEndAction += OnOneGameEnd;
        }
        internal void OnDisable()
        {
            EventManager.Instance.OneGameEndAction -= OnOneGameEnd;
        }

        private void OnOneGameEnd(bool iswin)
        {
           // if (iswin)
            //    this.Money += GameingMainManager.Instance.gameingMoneyManager.money;


            //  Debug.Log($"玩家获取money:{GameingMainManager.Instance.gameingMoneyManager.money} " +
            //      $",目前的money:{money}");
        }

        public override GameSaveData SaveData()
        {
            var data= new GameSaveData();
            data.money = this.Money;
            return data;
        }
        public override void UpDataSaveData(GameSaveData gameSave)
        {
            this.money = gameSave.money;
        }
    }
}
