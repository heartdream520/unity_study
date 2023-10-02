using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUICharacterModleManager : MonoSingleton<MainUICharacterModleManager>
{
    private GameObject nowActiveModle;

    public void SetActiveModle(string modleName)
    {
        if (nowActiveModle != null)
        {
            nowActiveModle.SetActive(false);
        }
        nowActiveModle = MyTools.BfsGetObjectPosNameGameObject(this.gameObject, modleName);
        nowActiveModle.SetActive(true);
    }
}
