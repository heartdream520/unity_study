using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldUISeekerDaoJiShi : UIWindows
{
    public Text timeText;
    public GameObject Panel;
    private void OnEnable()
    {
        EventManager.Instance.BeforGameBeginTimeChangeAction += this.OnBeforGameBeginTimeChange;
        EventManager.Instance.TimeOutGameBeginAction += this.OnTimeOutGameBegin;
    }
    private void OnDestroy()
    {
        EventManager.Instance.BeforGameBeginTimeChangeAction -= this.OnBeforGameBeginTimeChange;
        EventManager.Instance.TimeOutGameBeginAction -= this.OnTimeOutGameBegin;
    }
    private void Update()
    {
        if (Camera.main != null)
        {
            // 获取主摄像机的位置
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 toCameraDirection = cameraPosition - Panel.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(-toCameraDirection, Vector3.up);

            // 应用旋转到UI元素
            transform.rotation = lookRotation;
            // 让UI元素始终面向主摄像机
            //daoJiShiPanel.GetComponent<RectTransform>().LookAt(- cameraPosition, Vector3.up);

        }
    }
    private void OnBeforGameBeginTimeChange(float time)
    {
        timeText.text = time.ToString("0.0");
    }
    private void OnTimeOutGameBegin()
    {
        Destroy(gameObject, 0.3f);
    }
}
