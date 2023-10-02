
using System;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SeekerAIInput :CharacterInputBase
{
    public NavMeshAgent navMeshAgent;
 
    public Seeker seeker;


    /// <summary>
    /// 暂停追击的时间
    /// </summary>
    private float stopTime;

    /// <summary>
    /// 追击剩余时间
    /// </summary>
    private float runToLastTime;
    /// <summary>
    /// 上一个追击对象
    /// </summary>
    private int runToHiderId;
    public SeekerAIInput(Seeker seeker) : base()
    {
        EventManager.Instance.TimeOutGameBeginAction += this.OnTimeOutGameBegin;
        this.seeker = seeker;
        CanInput = false;
        //findOneHider = false;

        //findHiderTransform = null;


        navMeshAgent = seeker.AddComponent<NavMeshAgent>();
        navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
        //navMeshAgent.speed = 0;
        navMeshAgent.updateRotation = false;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        navMeshAgent.radius = GameDefine.NavMeshAgent_Radius;

        runToHiderId = 1;
        runToLastTime = 2f;
    }
    public override void OnOneGameEnd()
    {
        base.OnOneGameEnd();
        EventManager.Instance.TimeOutGameBeginAction -= this.OnTimeOutGameBegin;
    }

    private void OnTimeOutGameBegin()
    {
        CanInput = true;
    }

    public override void UpData()
    {
        base.UpData();
    }
    public override void Input()
    {
        base.Input();

        if(stopTime>0)
        {
            stopTime -= Time.deltaTime;
            seeker.InputXY = Vector2.zero;
            navMeshAgent.speed = 0;
            return;
        }

        //已经被攻击 或者 追击时间过去了 
        if (GameingMainManager.Instance.gameingManager.hiders[runToHiderId].gameObject.layer
            ==LayerMask.NameToLayer( GameDefine.CanNotAttack_Layer)||
            runToLastTime<=0)
        {
            GetOneRunToHider();
        }
        //navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
        if (runToLastTime>0)
        {
            runToLastTime -= Time.deltaTime;
            navMeshAgent.SetDestination(GameingMainManager.Instance.gameingManager.hiders[runToHiderId].transform.position);
            
            Vector3 vector3 = navMeshAgent.velocity.normalized;
            Vector2 vector2 = new Vector2(vector3.x, vector3.z);
            seeker.InputXY = vector2.normalized;

        }
        

    }
    /// <summary>
    /// 获取一个追击对象
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void GetOneRunToHider()
    {


        navMeshAgent.enabled = false;
        navMeshAgent.enabled = true;


        int id = runToHiderId;

        bool hasNotBeenAttackHider = false;
        for (int i = 0; i < GameDefine.GameHiderCount; i++)
        {
            if (GameingMainManager.Instance.gameingManager.hiders[i].gameObject.layer
            != LayerMask.NameToLayer(GameDefine.CanNotAttack_Layer))
            {
                hasNotBeenAttackHider = true;
                break;
            }
        }
        if (hasNotBeenAttackHider)
        {
            if (TestManager.Instance.testMode && TestManager.Instance.seekerToYourSelf)
            {
                id = 0;
            }
            else
                do
                {
                    id = MyRandom.RangeInt(0, GameDefine.GameHiderCount - 1);
                } while (GameingMainManager.Instance.gameingManager.hiders[id].gameObject.layer
                    == LayerMask.NameToLayer(GameDefine.CanNotAttack_Layer));
        }
        else
        {
            runToLastTime = 0;
            stopTime = 1f;
            return;
        }


        if (TestManager.Instance.testMode && TestManager.Instance.seekerToYourSelf)
            stopTime = 0;
        else
            stopTime = MyRandom.RangeFloat(1, 3);


        if (id != 0)
            runToLastTime = MyRandom.RangeFloat(2, 4);
        else runToLastTime = MyRandom.RangeFloat(3, 6);
        this.runToHiderId = id;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
    }

    public override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }
}
