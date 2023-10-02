
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class X_rayInspection : Singleton<X_rayInspection>
{


    public List<RaycastHit> Sector_Inspection
        (Vector3 position, Vector3 forward, float dis, 
        int lookAccurate, float lookAngle,
        float drawStartTime,LayerMask layerMask)

    {

        List<RaycastHit> hits = new List<RaycastHit>();
        //һ����ǰ������
        hits.AddRange(LookAround(position, forward, dis, 
            Quaternion.identity, Color.red, 
            drawStartTime, layerMask));


        //��һ����ȷ�ȾͶ������ԳƵ�����,ÿ�����߼н����ܽǶȳ��뾫��
        float subAngle = (lookAngle / 2) / lookAccurate;
        for (int i = 0; i < lookAccurate; i++)
        {
            hits.AddRange(LookAround(position, forward, dis, 
                Quaternion.Euler(0, -1 * subAngle * (i + 1), 0), Color.red,
                drawStartTime, layerMask));


            hits.AddRange(LookAround(position, forward, dis, 
                Quaternion.Euler(0, subAngle * (i + 1), 0), Color.red,
                drawStartTime, layerMask));
        }

        return hits;
    }
    private List<RaycastHit> LookAround
        (Vector3 position, Vector3 forward, float dis,
        Quaternion eulerAnger, Color DebugColor,
        float drawStartTime,LayerMask layerMask)

    {
        forward = eulerAnger * forward;
        Debug.DrawLine(position, position + forward.normalized * dis, DebugColor,drawStartTime);

        List<RaycastHit> hits = new List<RaycastHit>();
        RaycastHit hit;
        Physics.Raycast(position, forward, out hit, dis, layerMask);
        hits.Add(hit);
        return hits;
    }
    /// <summary>
    /// dir����dis��LayerMask���巵��true,���򷵻�false;
    /// </summary>
    /// <returns></returns>
    public bool RayToDown(Vector3 position,Vector3 dir, float dis,int layerMask)
    {
        RaycastHit hit;
        if (!Physics.Raycast(position, dir, 
            out hit, dis, layerMask))
        {
            return false;
        }
        return true;
    }
    public bool IsInXiePo(Vector3 position, Vector3 right, Vector3 forword, Vector3 dir, int layerMask)
    {
        RaycastHit hit;
        if (!Physics.Raycast(position, dir,
            out hit,100, layerMask))
        {
            return false;
        }
        var x = Vector3.Cross(right, hit.normal).normalized;
        if (Vector3.Angle(forword, x) > 5) return true;
        return false;
    }

}
