using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverMenRed : DeliverMenBase
{
    public override void GetOther()
    {
        otherDeliverMen = GameObject.Find("À¶´«ËÍÃÅ").GetComponent<DeliverMenBase>();
        base.GetOther();
    }

    private void Awake()
    {
        GetOther();
    }
}
