using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoSingleton<TestManager>
{
    //  public float PlayerCageTime = 0.986f;

    // public bool AttackStop = true;

    //public float SeekerZhuiJiDistance = 3.5f;


    public bool testMode;
    public bool InitTest;

    public bool noAds;
    public bool noSuccessTime;
    [HideInInspector]
    public bool noPlayingUI;
    public bool noBgm;
    public bool seekerToYourSelf;


    public float SeekerPlayerInputBaseSpeed = 5f;
    public float SeekerAIInputBaseSpeed = 3f;
    public float HiderPlayerInputBaseSpeed = 5f;
    public float HiderAIInputBaseSpeed = 4f;

    public float cameraDistance = 20f;
    public float cameraHigh = 30f;
    public float cameraXAngles = 54f;

    public float helpOtherDaojishiTime = 3f;
    public int testLevelId=1;


    public override void OnAwake()
    {
        base.OnAwake();
        testMode = PlayerPrefsDataManager.GetBool("testMode", false);
        noAds = PlayerPrefsDataManager.GetBool("noAds", false);
        noSuccessTime = PlayerPrefsDataManager.GetBool("hasSuccessTime", false);
        noPlayingUI = PlayerPrefsDataManager.GetBool("noPlayingUI", false);
        noBgm = PlayerPrefsDataManager.GetBool("noBgm", false);
        seekerToYourSelf = PlayerPrefsDataManager.GetBool("seekerToYourSelf", false);

        SeekerPlayerInputBaseSpeed = PlayerPrefs.GetFloat("SeekerPlayerInputBaseSpeed", 5f);
        SeekerAIInputBaseSpeed = PlayerPrefs.GetFloat("SeekerAIInputBaseSpeed", 3f);
        HiderPlayerInputBaseSpeed = PlayerPrefs.GetFloat("HiderPlayerInputBaseSpeed", 5f);
        HiderAIInputBaseSpeed = PlayerPrefs.GetFloat("HiderAIInputBaseSpeed", 4f);


        cameraHigh = PlayerPrefs.GetFloat("cameraHigh", 30.0f);
        cameraDistance = PlayerPrefs.GetFloat("cameraDistance", 20.0f);
        cameraXAngles = PlayerPrefs.GetFloat("cameraXAngles", 54f);
        helpOtherDaojishiTime = PlayerPrefs.GetFloat("helpOtherDaojishiTime", 3f);

        testLevelId = PlayerPrefs.GetInt("testLevelId", 1);

        StartCoroutine(SaveTestSet());

    }

    /*
                1.场景和背景颜色的调整 
                2.摄像机视角的调整              +
                3.绑定跟踪目标                  +
                4.取消成功时间限制              +   
                5.关卡调整（金币、道具的增减 
                6.摆关                          
                7.去ui                          
                         
                8.角色速度调整                  +
                9.去bgm保留音效                 +
                10 解救人的时间自定义 （或者短一点）   +
                11.修改关卡
                12.设置路径点
    */

    private IEnumerator SaveTestSet()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            PlayerPrefsDataManager.SetBool("testMode", testMode);
            PlayerPrefsDataManager.SetBool("noAds", noAds);
            PlayerPrefsDataManager.SetBool("hasSuccessTime", noSuccessTime);
            PlayerPrefsDataManager.SetBool("noPlayingUI", noPlayingUI);
            PlayerPrefsDataManager.SetBool("noBgm", noBgm);
            PlayerPrefsDataManager.SetBool("seekerToYourSelf", seekerToYourSelf);


            PlayerPrefs.SetFloat("SeekerPlayerInputBaseSpeed", SeekerPlayerInputBaseSpeed);
            PlayerPrefs.SetFloat("SeekerAIInputBaseSpeed", SeekerAIInputBaseSpeed);
            PlayerPrefs.SetFloat("HiderPlayerInputBaseSpeed", HiderPlayerInputBaseSpeed);
            PlayerPrefs.SetFloat("HiderAIInputBaseSpeed", HiderAIInputBaseSpeed);


            PlayerPrefs.SetFloat("cameraHigh", cameraHigh);
            PlayerPrefs.SetFloat("cameraDistance", cameraDistance);
            PlayerPrefs.SetFloat("cameraXAngles", cameraXAngles);
            PlayerPrefs.SetFloat("helpOtherDaojishiTime", helpOtherDaojishiTime);
            PlayerPrefs.SetInt("testLevelId", testLevelId);
        }
    }
    private void Update()
    {
        if (InitTest)
        {
            InitTest = false;
            noAds = false;
            noSuccessTime = false;
            noPlayingUI = false;
            noBgm = false;
            seekerToYourSelf = false;


            SeekerPlayerInputBaseSpeed = 5f;
            SeekerAIInputBaseSpeed = 3f;
            HiderPlayerInputBaseSpeed = 5f;
            HiderAIInputBaseSpeed = 4f;

            cameraDistance = 20f;
            cameraHigh = 30f;
            cameraXAngles = 54f;

            helpOtherDaojishiTime = 3f;
            testLevelId = 1;
        }
    }
}
