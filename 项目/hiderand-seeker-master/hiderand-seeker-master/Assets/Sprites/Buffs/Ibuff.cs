using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff 
{
    public void OnBuffBegin();
    public void OnBuffUpdata();
    public void OnBuffEnd();
}
