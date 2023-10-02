using GameData;

namespace GameData
{
    public abstract class GameDadaBase : IGameData
    {
        public virtual void Init()
        {
            SaveableRegister();
        }
        public void SaveableRegister()
        {
            GameDataInSaveToFileManager.Instance.Register(this);
        }

        public abstract GameSaveData SaveData();

        public abstract void UpDataSaveData(GameSaveData gameSave);

    }
}