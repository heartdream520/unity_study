
using System.Collections.Generic;
using UnityEngine;
public static class MyTools
{
    public static T BfsGetComponent<T> (GameObject root) where T :Component
    {
        Queue<GameObject> q = new Queue<GameObject>();
        q.Enqueue(root);
        T t = null;
        while(q.Count!=0)
        {
            var x = q.Dequeue();
            if (x.TryGetComponent(out t))
            {
                return t;
            }
            for(int i= 0;i<x.transform.childCount;i++)
            {
                q.Enqueue(x.transform.GetChild(i).gameObject);
            }
        }
        Debug.LogError($"{root.name} �²����������{typeof(T)}");
        return null;
    }

    public static int GetAnimatorLayerIdByLayerName( Animator animator,string layerName)
    {
        if (animator == null) return 0;
        for(int i=0;i<animator.layerCount;i++)
        {
            if(animator.GetLayerName(i)==layerName)
            {
                return i;
            }
        }
        Debug.LogError($"{animator.name}��animator�������㼶��{layerName}");
        return -1;
    }

    public static GameObject BfsGetObjectPosNameGameObject(GameObject root, string posName)
    {
        Queue<GameObject> q = new Queue<GameObject>();
        q.Enqueue(root);

        while (q.Count != 0)
        {
            var x = q.Dequeue();
            if(x.name==posName)
            {
                return x;
            }
            for (int i = 0; i < x.transform.childCount; i++)
            {
                q.Enqueue(x.transform.GetChild(i).gameObject);
            }
        }
        Debug.LogError($"{root.name} �²����� ����Ϊ{ posName }��������");
        return null;
    }
}