
using System.Collections.Generic;
using UnityEngine;

public class GameMapCenter : MonoBehaviour
{
    public Transform hiderTransform;
    public Transform seekerTransform;

    public GameObject GoThroughtWallssObjectParent;

    private float detectionL = 4f;
    private float detectionR = 5f;
    private LayerMask detectionLayer;
    private float RangeLength = 4f;
    private List<Vector3> nowPositions = new List<Vector3>();


    private void Awake()
    {
        GameingMainManager.Instance.GameMapCenter = this;
        nowPositions.Add(this.transform.position);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            seekerTransform.gameObject.SetActive(!seekerTransform.gameObject.activeSelf);
        }

        /*
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SetHiderBeenAttack(1);
        }if(Input.GetKeyDown(KeyCode.F2))
        {
            SetHiderBeenAttack(2);
        }if(Input.GetKeyDown(KeyCode.F3))
        {
            SetHiderBeenAttack(3);
        }if(Input.GetKeyDown(KeyCode.F4))
        {
            SetHiderBeenAttack(4);
        }
        */
    }
    private void SetHiderBeenAttack(int i)
    {
        Hider hider = GameingMainManager.Instance.gameingManager.hiders[i];
        hider.AfterGetAttack();

        AudioManager.Instance.PlaySeekerAttack();
        //是AIInput立刻停止
        if (hider.isAIInput)
        {
            hider.aiInput.navMeshAgent.speed = 0;
        }
        else
        {
            hider.statusControl.SetStatus(MyEnum.HiderStatusEnum.BeenAttack);
        }
    }
    internal Vector3 GetOneHiderPositon()
    {
        int cnt = 0;
        Vector3 thisPosition = this.transform.position;
        while (true)
        {
            cnt++;
            if (cnt >= 100)
            {
                cnt = 0;
                detectionL -=1;
            }
            float x = Random.Range(-RangeLength, RangeLength);
            float z = Random.Range(-RangeLength, RangeLength);
            Vector3 vector3 = new Vector3(thisPosition.x + x, thisPosition.y, thisPosition.z + z);

            if (!Judging_Surrounding_Delusions(vector3))
            {
                while(!X_rayInspection.Instance.RayToDown(vector3,Vector3.down,0.2f,1<<LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
                {
                    vector3.y -= 0.1f;
                    //Debug.Log(vector3);
                }
                nowPositions.Add(vector3);
                return vector3;
            }

        }
    }

    public bool Judging_Surrounding_Delusions(Vector3 center)
    {
        foreach (var pos in this.nowPositions)
        {
            float len = Vector3.Distance(center, pos);
            if (!(len >= detectionL && len <= 100)) return true;
        }
        return false;
    }
    internal Vector3 GetOneSeekerPositon()
    {
        return this.transform.position;
    }

    internal float GetOneRotation()
    {
        return Random.Range(0, 360f);
    }

    /// <summary>
    /// 判断周围某个范围内有没有物体
    /// </summary>
    /// <returns>存在返回true 不存在返回false</returns>
    /*
    public bool Judging_Surrounding_Delusions(Vector3 center)
    {

        //center.y += 100f;
        // 在 center 位置周围创建一个检测范围，检测半径为 detectionRadius
        Collider[] colliders = Physics.OverlapSphere(center, detectionRadius, detectionLayer);

        // 判断 colliders 数组是否为空，如果不为空表示周围有物体存在
        if (colliders.Length > 0)
        {

            // 进行相关逻辑处理
            foreach (var x in colliders)
            {
                Debug.Log("x.tag" + x.tag);
                if (x.tag == GameDefine.HiderTag || x.tag == GameDefine.SeekerTag)
                {
                    Debug.LogError(x.name);
                    Debug.Log("与hider或seeker碰撞");
                    return true;

                }
            }
        }
        return false;
    }
    */
}