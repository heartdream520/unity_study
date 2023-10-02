using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(other.gameObject.name + "进入水");
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.LogError(other.gameObject.name + "离开水");

    }
}
