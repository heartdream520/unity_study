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

        //如果Unity没有这个，我们如何渲染出深度图呢？ 确实没有Normal，应该去尝试渲染Normal
        //x_ray_Camera.RenderWithShader();
    }

    protected override void SetMaterialData()
    {
        Material.SetTexture("_XRayDepthTexture", x_ray_dpeth);
        Material.SetMatrix("_ViewToWorld", Cam.cameraToWorldMatrix);
    }
}
