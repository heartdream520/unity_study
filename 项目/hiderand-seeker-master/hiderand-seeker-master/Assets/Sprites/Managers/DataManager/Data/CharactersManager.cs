using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameData
{
    public class CharactersManager : GameDadaBase
    {
        public string selectedHider_String;
        public string selectedSeeker_String;
        public bool haveNotGet = false;
        int hiderJieSuoId = 0;
        int seekerJieSuoId = 0;
        /// <summary>
        /// 要获取的奖励角色的名字
        /// </summary>
        public string CharacterJinDuString
        {
            get { return characterJinDuString; }
            set { characterJinDuString = value;
                GameDataInSaveToFileManager.Instance.SaveAllData(); }
        }
        public string characterJinDuString;
        public float CharacterJinDu
        {
            get { return characterJinDu; }
            set { characterJinDu = value;
                GameDataInSaveToFileManager.Instance.SaveAllData();
            }
        }
        public float characterJinDu;


        public Dictionary<string, bool> HiderJieSuoDic
        {
            get { return hiderJieSuoDic; }
            set { hiderJieSuoDic = value;
                GameDataInSaveToFileManager.Instance.SaveAllData();
            }
        }
        public Dictionary<string, bool> hiderJieSuoDic;
        public Dictionary<string, bool> SeekerJieSuoDic
        {
            get { return seekerJieSuoDic; }
            set { seekerJieSuoDic = value;
                GameDataInSaveToFileManager.Instance.SaveAllData();
            }
        }
        public Dictionary<string, bool> seekerJieSuoDic;
        public string SelectedHider_String
        {
            get{ return selectedHider_String; }
            set
            {
                if (selectedHider_String == value) return;

                EventManager.Instance.CallOnSelectedHiderChangeAction(value);
                selectedHider_String = value;
                GameDataInSaveToFileManager.Instance.SaveAllData();
            }
        }
        public string SelectedSeeker_String
        {
            get { return selectedSeeker_String; }
            set
            {
                selectedSeeker_String = value;
                GameDataInSaveToFileManager.Instance.SaveAllData();
            }
        }

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
            var data = new GameSaveData();
            data.selectedHider_String = SelectedHider_String;
            data.selectedSeeker_String = SelectedSeeker_String;
            data.hiderJieSuoDic = HiderJieSuoDic;
            data.seekerJieSuoDic = SeekerJieSuoDic;
            data.characterJinDu = CharacterJinDu;
            data.characterJinDuString = CharacterJinDuString;

            data.seekerJieSuoId = seekerJieSuoId;
            data.hiderJieSuoId = hiderJieSuoId;
            return data;
        }

        public override void UpDataSaveData(GameSaveData gameSave)
        {
            selectedHider_String = gameSave.selectedHider_String;
            selectedSeeker_String = gameSave.selectedSeeker_String;
            hiderJieSuoDic = gameSave.hiderJieSuoDic;
            seekerJieSuoDic = gameSave.seekerJieSuoDic;
            characterJinDu = gameSave.characterJinDu;
            characterJinDuString = gameSave.characterJinDuString;

            seekerJieSuoId = gameSave.seekerJieSuoId;
            hiderJieSuoId = gameSave.hiderJieSuoId;
            InitFirstInGame();

        }
        private void InitFirstInGame()
        {
            if (string.IsNullOrEmpty(SelectedHider_String)) selectedHider_String = "Hider_01";
            if (string.IsNullOrEmpty(SelectedSeeker_String)) selectedSeeker_String = "Seeker_01";
            if (hiderJieSuoDic == null || hiderJieSuoDic.Count == 0)
            {
                hiderJieSuoDic = new Dictionary<string, bool>();
                var list = SO_DataManager.Instance.hiderSo_Data.characterList;
                hiderJieSuoDic.Add(list[0].name, true);
                for (int i = 1; i < list.Count; i++)
                {
                    hiderJieSuoDic.Add(list[i].name, false);
                }
            }
            if (seekerJieSuoDic == null || seekerJieSuoDic.Count == 0)
            {
                seekerJieSuoDic = new Dictionary<string, bool>();
                var list = SO_DataManager.Instance.SeekerSo_Data.characterList;
                seekerJieSuoDic.Add(list[0].name, true);
                for (int i = 1; i < list.Count; i++)
                {
                    seekerJieSuoDic.Add(list[i].name, false);
                }
            }
            if(string.IsNullOrEmpty(characterJinDuString))
            {
                characterJinDuString = GetOneCharacterJinDuString();
            }
            //characterJinDu = 0;
        }
        public string GetOneCharacterJinDuString()
        {

            CharacterJinDuString = GetCharacterJieSuo();
            //Debug.Log("解锁中的角色："+ CharacterJinDuString);
            return CharacterJinDuString;
        }
        List<Character_SoData> hiderSoList => 
            SO_DataManager.Instance.hiderSo_Data.characterList;
        List<Character_SoData> seekerSoList => 
            SO_DataManager.Instance.SeekerSo_Data.characterList;
       

       
        private string GetCharacterJieSuo()
        {
            int op = -1;
            
            if (!string.IsNullOrEmpty(CharacterJinDuString))
            {
                if (CharacterJinDuString.StartsWith("Hider"))
                {
                    op = 2;
                    if (!haveNotGet)
                        if (!HiderJieSuoDic[CharacterJinDuString])
                            return CharacterJinDuString;
                }
                else
                {
                    op = 1;
                    if (!haveNotGet)
                        if (!SeekerJieSuoDic[CharacterJinDuString])
                            return CharacterJinDuString;
                }
            }
            if (op == -1) op = 1;
            haveNotGet = false;
            if (op == 1)
            {
                for(int i=hiderJieSuoId+1;i<hiderSoList.Count;i++)
                {
                   
                    if (!HiderJieSuoDic[hiderSoList[i].name])
                    {
                        hiderJieSuoId = i;
                        return hiderSoList[i].name;
                    }
                }
                for(int i=0;i< hiderJieSuoId;i++)
                {
                    if (!HiderJieSuoDic[hiderSoList[i].name])
                    {
                        hiderJieSuoId = i;
                        return hiderSoList[i].name;
                    }
                }
                for (int i = seekerJieSuoId + 1; i < seekerSoList.Count; i++)
                {

                    if (!seekerJieSuoDic[seekerSoList[i].name])
                    {
                        seekerJieSuoId = i;
                        return seekerSoList[i].name;
                    }
                }
                for (int i = 0; i < seekerJieSuoId; i++)
                {
                    if (!seekerJieSuoDic[seekerSoList[i].name])
                    {
                        seekerJieSuoId = i;
                        return seekerSoList[i].name;
                    }
                }
                /*
                foreach (var x in this.HiderJieSuoDic)
                {
                    if (x.Key.StartsWith("Hider"))
                        if (!x.Value) return x.Key;
                }
                foreach (var x in this.SeekerJieSuoDic)
                {
                    if (x.Key.StartsWith("Seeker"))
                        if (!x.Value) return x.Key;
                }
                */
            }
            else
            {
                for (int i = seekerJieSuoId + 1; i < seekerSoList.Count; i++)
                {

                    if (!seekerJieSuoDic[seekerSoList[i].name])
                    {
                        seekerJieSuoId = i;
                        return seekerSoList[i].name;
                    }
                }
                for (int i = 0; i < seekerJieSuoId; i++)
                {
                    if (!seekerJieSuoDic[seekerSoList[i].name])
                    {
                        seekerJieSuoId = i;
                        return seekerSoList[i].name;
                    }
                }
                for (int i = hiderJieSuoId + 1; i < hiderSoList.Count; i++)
                {

                    if (!HiderJieSuoDic[hiderSoList[i].name])
                    {
                        hiderJieSuoId = i;
                        return hiderSoList[i].name;
                    }
                }
                for (int i = 0; i < hiderJieSuoId; i++)
                {
                    if (!HiderJieSuoDic[hiderSoList[i].name])
                    {
                        hiderJieSuoId = i;
                        return hiderSoList[i].name;
                    }
                }
               
                /*
                foreach (var x in this.SeekerJieSuoDic)
                {
                    if (x.Key.StartsWith("Seeker"))
                        if (!x.Value) return x.Key;
                }
                foreach (var x in this.HiderJieSuoDic)
                {
                    if (x.Key.StartsWith("Hider"))
                        if (!x.Value) return x.Key;
                }
                */
            }
            return "";
        }
        internal GameObject GetNowSelectedHiderPrefabs()
        {
            return SO_DataManager.Instance.hiderSo_Data.GetCharacter(SelectedHider_String).prefabs;
        }
        internal GameObject GetOneRanderHiderPrefabs()
        {
            return SO_DataManager.Instance.hiderSo_Data.GetOneRander().prefabs;
        }

        internal GameObject GetNowSelectedSeekerPrefabs()
        {
            return SO_DataManager.Instance.SeekerSo_Data.GetCharacter(SelectedSeeker_String).prefabs;
        }
        internal GameObject GetOneRanderSeekerPrefabs()
        {
            return SO_DataManager.Instance.SeekerSo_Data.GetOneRander().prefabs;
        }
        public void SetHiderJieSuo(string name)
        {
            this.HiderJieSuoDic[name] = true;
        }
        public void SetSeekerJieSuo(string name)
        {
            this.SeekerJieSuoDic[name] = true;
        }


       

    }
}