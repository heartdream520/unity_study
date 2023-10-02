using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveObjectManager : Singleton<ReviveObjectManager>
{

    public void ReviveOneObjectAfterSecend(GameObject go, float time)
    {
        go.SetActive(false);
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            if(go!=null)
            go.SetActive(true);
        }, time);
    }

}