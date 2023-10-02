using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayZhuJian : MonoBehaviour
{
    public float beforePlayTime;
    public float playingTime=0.2f;
    Coroutine coroutine;
    Vector3 scale => this.transform.localScale;
    private void Start()
    {
        OnEnable();
    }
    private void OnEnable()
    {
        //Debug.Log("OnEnable");
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(UIPlayIEnumerator());
    }
    IEnumerator UIPlayIEnumerator()
    {
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(beforePlayTime);
        float nowScale = 0;
        float speed = 1f / playingTime;
        while (nowScale < 1)
        {
            nowScale += Time.deltaTime * speed;
            if (nowScale > 1) nowScale = 1;
            transform.localScale = new Vector3(nowScale, nowScale, nowScale);
            yield return new WaitForEndOfFrame();
        }
    }
}
