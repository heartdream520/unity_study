using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIGameOverStarPanel : MonoBehaviour
{
    public GameObject[] stars;
    internal void SetStarFrowXToY(int from, int to)
    {
        StartCoroutine(SetStarTo3I());
    }
    public void SetStarTo0()
    {
        StartCoroutine(SetStarTo0I());
    }
    public void SetStarTo3()
    {
        StartCoroutine(SetStarTo3I());
    }
    IEnumerator SetStarTo3I()
    {
        for (int i = 0; i < 3; i++)
        {
            float scale = 0;
            var rectTransform = stars[i].GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(scale, scale, scale);
        }
        for (int i = 0; i < 3; i++)
        {
            AudioManager.Instance.PlayWinStar();
            float scale = 0;
            var rectTransform = stars[i].GetComponent<RectTransform>();
            while (scale < 1)
            {
                scale += 2f * Time.deltaTime;
                scale = Mathf.Min(scale, 1);
                rectTransform.localScale = new Vector3(scale, scale, scale);
                yield return new WaitForEndOfFrame();
            }

        }
    }
    IEnumerator SetStarTo0I()
    {
        for (int i = 0; i < 3; i++)
        {
            float scale = 1;
            var rectTransform = stars[i].GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(scale, scale, scale);
        }
        for (int i = 2; i >=0; i--)
        {
            AudioManager.Instance.PlayFailStar();
            float scale = 1;
            var rectTransform = stars[i].GetComponent<RectTransform>();
            while (scale > 0)
            {
                scale -= 2f * Time.deltaTime;
                scale = Mathf.Max(scale, 0);
                rectTransform.localScale = new Vector3(scale, scale, scale);
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
