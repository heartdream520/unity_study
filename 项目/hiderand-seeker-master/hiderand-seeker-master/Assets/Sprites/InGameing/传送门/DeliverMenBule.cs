using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverMenBule : DeliverMenBase
{
    private void Awake()
    {
        GetOther();
}
    public override void GetOther()
    {
        otherDeliverMen = GameObject.Find("ºì´«ËÍÃÅ").GetComponent<DeliverMenBase>();
        base.GetOther();
    }
}
