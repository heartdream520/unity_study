using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using Unity.VisualScripting;
using System;

public class GameDataManager : MonoSingleton<GameDataManager>
{
    private GameDataInSaveToFileManager gameDataInSaveToFileManager;
    public CharactersManager charactersManager=new CharactersManager();
    public ItemsManager itemsManager=new ItemsManager();
    public LevelManager levelManager=new LevelManager();
    public MoneyManager moneyManager=new MoneyManager();
    public void Init()
    {
        gameDataInSaveToFileManager = GameDataInSaveToFileManager.Instance;
        gameDataInSaveToFileManager.Init();

        charactersManager.Init();
        itemsManager.Init();
        levelManager.Init();
        moneyManager.Init();


        gameDataInSaveToFileManager.OnInGame();
    }
    private void OnEnable()
    {
        if (gameDataInSaveToFileManager == null)
            gameDataInSaveToFileManager = GameDataInSaveToFileManager.Instance;
        gameDataInSaveToFileManager.OnEnable();

        charactersManager.OnEnable();
        itemsManager.OnEnable();
        levelManager.OnEnable();
        moneyManager.OnEnable();

        
    }
    private void OnDisable()
    {
        if (gameDataInSaveToFileManager == null)
            gameDataInSaveToFileManager = GameDataInSaveToFileManager.Instance;
        gameDataInSaveToFileManager.OnDisable();

        charactersManager.OnDisable();
        itemsManager.OnDisable();
        levelManager.OnDisable();
        moneyManager.OnDisable();
    }

}