using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneGameEndCharacterModleManager : MonoSingleton<OneGameEndCharacterModleManager>
{
    private GameObject nowActiveModle;
    public ParticleSystem yanhua;
    Coroutine coroutine;

    private void Start()
    {
        EventManager.Instance.BeforeTimeBeginFlowAction += this.OnBeforeTimeBeginFlow;
    }
    private void OnDestroy()
    {
        EventManager.Instance.BeforeTimeBeginFlowAction -= this.OnBeforeTimeBeginFlow;

    }
    private void OnBeforeTimeBeginFlow()
    {
        beforeModleName = "";
        beforeStatusString = "";
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
    GameObject cry;
    string beforeModleName;
    string beforeStatusString;

    public void SetActiveModleWithStatus(string modleName, string statusString)
    {
        if (beforeModleName == modleName && beforeStatusString == statusString)
            return;
        beforeModleName = modleName;
        beforeStatusString = statusString;
        if (cry!=null)
        {
            cry.SetActive(false);
            cry = null;
        }

        if (nowActiveModle != null)
        {
            nowActiveModle.SetActive(false);
        }
        if(statusString=="win")
        {
            yanhua.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(setYanHua());
        }
        else
        {
            yanhua.gameObject.SetActive(false);
        }
        
        nowActiveModle = MyTools.BfsGetObjectPosNameGameObject(this.gameObject, modleName);
        if (GameingMainManager.Instance.player_Identity_Enum 
            == MyEnum.Player_Identity_Enum.Hider)
        {
            cry = MyTools.BfsGetObjectPosNameGameObject(nowActiveModle.gameObject, "Cry");
           if (statusString == "win")
            cry.SetActive(false);
           else cry.SetActive(true);
        }
       
        Animator animator = MyTools.BfsGetComponent<Animator>(nowActiveModle);
        nowActiveModle.SetActive(true);
        animator.SetTrigger(statusString);
        
    }
    IEnumerator setYanHua()
    {
        while(true)
        {
            yanhua.Play();
            yield return new WaitForSeconds(4f);
        }
    }
}
