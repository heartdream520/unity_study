using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerPlayerinput :CharacterInputBase
{
    public Seeker seeker;
    public Transform transform => seeker.transform;

    
    public SeekerPlayerinput(Seeker seeker) : base()
    {
        EventManager.Instance.TimeOutGameBeginAction += this.OnTimeOutGameBegin;
        this.seeker = seeker;
        CanInput = false;
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
           
        
        
        /*
        if (vector.magnitude >= 0.1f)
        {
            Vector3 direction = new Vector3(vector.x, 0f, vector.y).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            //if (vector.x > 0.1f && vector.y > 0.1f)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, statusControl.seeker.rotationSpeed * Time.deltaTime);

            statusControl.SetStatus(MyEnum.SeekerStatusEnum.Run);
        }
        else
        {
            statusControl.SetStatus(MyEnum.SeekerStatusEnum.Idea);
        }
        */
    }
    public override void Input()
    {
        base.Input();
        Vector2 vector = PlayerInputManager.Instance.GetPlayerXYInput();
        seeker.InputXY = vector;
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
