using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingSlider : MonoBehaviour
{
    public Slider slider;
    public void StartLoading(float loadTime,UnityAction action=null)
    {
        StartCoroutine(Loading(loadTime,action));
    }

    private IEnumerator Loading(float loadTime, UnityAction action=null)
    {
        while(true)
        {
            yield return null;
        }
    }
}
