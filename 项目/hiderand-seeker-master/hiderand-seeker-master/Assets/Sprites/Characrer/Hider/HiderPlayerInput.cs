using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HiderPlayerInput :CharacterInputBase
{
    public Hider hider;
    public Transform transform => hider.transform;

    public HiderPlayerInput(Hider hider) :base()
    {
       
        this.hider = hider;
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
        Vector2 vector = PlayerInputManager.Instance.GetPlayerXYInput();
        hider.InputXY = vector;
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
