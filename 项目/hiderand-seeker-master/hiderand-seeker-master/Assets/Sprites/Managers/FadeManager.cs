using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoSingleton<FadeManager>
{
    private bool isFadeing;
    public CanvasGroup canvasGroup;
    public float fadeTime = 0.3f;
    public override void OnStart()
    {
        base.OnStart();
        this.Init();
    }
    public void Init()
    {
        isFadeing = false;
        canvasGroup.blocksRaycasts = false;

    }
    public IEnumerator FadeTo0(float fadeTime = 0.3f)
    {
        yield return StartCoroutine(FadeCoroutine(0, fadeTime));
    }
    public IEnumerator FadeTo1()
    {
        yield return StartCoroutine(FadeCoroutine(1, fadeTime));
    }
    IEnumerator FadeCoroutine(float fadeTo, float fadeTime)
    {
        if (isFadeing)
        {
            yield break;
        }
        canvasGroup.blocksRaycasts = true;
        this.isFadeing = true;

        float lenght = Mathf.Abs(fadeTo - this.canvasGroup.alpha);
        float speed = lenght / fadeTime;

        while (!Mathf.Approximately(this.canvasGroup.alpha, fadeTo))
        {
            yield return this.canvasGroup.alpha
                = Mathf.MoveTowards(this.canvasGroup.alpha, fadeTo, speed * Time.deltaTime);
            //yield return new WaitForEndOfFrame();

        }

        canvasGroup.blocksRaycasts = false;
        this.isFadeing = false;
        yield return null;
    }
}
