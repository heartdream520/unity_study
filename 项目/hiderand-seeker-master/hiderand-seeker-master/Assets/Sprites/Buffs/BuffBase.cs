using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBase : IBuff
{
    
    public float buffLastTime = 5f;
    public BuffBase(float lastTime)
    {
        buffLastTime = lastTime;
    }
    public virtual void OnBuffBegin()
    {

    }

    public virtual void OnBuffEnd()
    {

    }

    public virtual void OnBuffUpdata()
    {

    }
}
