using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class XRay : PostEffectsBase
{
    public Camera x_ray_Camera;

    private RenderTexture x_ray_dpeth;

    private void OnEnable()
    {
        Cam.depthTextureMode = DepthTextureMode.Depth;
        x_ray_Camera.depthTextureMode = DepthTextureMode.Depth;
        
        x_ray_dpeth = RenderTexture.GetTemporary(x_ray_Camera.pixelWidth, x_ray_Camera.pixelHeight, 0, RenderTextureFormat.Depth);
        x_ray_Camera.targetTexture = x_ray_dpeth;

        //���Unityû����������������Ⱦ�����ͼ�أ� ȷʵû��Normal��Ӧ��ȥ������ȾNormal
        //x_ray_Camera.RenderWithShader();
    }

    protected override void SetMaterialData()
    {
        Material.SetTexture("_XRayDepthTexture", x_ray_dpeth);
        Material.SetMatrix("_ViewToWorld", Cam.cameraToWorldMatrix);
    }
}
