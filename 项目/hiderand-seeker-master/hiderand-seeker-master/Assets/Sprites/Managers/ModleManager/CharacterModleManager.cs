using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModleManager : MonoSingleton<CharacterModleManager>
{
    private GameObject nowActiveModle;

    public void SetActiveModle(string modleName)
    {
        if(nowActiveModle!=null)
        {
            nowActiveModle.SetActive(false);
        }
        nowActiveModle = MyTools.BfsGetObjectPosNameGameObject(this.gameObject, modleName);
        nowActiveModle.SetActive(true);
    }
}
