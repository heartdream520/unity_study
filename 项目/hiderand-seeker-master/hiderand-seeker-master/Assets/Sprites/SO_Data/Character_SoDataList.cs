using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character_SoData",menuName = "SO_Data/CharacterList_So")]
public class Character_SoDataList : ScriptableObject
{
   public List<Character_SoData> characterList;

    public Character_SoData GetCharacter(string s)
    {
        return characterList.Find(x => x.name == s);
    }
    public int GetCount()
    {
        return characterList.Count;
    }
    private int beforeRander;
    public Character_SoData GetOneRander()
    {
        int x;
        do
        {
            x = MyRandom.RangeInt(0, GetCount() - 1);

        } while (x == beforeRander);
        beforeRander = x;
        return characterList[x];
    }
}
[Serializable]
public class Character_SoData
{
    public string name;
    public GameObject prefabs;

    public Sprite shopImageSprite;
    //¼ÛÇ®
    public int price;
}