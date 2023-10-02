using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeManager : MonoSingleton<InvokeManager>
{
    public void InvokeOneActiveAfterSeconds(Action action,float seconds)
    {
        StartCoroutine(InvokeOneActiveAfterSecondsCoroutine(action, seconds));
    }

    internal void InvokeOneActionWhen(Action action, Func<bool> when)
    {
        StartCoroutine(InvokeOneActiveWhenCoroutine(action, when));
    }

    private IEnumerator InvokeOneActiveAfterSecondsCoroutine(Action action, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    } 
    private IEnumerator InvokeOneActiveWhenCoroutine(Action action, Func<bool> when)
    {
        while(!when())
        {
            yield return null;
        }
        action?.Invoke();
    }
}
