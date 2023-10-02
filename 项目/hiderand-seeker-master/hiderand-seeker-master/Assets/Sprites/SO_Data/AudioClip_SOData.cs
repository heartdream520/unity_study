using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClip_SOData", menuName = "SO_Data/AudioClip_SOData")]
public class AudioClip_SOData : ScriptableObject
{
    public List<AudioClip_Data> aduioClipData;
    public List<AudioClip> GetAudioClip(string s)
    {
        List<AudioClip> audioClips = new List<AudioClip>();
        var x = aduioClipData.FindAll(x => x.name.StartsWith(s));
        for (int i=0;i<x.Count;i++)
        {
            audioClips.Add(x[i].audioClip);
        }
        return audioClips;
    }
}
[Serializable]
public class AudioClip_Data
{
    public string name=> audioClip.name;
    public AudioClip audioClip;
}
