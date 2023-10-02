using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    public Dictionary<string, Queue<GameObject>> objectPool;
    public Dictionary<string,GameObject> objectParent;
    private void Start()
    {
        objectPool = new Dictionary<string, Queue<GameObject>>();
        objectParent = new Dictionary<string, GameObject>();
    }
    private GameObject TryGetOneObjectFromPool(string name)
    {
        if (!objectPool.ContainsKey(name) || objectPool[name].Count == 0)
        {
            return null;
        }
        GameObject go;
        do
        {
            go = objectPool[name].Dequeue();
        } while (go == null && objectPool[name].Count != 0);
        if (go != null)
            go.SetActive(true);
        return go;
    }
    public GameObject GetOneObjectFromPool(GameObject go)
    {
        string name = go.name;
        var g = TryGetOneObjectFromPool(name);
        if(g==null)
        {
            g = GameObject.Instantiate(go);
            g.name = name;
        }
        g.transform.localPosition = Vector3.zero;
        return g;
    }
    /// <summary>
    /// time秒后将 go 加入对象池
    /// </summary>
    public void SetOneObjectInPool(GameObject go,float time=0)
    {
        if (go == null) return;
        string name = go.name;
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            if (!objectPool.ContainsKey(name))
            {
                objectPool.Add(name, new Queue<GameObject>());
                var g = new GameObject(name + "s");
                g.transform.SetParent(transform, false);
                objectParent.Add(name, g);
            }
            if (go == null) return;
            objectPool[name].Enqueue(go);
            go.transform.SetParent(objectParent[name].transform);
            go.SetActive(false);
        }, time);
        
    }
}
