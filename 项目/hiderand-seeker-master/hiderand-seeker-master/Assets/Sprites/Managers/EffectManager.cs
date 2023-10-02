using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    /// <summary>
    /// ������Ч
    /// </summary>
    static List<GameObject> caini;
    /// <summary>
    /// ��������Ч
    /// </summary>
    static List<GameObject> cainihou;

    static List<GameObject> chuanSong;
    static GameObject jinbiObject;
    /// <summary>
    /// ����������
    /// </summary>
    static GameObject attcakYan;
    /// <summary>
    /// hider�ı�����
    /// </summary>
    static GameObject baoHuZhao;
    /// <summary>
    /// ���ٵ���
    /// </summary>
    static GameObject jiaSuDeYan;
    /// <summary>
    /// X����ɨ����Ч
    /// </summary>
    static GameObject x_RaySaoMiao;

    static GameObject throwObjectJianShe;
    /// <summary>
    /// �ʻ�
    /// </summary>
    static GameObject caiHuaObjectJianShe;

    static GameObject throwRengGuang;
    static EffectManager()
    {
        caini = new List<GameObject>();
        cainihou = new List<GameObject>();
        chuanSong = new List<GameObject>();
        for (int i = 1; i <= GameDefine.NiCount; i++)
        {
            caini.Add(Resloader.LoadGameObject(PathDefine.CaiNiEffectPath + i));
            cainihou.Add(Resloader.LoadGameObject(PathDefine.CaiNiHouEffectPath + i));
        }
        chuanSong.Add(Resloader.LoadGameObject(PathDefine.ChuanSongRedEffectObjectPath));
        chuanSong.Add(Resloader.LoadGameObject(PathDefine.ChuanSongBlueEffectObjectPath));

        jinbiObject = Resloader.LoadGameObject(PathDefine.JinBiObjectPath);
        attcakYan = Resloader.LoadGameObject(PathDefine.AttackYanEffectPath);

        baoHuZhao = Resloader.LoadGameObject(PathDefine.BaoHuZhaoEffectPath);
        jiaSuDeYan = Resloader.LoadGameObject(PathDefine.JiaSuDeYanEffectPath);

        x_RaySaoMiao= Resloader.LoadGameObject(PathDefine.x_RaySaoMiaoEffectPath);

        throwObjectJianShe= Resloader.LoadGameObject(PathDefine.ThrowJianSheEffectPath);

        caiHuaObjectJianShe = Resloader.LoadGameObject(PathDefine.CaiHuaEffectPath);
        throwRengGuang = Resloader.LoadGameObject(PathDefine.ThrowRengGuang);
    }
    /// <summary>
    ///  ����һ������ʱ����Ч
    /// </summary>
    public void CreateOneCaiNi(int id, Vector3 pos, float time = 2f)
    {
        id--;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(caini[id]);
        go.transform.position = pos;

        ObjectPoolManager.Instance.SetOneObjectInPool(go, time);
    }
    /// <summary>
    ///  ����һ����������ߵ���Ч
    /// </summary>
    public void CreateOneCaiNiHou(int id, Vector3 pos, float time = 2f)
    {
        id--;
        string name = cainihou[id].name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(cainihou[id]);
        go.transform.position = pos;

        ObjectPoolManager.Instance.SetOneObjectInPool(go, time);
    }
    public void CreateOneChuanSong(int id, Vector3 pos, float time = 2f)
    {
        string name = chuanSong[id].name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(chuanSong[id]);
        go.transform.position = pos;

        ObjectPoolManager.Instance.SetOneObjectInPool(go, time);
    }
    public void CreateOneJinBi(Vector3 pos, float time = 2f)
    {
        string name = jinbiObject.name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(jinbiObject);
        go.transform.position = pos;

        ObjectPoolManager.Instance.SetOneObjectInPool(go, time);
    }
    public void CreateAttackYan(Vector3 pos, float time = 3f)
    {
        string name = attcakYan.name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(attcakYan);
        go.transform.position = pos;

        ObjectPoolManager.Instance.SetOneObjectInPool(go, time);
    }
    /// <summary>
    /// ����һ����������Ч������
    /// </summary>
    /// <returns>��������Ч����</returns>
    public GameObject CreateBaoHuZhao()
    {
        string name = baoHuZhao.name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(baoHuZhao);
        return go;
    }
    /// <summary>
    /// ����һ�����ٵ�����Ч������
    /// </summary>
    /// <returns>���ٵ�����Ч����</returns>
    public GameObject CreateJiaSuDeYan()
    {
        string name = jiaSuDeYan.name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(jiaSuDeYan);
        return go;
    }
    /// <summary>
    /// ����һ��X����ɨ����Ч
    /// </summary>
    /// <returns></returns>
    public GameObject CreateX_RaySaoMiao()
    {
        string name = x_RaySaoMiao.name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(x_RaySaoMiao);
        return go;
    }
    public GameObject CreateThrowJianShe()
    {
        string name = throwObjectJianShe.name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(throwObjectJianShe);
        return go;
    }
    public GameObject CreateCaiHua()
    {
        string name = caiHuaObjectJianShe.name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(caiHuaObjectJianShe);
        return go;
    }
    /// <summary>
    /// ����һ��Ͷ�����ӳ�ȥ�Ĺ�
    /// </summary>
    /// <returns></returns>
    public GameObject CreateThrowRengGuang()
    {
        string name = throwRengGuang.name;
        GameObject go = ObjectPoolManager.Instance.GetOneObjectFromPool(throwRengGuang);
        return go;
    }
}