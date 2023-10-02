using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Video;

class Resloader
{
    public static T Load<T>(string path) where T : UnityEngine.Object
    {
        T t = Resources.Load<T>(path);
        if (t == null)
        {
            Debug.LogErrorFormat("Resloader->Load:Path:{0} Not Exist", path);
        }
        return t;
    }
    public static Sprite LoadSprite(string path)
    {
        
        return Resources.Load<Sprite>(path);
    }
    public static GameObject LoadGameObject(string path)
    {
        return Resources.Load<GameObject>(path);
    }


    public static AudioClip LoadAudioClip(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
    public static List<Sprite> LoadAllSprites_List(string path)
    {
        var Paths = GetAllResourcesPath(path);
        List<Sprite> result = new List<Sprite>();
        for (int i = 0; i < Paths.Length; i++)
        {
            result.Add(LoadSprite(Paths[i]));
        }
        return result;
    }    
    public static Dictionary<string,Sprite> LoadAllSprites_Dictionary(string path)
    {
        var Paths = GetAllResourcesPath(path);
        Dictionary<string,Sprite> result = new Dictionary<string, Sprite>();
        for (int i = 0; i < Paths.Length; i++)
        {
            result[Paths[i].Substring(path.LastIndexOf('/') + 1)] = LoadSprite(Paths[i]);
        }
        return result;
    }
    public static Dictionary<string,AudioClip> LoadAllAudioClip_Dictionary(string path)
    {
        var Paths = GetAllResourcesPath(path);
        Dictionary<string, AudioClip> result = new Dictionary<string, AudioClip>();
        for (int i = 0; i < Paths.Length; i++)
        {
            result[Paths[i].Substring(path.LastIndexOf('/') + 1)] = LoadAudioClip(Paths[i]);
        }
        return result;
    }
    private static string[] GetAllResourcesPath(string path)
    {
        string[] allPaths = Resources.LoadAll(path, typeof(UnityEngine.Object)).Select(o => o.name).ToArray();
        allPaths= allPaths.Distinct().ToArray();
        for (int i = 0; i < allPaths.Length; i++)
        {
            int x = allPaths[i].LastIndexOf('.');
            allPaths[i] = allPaths[i].Substring(0, x == -1 ? allPaths[i].Length : x);
            allPaths[i] = path + allPaths[i];
        }
        return allPaths;
    }

    internal static GameObject LoadGameObject(object jinBiObjectPath)
    {
        throw new NotImplementedException();
    }

}