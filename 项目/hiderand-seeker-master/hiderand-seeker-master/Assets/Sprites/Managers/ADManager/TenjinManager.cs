using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenjinManager : MonoSingleton<TenjinManager>
{
    BaseTenjin baseTenjin;
    private void Start()
    {
        TenjinConnect();
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            TenjinConnect();
        }
    }

    public void TenjinConnect()
    {
        baseTenjin = Tenjin.getInstance("HJNVUOAPZKVFHZ1APSY7BYY1SZPMNMRT");

        baseTenjin.SetAppStoreType(AppStoreType.googleplay);
        
        baseTenjin.Connect();
    }
    public void SendFirstInFirstLevel()
    {
        int flag = PlayerPrefs.GetInt("TenjinFirstInFirstLevel", -1);
        if (flag == -1)
        {

            PlayerPrefs.SetInt("TenjinFirstInFirstLevel", 1);
            baseTenjin.SendEvent("FirstInLevel");

            Debug.Log("Tenjin -> Is Entering the first level for the first time");
        }
     /*   else
        {
            baseTenjin.SendEvent("NotFirstInLevel");
            Debug.Log("Tenjin -> Not Is Entering the first level for the first time");

        }*/
    }

}
