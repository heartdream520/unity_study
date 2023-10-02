using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Text;
using System;
using System.IO;

using Newtonsoft.Json;
using System.Xml;

public class JsonDataManager : Singleton<JsonDataManager>
{
    public string DataPath;

    /// <summary>
    /// 地图字典
    /// </summary>
    /// 

    public Dictionary<int, TestDefine> Tests = new Dictionary<int, TestDefine>();
    public JsonDataManager()
    {
        this.DataPath = "Assets/JsonData/";
        Debug.Log("JsonDataManager -> JsonDataManager()");
    }
    /// <summary>
    /// 给服务端用的
    /// </summary>
    public void Load()
    {

        string json = File.ReadAllText(this.DataPath + "TestDefine.txt");
        this.Tests = JsonConvert.DeserializeObject<Dictionary<int, TestDefine>>(json);
        Debug.LogFormat("DataManager->TestDefine");

    }
    /// <summary>
    /// 给客户端用的
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadData()
    {
        Debug.Log("DataManager -> LoadData()");
        yield return null;
        string json = File.ReadAllText(this.DataPath + "TestDefine.txt");
        this.Tests = JsonConvert.DeserializeObject<Dictionary<int, TestDefine>>(json);
        Debug.Log("DataManager->TestDefine");

    }
}
