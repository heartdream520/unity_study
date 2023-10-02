using System;
using System.Collections;
using UnityEngine;

public class MenHingeJointCollision : MonoBehaviour
{
    private HingeJoint hingeJoint__;
    private float originalAngle;
    private float originalAngleMax;
    private float originalAngleMin;
    private float beginAngle;

    private float changeAngle;
    private float nowAngles=> this.transform.eulerAngles.y;
    Coroutine coroutine;
    private void Start()
    {
        hingeJoint__ = GetComponent<HingeJoint>();
        originalAngle = hingeJoint__.angle;
        originalAngleMax = hingeJoint__.limits.max;
        originalAngleMin = hingeJoint__.limits.min;
        beginAngle = nowAngles;
        SetLayer(GameDefine.Men_Layer);

    }
    private void Update()
    {
        if(Mathf.Abs( this.transform.eulerAngles.y-beginAngle)>=75f)
        {
            this.SetLayer(GameDefine.NotCollisionWithPlayer_Layer);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        // 当发生碰撞时，记录当前角度
        originalAngle = hingeJoint__.angle;
        if(coroutine!=null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 当碰撞结束时，将物体的角度恢复到原来的位置
        JointMotor motor = hingeJoint__.motor;
        motor.targetVelocity = 0f;
        motor.force = 0f;
        hingeJoint__.motor = motor;

        changeAngle = MathF.Abs(nowAngles - beginAngle);
        coroutine = StartCoroutine(AfterExit());
    }

    IEnumerator AfterExit()
    {
        JointLimits limits = hingeJoint__.limits;

        while (changeAngle >= 5f)
        {
            limits.max = changeAngle + 2f;
            limits.min = -changeAngle - 2f;
            while (MathF.Abs(nowAngles - beginAngle) > 1f)
            {
                yield return null;
            }
            changeAngle /= 3;

        }
        
       
            
        while (Mathf.Abs(nowAngles - this.beginAngle) >= 5)
        {
            yield return null;
        }

        SetLayer(GameDefine.Men_Layer);

        limits.min = originalAngle;
        limits.max = originalAngle;
        hingeJoint__.limits = limits;
        yield return new WaitForEndOfFrame();


        limits.min = originalAngleMax;
        limits.max = originalAngleMin;

        yield return new WaitForSeconds(0.2f);
        hingeJoint__.limits = limits;
    }
    private void SetLayer(string layer)
    {
        this.gameObject.layer = LayerMask.NameToLayer(layer);
    }

}
