using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsManager :Singleton<MaterialsManager>
{
    public Dictionary<GameObject, SkinnedMeshRenderer> objectMaterialDic;
    public Dictionary<string, Material> pathMteriaclDic;
    public void Init()
    {
        EventManager.Instance.OneGameEndAction += this.OnOneGameEnd;
        objectMaterialDic = new Dictionary<GameObject, SkinnedMeshRenderer>();
        pathMteriaclDic = new Dictionary<string, Material>();
    }
    public void OnDestory()
    {
        EventManager.Instance.OneGameEndAction -= this.OnOneGameEnd;
    }
    private void OnOneGameEnd(bool iswin)
    {

    }
    public void ChangeObjectMaterial(GameObject go, string MaterPath)
    {
        var renderer = GetSkinnedMeshRenderer(go);

        renderer.material = GetNeedMaterial(MaterPath);

    }


    private SkinnedMeshRenderer GetSkinnedMeshRenderer(GameObject go)
    {
        SkinnedMeshRenderer renderer = null;
        if (!objectMaterialDic.TryGetValue(go, out renderer))
        {
            renderer = MyTools.BfsGetComponent<SkinnedMeshRenderer>(go);
            objectMaterialDic.Add(go, renderer);
        }
        return renderer;

    }
    private Material GetNeedMaterial(string materPath)
    {
        Material material = null;
        if (!pathMteriaclDic.TryGetValue(materPath, out material))
        {
            material = Resloader.Load<Material>(materPath);
            pathMteriaclDic.Add(materPath, material);
        }
        return material;
    }

}
