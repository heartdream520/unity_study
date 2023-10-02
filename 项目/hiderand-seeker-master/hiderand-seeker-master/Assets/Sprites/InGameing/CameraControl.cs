using MyEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraControl : MonoBehaviour
{
    private GameObject playerObject;
    private Transform playerTransform;
    Player_Identity_Enum playerIdentity;
    public float smoothSpeed = 0.125f; // 平滑跟踪的速度
    public float distance = 20f; // 相机距离目标的距离
    public float height = 30.0f; // 相机距离目标的高度
    private bool isSeekerAndBeforeGameTimeNotOut;
    public float eulerAngles = 54f;
    private void OnDisable()
    {
        if(playerIdentity == MyEnum.Player_Identity_Enum.Seeker)
        {
            EventManager.Instance.BeforeTimeBeginFlowAction -= OnSeekerBeforeTimeBeginflowAction;
            EventManager.Instance.TimeOutGameBeginAction -= OnSeekerTimeOutGameBeginAction;
        }
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.O))
        {
            float x = this.transform.eulerAngles.x;
            x++;
            Debug.Log(x);
            this.transform.eulerAngles = new Vector3(x, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            float x = this.transform.eulerAngles.x;
            x--;
            Debug.Log(x);
            this.transform.eulerAngles = new Vector3(x, 0, 0);
        }
        */

        if(TestManager.Instance.testMode)
        {
            distance = TestManager.Instance.cameraDistance;
            height = TestManager.Instance.cameraHigh;
            this.transform.eulerAngles = new Vector3(TestManager.Instance.cameraXAngles, 0, 0);
        }
    }
    internal void InitCameraAfterSelectedIdentity(Player_Identity_Enum identityEnum, GameObject go)
    {
        this.SetMode(identityEnum);
        SetPlayerGameObject(go);
        this.transform.eulerAngles = new Vector3(eulerAngles, 0, 0);
        //this.transform.position = this.GetNowPos();

        if (playerIdentity == MyEnum.Player_Identity_Enum.Seeker)
        {
            EventManager.Instance.BeforeTimeBeginFlowAction += OnSeekerBeforeTimeBeginflowAction;
            EventManager.Instance.TimeOutGameBeginAction += OnSeekerTimeOutGameBeginAction;
        }
    }

    private void OnSeekerBeforeTimeBeginflowAction()
    {
        isSeekerAndBeforeGameTimeNotOut = true;
    }
    private void OnSeekerTimeOutGameBeginAction()
    {
        isSeekerAndBeforeGameTimeNotOut = false;
    }

    internal void InitCameraBeforeSelectedIdentity()
    {
        this.transform.eulerAngles = new Vector3(eulerAngles, 0, 0);
        Vector3 centryPos = GameingMainManager.Instance.gameMapCenter.transform.position;
        Vector3 vector= centryPos - Vector3.forward * this.distance*2 + Vector3.up * height*2;
        this.transform.position = vector;
    }
    internal void SetMode(Player_Identity_Enum identityEnum)
    {
        playerIdentity = identityEnum;
    }

    internal void SetPlayerGameObject(GameObject go)
    {
        playerObject = go;
        playerTransform = go.transform;
    }
    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 desiredPosition;
            // 计算新的相机位置
            if (this.isSeekerAndBeforeGameTimeNotOut)
            {
                desiredPosition = GetIsSeekerTimeNotOutPos();
            }
            else
                desiredPosition = GetNowPos();
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 计算新的相机旋转角度
            //Quaternion desiredRotation = playerTransform.rotation;
            //Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);

            // 设置相机位置和旋转角度
            transform.position = smoothedPosition;
            //transform.rotation = smoothedRotation;
        }
    }
    private Vector3 GetNowPos()
    {
        return playerTransform.position - Vector3.forward * this.distance + Vector3.up * height;
    }
    private Vector3 GetIsSeekerTimeNotOutPos()
    {
        return playerTransform.position - Vector3.forward * this.distance/2 + Vector3.up * height/2;
    }


    
}
