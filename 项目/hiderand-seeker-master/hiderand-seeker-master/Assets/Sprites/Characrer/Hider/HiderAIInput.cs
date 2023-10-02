using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HiderAIInput: CharacterInputBase
{
    public Hider hider;
    public NavMeshAgent navMeshAgent;
    /// <summary>
    /// 当前场景的导航点的位置
    /// </summary>
    private List<Transform> hiderCruisePointPositions;
    private int hiderCruisePointPositionCount;
    /// <summary>
    /// 暂停移动的时间
    /// </summary>
    private float stopTime;
    private float hasMoveTime;
    private int nowCruisePointId;
    private float navMeshAgentStoppingDistance;

    private bool isfirst = true;

    public HiderAIInput(Hider hider) : base()
    {
        this.hider = hider;
        navMeshAgent = hider.AddComponent<NavMeshAgent>();
        navMeshAgent.speed= GameDefine.NavMeshAgentSpeed;
        navMeshAgent.updateRotation= false;
        //navMeshAgent.angularSpeed = 40f;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        navMeshAgent.radius = GameDefine.NavMeshAgent_Radius;
        navMeshAgent.agentTypeID = NavMesh.GetAreaFromName("Hider");


        hiderCruisePointPositions = new List<Transform>();
        foreach (var x in GameObject.FindGameObjectsWithTag(GameDefine.HiderCruisePointTag))
        {
            hiderCruisePointPositions.Add(x.transform);
        }
        hiderCruisePointPositionCount = this.hiderCruisePointPositions.Count;
        stopTime = 0f;
        hasMoveTime = 0f;
        nowCruisePointId = -1;
    }

    public override void OnOneGameEnd()
    {
        base.OnOneGameEnd();
    }

    public override void UpData()
    {
        base.UpData();
    }
    public override void Input()
    {
        base.Input();
        if (stopTime > 0)
        {
            stopTime -= Time.deltaTime;
            hider.InputXY = Vector2.zero;

                        navMeshAgent.speed = 0;


            navMeshAgent.enabled = false;


            return;
        }

        navMeshAgent.enabled = true;

        if (nowCruisePointId == -1)
        {

            //navMeshAgent.enabled = false;
            //navMeshAgent.enabled = true;

            nowCruisePointId = MyRandom.RangeInt(0, hiderCruisePointPositionCount - 1);
            navMeshAgentStoppingDistance = MyRandom.RangeFloat(0.5f, 2f);
            this.navMeshAgent.stoppingDistance = navMeshAgentStoppingDistance;
            this.navMeshAgent.SetDestination(this.hiderCruisePointPositions[nowCruisePointId].position);
            
        }

                
                //navMeshAgent.speed = hider.NowSpeed;

        hasMoveTime += Time.deltaTime;
        //Debug.Log("remainingDistance:" + navMeshAgent.remainingDistance);

        Vector3 vector3 = navMeshAgent.velocity.normalized;

        hider.InputXY = new Vector2(vector3.x, vector3.z).normalized ;



        if(navMeshAgent.remainingDistance == 0f)
        {
            if(isfirst)
            {
                isfirst = false;
            }
            else
            {
               // Debug.LogError("HiderAIInput->Invalid path! Cannot reach the target.");
                //Debug.LogError("不合法的点位名：" + this.hiderCruisePointPositions[nowCruisePointId]);
                StopMove();
            }
            
        }
        if (navMeshAgent.remainingDistance < navMeshAgentStoppingDistance + 0.1f)
        {
            StopMove();
        }
        
    }
    /// <summary>
    /// 停止移动一段时间
    /// </summary>
    public void StopMove()
    {


       


        //Debug.LogWarning(navMeshAgent.remainingDistance.ToString());
        this.stopTime = Mathf.Max(0.5f, hasMoveTime / 3f);
        this.stopTime = Mathf.Min(2f, hasMoveTime / 3f);
        hasMoveTime = 0;
        nowCruisePointId = -1;

                navMeshAgent.speed = 0;

       
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
