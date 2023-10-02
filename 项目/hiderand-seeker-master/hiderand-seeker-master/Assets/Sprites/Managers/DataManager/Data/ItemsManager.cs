using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameData
{
    public class ItemsManager: GameDadaBase
    {
        public override void Init()
        {
            base.Init();
        }
        internal void OnEnable()
        {

        }
        internal void OnDisable()
        {

        }
        public override GameSaveData SaveData()
        {
            return new GameSaveData();
        }

        public override void UpDataSaveData(GameSaveData gameSave)
        {

        }
        internal int GetSelectedItem()
        {
            return 1001;
        }




    }
}