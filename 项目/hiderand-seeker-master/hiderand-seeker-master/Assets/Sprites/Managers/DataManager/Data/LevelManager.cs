using MyEnum;
using System;
using UnityEngine;
namespace GameData
{
    public class LevelManager : GameDadaBase
    {
        private int nowLevel = 0;
        public int NowLevel
        {
            get
            {
                return nowLevel;
            }
            set
            {
                if (value > nowLevel)
                {
                    hasWinOneGame = false;
                }
                nowLevel = value;
                GameDataInSaveToFileManager.Instance.SaveAllData();
            }
        }
        /// <summary>
        /// 不是游戏关卡的场景数量
        /// </summary>
        private int notIsLevelSceneCnt;
        /// <summary>
        /// 游戏中的关卡数量
        /// </summary>
        private int levelCnt;
        /// <summary>
        /// 已经赢得了一场游戏
        /// </summary>
        bool hasWinOneGame = false;

        /// <summary>
        /// -1表示还没有进入过第一关,1表示已经进入过第一关
        /// </summary>
        public int notInFitstLevel;
        public override void Init()
        {
            base.Init();
            notIsLevelSceneCnt = 0;
            while (!CheckLevel(notIsLevelSceneCnt)) notIsLevelSceneCnt++;
            levelCnt = (int)MyEnum.SceneNameEnum.MaxCnt - notIsLevelSceneCnt;

            // Debug.Log("notIsLevelSceneCnt:" + notIsLevelSceneCnt + "   levelCnt" + levelCnt);
        }

        private bool CheckLevel(int notIsLevelSceneCnt)
        {
            string x = ((MyEnum.SceneNameEnum)notIsLevelSceneCnt).ToString();
            if (x.StartsWith("Level_"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void OnEnable()
        {
            EventManager.Instance.OneGameEndAction += this.OnOneGameEnd;
        }
        internal void OnDisable()
        {
            EventManager.Instance.OneGameEndAction -= this.OnOneGameEnd;
        }

        private void OnOneGameEnd(bool iswin)
        {
            if (iswin)
            {
                hasWinOneGame = true;
            }
        }


        public override GameSaveData SaveData()
        {
            GameSaveData data = new GameSaveData();
            data.nowLevel = nowLevel;
            data.hasWinOneGame = hasWinOneGame;
            //data.nowLevel = 1;
            data.notInFitstLevel = notInFitstLevel;
            return data;
        }

        public override void UpDataSaveData(GameSaveData gameSave)
        {
            this.nowLevel = gameSave.nowLevel;
            //Debug.LogError("NowLevel:" + nowLevel);
            if (gameSave.hasWinOneGame)
            {
                nowLevel++;
            }
            if (nowLevel == 0)
                nowLevel = 1;
            notInFitstLevel = gameSave.notInFitstLevel;

            if (notInFitstLevel == 0)
                notInFitstLevel = -1;
            //nowLevel = 3;
        }
        internal SceneNameEnum GetNowLevel()
        {
            if (TestManager.Instance.testMode)
            {
                nowLevel = TestManager.Instance.testLevelId;

                return GetLevel(TestManager.Instance.testLevelId);
            }
            return GetLevel(nowLevel);
        }
        internal SceneNameEnum GetNextLevel()
        {

            return GetLevel(nowLevel + 1);

        }
        private SceneNameEnum GetLevel(int nowLevel)
        {
            int levelId = (nowLevel - 1) % levelCnt + 1;
            levelId = notIsLevelSceneCnt + levelId - 1;
            // Debug.LogWarning("nowLevel:" + nowLevel +
            //     "  SceneNameEnum:" + ((MyEnum.SceneNameEnum)levelId).ToString());
            return (MyEnum.SceneNameEnum)levelId;
        }
        public int GetNowLevelInt()
        {
            return (NowLevel > 1000 ? 1000 : NowLevel);
        }

        internal string GetAdLevel()
        {
            return (NowLevel > 1000 ? 1000 : NowLevel).ToString();
        }
    }
}