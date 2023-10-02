using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameData
{
    //���ݱ���ӿ�
    public interface IGameData
    {

        public void SaveableRegister()
        {
            GameDataInSaveToFileManager.Instance.Register(this);
        }
        public GameSaveData SaveData();
        public void UpDataSaveData(GameSaveData gameSave);
    }
    //Ҫ���������
    public class GameSaveData
    {
        public int nowLevel;
        public bool hasWinOneGame;
        public int notInFitstLevel;

        public int money;

        public  string selectedHider_String;
        public string selectedSeeker_String;
        public Dictionary<string, bool> hiderJieSuoDic;
        public Dictionary<string, bool> seekerJieSuoDic;
        public float characterJinDu;
        public string characterJinDuString;

        public int hiderJieSuoId;
        public int seekerJieSuoId;



    }
}