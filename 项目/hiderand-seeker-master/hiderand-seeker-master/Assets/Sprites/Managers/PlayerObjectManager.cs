using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectManager : Singleton<PlayerObjectManager>
{
    struct player_PosNameStruct
    {
        public GameObject GameObject;
        public string posName;
    }
    private Dictionary<player_PosNameStruct, GameObject> player_PosNameGameObjectDic;
    private Dictionary<string, GameObject> pathGameObjcetPrefabsDic;
    public void Init()
    {
        EventManager.Instance.OneGameEndAction += this.OnOneGameEnd;
        player_PosNameGameObjectDic = new Dictionary<player_PosNameStruct, GameObject>();
        pathGameObjcetPrefabsDic = new Dictionary<string, GameObject>();
    }
    public GameObject CreateOneGameObjectInObjectPosName(GameObject go,string prefabsPath,string posName)
    {
        GameObject pos;
        player_PosNameStruct @struct;
        @struct.GameObject = go;
        @struct.posName = posName;
        if (!player_PosNameGameObjectDic.TryGetValue(@struct, out pos))
        {
            pos = MyTools.BfsGetObjectPosNameGameObject(go, posName);
            player_PosNameGameObjectDic.Add(@struct,pos);
        }
        GameObject prefabs;
        if(!pathGameObjcetPrefabsDic.TryGetValue(prefabsPath,out prefabs))
        {
            prefabs = Resloader.LoadGameObject(prefabsPath);
            pathGameObjcetPrefabsDic.Add(prefabsPath, prefabs);
        }
        var prefabsObject = ObjectPoolManager.Instance.GetOneObjectFromPool(prefabs);

        Quaternion savedRotation = prefabsObject.transform.localRotation;
        prefabsObject.transform.SetParent(pos.transform,false);
        prefabsObject.transform.localRotation = savedRotation;
        prefabsObject.transform.localPosition = Vector3.zero;
        prefabsObject.transform.localRotation = Quaternion.identity;

        return prefabsObject;
    }

    public void SetScaleFromGoToEnd(GameObject go, GameObject endGo)
    {

        if (go == endGo) return;
        GameObject now = go.transform.parent.gameObject;
        while (true)
        {
            Vector3 a = go.transform.localScale;
            Vector3 b = now.transform.localScale;
            go.transform.localScale = new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
            if (now == endGo) return;
            now = now.transform.parent.gameObject;
        }
    }
    public void OnDestory()
    {
        EventManager.Instance.OneGameEndAction -= this.OnOneGameEnd;
    }
    private void OnOneGameEnd(bool iswin)
    {
        player_PosNameGameObjectDic.Clear();
    }
}
