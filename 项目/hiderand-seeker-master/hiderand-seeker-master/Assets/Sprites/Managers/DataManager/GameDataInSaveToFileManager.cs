using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameData
{
    public class GameDataInSaveToFileManager : Singleton<GameDataInSaveToFileManager>
    {

        public Dictionary<string, IGameData> igameDataDir ;
        public Dictionary<string, GameSaveData> gameSaveDataDir;
        /// <summary>
        /// 存放数据的文件夹
        /// </summary>
        private string saveDataDir;
        private string saveDataPath;
        public void Init()
        {
            //base.OnStart();
            saveDataDir = Application.persistentDataPath + "/SAVE/";
            saveDataPath = saveDataDir + "data.sav";
            Debug.Log(saveDataPath);
            igameDataDir = new Dictionary<string, IGameData>();
            gameSaveDataDir = new Dictionary<string, GameSaveData>();
        }
        public void OnEnable()
        {
            Application.quitting += SaveAllData;

        }
        public void OnDisable()
        {
            Application.quitting -= SaveAllData;
        }
        /// <summary>
        /// 当进入游戏时做的事
        /// </summary>
        public void OnInGame()
        {
            if (!Directory.Exists(this.saveDataDir))
            {
                Directory.CreateDirectory(this.saveDataDir);
            }
            
            UpDataAllData();
        }

        public void Register(IGameData gameData)
        {
            string name = gameData.GetType().Name;
            if (!igameDataDir.ContainsKey(name))
            {
                igameDataDir.Add(name, gameData);
            }
        }
        public void SaveAllData()
        {
            foreach (var x in this.igameDataDir)
            {
                string name = x.Key;
                var igameData = x.Value;
                if (gameSaveDataDir.ContainsKey(name))
                {
                    this.gameSaveDataDir[name] = igameData.SaveData();
                }
                else
                    this.gameSaveDataDir.Add(name, igameData.SaveData());
            }
            this.WriteToFile();
        }
        public void UpDataAllData()
        {
            this.ReadFromFile();
            foreach (var x in this.igameDataDir)
            {
                string name = x.Key;
                var igameData = x.Value;
                if (!this.gameSaveDataDir.ContainsKey(name))
                    gameSaveDataDir.Add(name, new GameSaveData());
                igameData.UpDataSaveData(this.gameSaveDataDir[name]);
            }
        }
        private void WriteToFile()
        {
            var jsonData = JsonConvert.SerializeObject(this.gameSaveDataDir, Formatting.Indented);
            if (!File.Exists(saveDataPath))
            {
                var x = File.Create(saveDataPath);
                x.Close();
            }
            File.WriteAllText(saveDataPath, jsonData);
            /*
            using (FileStream file = File.OpenWrite(this.saveDataPath))
            {
                file.Write(Encoding.UTF8.GetBytes(jsonData));
                file.Close();
            }
            */

        }
        private void ReadFromFile()
        {

            if (File.Exists(this.saveDataPath))
            {
                // 使用 File.ReadAllText 一次性读取文件内容
                string jsonData = File.ReadAllText(this.saveDataPath);

                // 直接进行 JSON 反序列化
                this.gameSaveDataDir = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(jsonData);
            }
            else
            {
                this.gameSaveDataDir = new Dictionary<string, GameSaveData>();
            }
            /*
            string jsonData = "";
            byte[] bytes = new byte[1024 * 1024];
            int count = 0;
            Array.Clear(bytes, 0, bytes.Length);
            if (File.Exists(this.saveDataPath))
            {
                using (FileStream file = File.OpenRead(this.saveDataPath))
                {
                    count = file.Read(bytes);
                }
                jsonData = Encoding.UTF8.GetString(bytes, 0, count);
                this.gameSaveDataDir = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(jsonData);

            }
            */
        }
    }
}