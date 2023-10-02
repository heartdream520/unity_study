using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBuffControl
{
    public class BuffInformation
    {
        public BuffInformation(BuffBase buff)
        {
            this.buff = buff;
            this.lastTime = buff.buffLastTime;
        }
        public BuffBase buff;
        //剩余的持续时间
        public float lastTime;
        public void UpDataLastTime()
        {
            lastTime = buff.buffLastTime;
        }
    }


    public Dictionary<string, BuffInformation> buffsDic;
    public CharacterBase character;
    public PlayerBuffControl(CharacterBase character)
    {
        buffsDic = new Dictionary<string, BuffInformation>();
        this.character = character;
    }
    public List<string> removeBuffName = new List<string>();
    public void updata()
    {
        foreach (var x in this.buffsDic)
        {
            x.Value.buff.OnBuffUpdata();
            x.Value.lastTime -= Time.deltaTime;
            if (x.Value.lastTime <= 0)
            {
                x.Value.buff.OnBuffEnd();
                removeBuffName.Add(x.Key);
            }

        }
        if (removeBuffName.Count > 0)
        {
            for (int i = 0; i < removeBuffName.Count; i++)
            {
                if (buffsDic.ContainsKey(removeBuffName[i]))

                    buffsDic.Remove(removeBuffName[i]);
            }
        }
        removeBuffName.Clear();
    }

    /// <summary>
    /// 添加一个buff
    /// </summary>
    /// <param name="buffBase">要添加的buff</param>
    public void AddOneBuff<T>(T buff) where T : BuffBase
    {
        string name = typeof(T).ToString();
        //OnOneBuffEnd<T>();
        if (buffsDic.ContainsKey(name))
        {
            buffsDic[name].buff.OnBuffEnd();
            buffsDic.Remove(name);
        }
        var x = new BuffInformation(buff);
        buff.OnBuffBegin();
        this.buffsDic.Add(name, x);


    }
    public void OnOneBuffEnd<T>()
    {
        string name = typeof(T).ToString();
        if (buffsDic.ContainsKey(name))
        {
            buffsDic[name].buff.OnBuffEnd();
            removeBuffName.Add(name);
        }
    }
    public void OnOneGameEnd()
    {
        foreach (var x in this.buffsDic)
        {
            if (x.Value.buff is MaterialBuffBase) continue;
            x.Value.buff.OnBuffEnd();
        }
    }
}