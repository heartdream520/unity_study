using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuoWei : MonoBehaviour
{
    public Transform playerTransform;
    /// <summary>
    /// Æ«ÒÆÏòÁ¿
    /// </summary>
    public Vector3 pianyiVector;
    private void Awake()
    {
        playerTransform = null;
    }
    public void InitTuoWei(Transform transform)
    {
        playerTransform = transform;
        this.transform.position = playerTransform.position;
    }
    private void OnEnable()
    {
        if (playerTransform != null)
        {
            this.transform.position = playerTransform.position;
        }
    }
    private void Update()
    {
        if(playerTransform!=null)
        {
            var x = playerTransform.position;
            this.transform.localPosition = pianyiVector.x * playerTransform.right +
                                           pianyiVector.y * playerTransform.up +
                                           pianyiVector.z * playerTransform.forward;

        }
    }
}
