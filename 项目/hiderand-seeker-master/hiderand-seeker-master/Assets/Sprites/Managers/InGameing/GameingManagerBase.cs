using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameingManagerBase
{
    public GameMapCenter GameMapCenter;
    public CameraControl cameraControl;

    public bool gameEnd;

    public List<Hider> hiders; 
    public Seeker seeker;



    public virtual void InitGame()
    {
        GameMapCenter= GameingMainManager.Instance.GameMapCenter;
        cameraControl = 
            GameObject.FindGameObjectWithTag(GameDefine.MainCameraTag).
            AddComponent<CameraControl>();
        hiders = new List<Hider>();
        seeker = (CreateOne_SeekerBase(GameDataManager.Instance.
            charactersManager.GetNowSelectedSeekerPrefabs()));
        hiders.Add(CreateOne_HiderBase(GameDataManager.Instance.
            charactersManager.GetNowSelectedHiderPrefabs()));
        for (int i = 1; i < GameDefine.GameHiderCount; i++)
        {
            hiders.Add(CreateOne_HiderBase(GameDataManager.Instance.
                charactersManager.GetOneRanderHiderPrefabs()));
        }
        
        cameraControl.InitCameraBeforeSelectedIdentity();
    }
    public virtual void OnOneGameEnd(bool iswin)
    {
        gameEnd = true;
        foreach (var x in this.hiders)
        {
            x.OnOneGameEnd(iswin);
        }
        seeker.OnOneGameEnd(iswin);
    }
    public virtual void OnOneGameFail()
    {
        foreach (var x in this.hiders)
        {
            x.OnOneGameFail();
        }
        seeker.OnOneGameFail();
    }
    internal void OnOneGameFuHuo()
    {
        foreach (var x in this.hiders)
        {
            x.OnOneGameFuHuo();
        }
        seeker.OnOneGameFuHuo();
    }
    public Hider CreateOne_HiderBase(GameObject prefabs)
    {
        var go = GameObject.Instantiate(prefabs);
        var x = go.AddComponent<Hider>();
        float rotation = GameMapCenter.GetOneRotation();
        Vector3 position = GameMapCenter.GetOneHiderPositon();
        x.InitBase(position, rotation, GameMapCenter.hiderTransform, prefabs.name);
        return x;
    }
    public Seeker CreateOne_SeekerBase(GameObject prefabs)
    {
        var go = GameObject.Instantiate(prefabs);
        var x = go.AddComponent<Seeker>();
        float rotation = GameMapCenter.GetOneRotation();
        Vector3 position = GameMapCenter.GetOneSeekerPositon();
        x.InitBase(position, rotation, GameMapCenter.seekerTransform, prefabs.name);
        return x;
    }
    public virtual void UpData()
    {

    }

    internal void DeleteAllCharacter()
    {
        for (int i = hiders.Count - 1; i >= 0; i--)
        {
            hiders[i].statusControl.OnOneGameEnd();
            GameObject.Destroy(hiders[i].gameObject);
        }
        seeker.statusControl.OnOneGameEnd();
        GameObject.Destroy(seeker.gameObject);
    }
}