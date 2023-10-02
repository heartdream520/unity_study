using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScanEffect : PostEffectsBase
{
    public float startScanRange = 0;
    public float maxScanRange = 20;
    public float scanWidth = 3;
    public float scanSpeed = 1;
    public Color headColor;
    public Color trailColor;

    private bool isInScan = false;
    private Vector3 centerPos;
    private float scanRadius;
    private IEnumerator scanHandler = null;

    void OnEnable()
    {
        scanRadius = startScanRange;
        Cam.depthTextureMode = DepthTextureMode.Depth;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                centerPos = hit.point;
                if (scanRadius <= startScanRange)
                {
                    Scan();
                }
                else
                {
                    ScanBack();
                }
            }
        }
    }

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Material != null && isInScan)
        {
            Material.SetVector("_ScanCenterPos", centerPos);
            Material.SetFloat("_ScanRadius", scanRadius);
            Material.SetFloat("_ScanWidth", scanWidth);
            Material.SetColor("_HeadColor", headColor);
            Material.SetColor("_TrailColor", trailColor);

            RaycastCornerBlit(source, destination, Material);
        }
        else 
        {
            Graphics.Blit(source, destination);
        }
    }

    void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
    {
        float CameraFar = Cam.farClipPlane;
        float CameraFov = Cam.fieldOfView;
        float CameraAspect = Cam.aspect;

        float fovWHalf = CameraFov * 0.5f;

        Vector3 toRight = Cam.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * CameraAspect;
        Vector3 toTop = Cam.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

        Vector3 topLeft = (Cam.transform.forward - toRight + toTop);
        float CameraScale = topLeft.magnitude * CameraFar;

        topLeft.Normalize();
        topLeft *= CameraScale;

        Vector3 topRight = (Cam.transform.forward + toRight + toTop);
        topRight.Normalize();
        topRight *= CameraScale;

        Vector3 bottomRight = (Cam.transform.forward + toRight - toTop);
        bottomRight.Normalize();
        bottomRight *= CameraScale;

        Vector3 bottomLeft = (Cam.transform.forward - toRight - toTop);
        bottomLeft.Normalize();
        bottomLeft *= CameraScale;

        RenderTexture.active = dest;

        mat.SetTexture("_MainTex", source);

        GL.PushMatrix();
        GL.LoadOrtho();

        mat.SetPass(0);

        GL.Begin(GL.QUADS);

        GL.MultiTexCoord2(0, 0.0f, 0.0f);
        GL.MultiTexCoord(1, bottomLeft);
        GL.Vertex3(0.0f, 0.0f, 0.0f);

        GL.MultiTexCoord2(0, 1.0f, 0.0f);
        GL.MultiTexCoord(1, bottomRight);
        GL.Vertex3(1.0f, 0.0f, 0.0f);

        GL.MultiTexCoord2(0, 1.0f, 1.0f);
        GL.MultiTexCoord(1, topRight);
        GL.Vertex3(1.0f, 1.0f, 0.0f);

        GL.MultiTexCoord2(0, 0.0f, 1.0f);
        GL.MultiTexCoord(1, topLeft);
        GL.Vertex3(0.0f, 1.0f, 0.0f);

        GL.End();
        GL.PopMatrix();
    }

    void CheckAndBlock()
    {
        if (scanHandler != null)
        {
            StopCoroutine(scanHandler);
        }
    }

    void Scan()
    {
        CheckAndBlock();
        scanHandler = ScanCoroutine();
        StartCoroutine(scanHandler);
    }

    void ScanBack()
    {
        CheckAndBlock();
        scanHandler = ScanBackCoroutine();
        StartCoroutine(scanHandler);
    }

    private IEnumerator ScanCoroutine()
    {
        isInScan = true;
        while (scanRadius < maxScanRange) 
        {
            scanRadius += scanSpeed;
            yield return new WaitForSecondsRealtime(.01f);
        }
        scanRadius = maxScanRange;
        isInScan = false;
    }

    private IEnumerator ScanBackCoroutine()
    {
        isInScan = true;
        while (scanRadius > startScanRange)
        {
            scanRadius -= scanSpeed;
            yield return new WaitForSecondsRealtime(.01f);
        }
        scanRadius = startScanRange;
        isInScan = false;
    }
}
