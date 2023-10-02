using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowJianCe : MonoBehaviour
{

    private void OnEnable()
    {
        this.transform.rotation = Quaternion.identity;
    }
    public Action<Collider> OnTriggerEnterAction;
    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterAction?.Invoke(other);
    }
    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnterAction?.Invoke(other);
    }
}
