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
        otherDeliverMen = GameObject.Find("�촫����").GetComponent<DeliverMenBase>();
        base.GetOther();
    }
}
