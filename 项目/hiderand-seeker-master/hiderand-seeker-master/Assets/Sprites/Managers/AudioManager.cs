using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioClip_SOData musicClip_SoData;
    public AudioClip_SOData soundClip_SoData;

    public AudioSource musicAudioSource;
    public AudioSource soundAudioSource;
    public Dictionary<string, List<AudioClip>> audioClipDic =
        new Dictionary<string, List<AudioClip>>();
    public float buttonSoundBeforeTime;
    private void Start()
    {
        EventManager.Instance.OneGameEndAction += OnOneGameEnd;
        SetAduio();
    }
    private void OnDestroy()
    {
        EventManager.Instance.OneGameEndAction -= OnOneGameEnd;

    }
    public void SetAduio()
    {
        SetSoundAudio(GetSoundIsOn());
        SetMusicAudio(GetMusicIsOn());
    }
    internal static bool GetMusicIsOn()
    {
         return PlayerPrefsDataManager.GetBool("MusicIsOn");
    }

    internal static bool GetSoundIsOn()
    {
        return PlayerPrefsDataManager.GetBool("SoundIsOn");
    }
    public void SetSoundAudio( bool isOn)
    {
        PlayerPrefsDataManager.SetBool("SoundIsOn", isOn);
        soundAudioSource.volume = isOn ? 1 : 0;
    }
    public void SetMusicAudio( bool isOn)
    {
        PlayerPrefsDataManager.SetBool("MusicIsOn", isOn);
        musicAudioSource.volume = isOn ? GameDefine.MusicVolume : 0;
    }

    private void Update()
    {
        buttonSoundBeforeTime += Time.deltaTime;


        if(TestManager.Instance.testMode)
        {
            if(TestManager.Instance.noBgm)
            {
                musicAudioSource.volume = 0;
            }
            else
            {
                musicAudioSource.volume = GameDefine.MusicVolume;
            }

        }
        else
        {
            musicAudioSource.volume = GameDefine.MusicVolume;
        }
    }
    public void PlayOnChickButton()
    {
        // if (buttonSoundBeforeTime > 0.2f)
        //{
        soundAudioSource.PlayOneShot(GetSoundClip("��ť")[0]);
        buttonSoundBeforeTime = 0;
        // }
    }
    public void PlayMainBackMusic()
    {
        musicAudioSource.clip = GetMusicClip("������")[0];
        musicAudioSource.Play();

    }
    public void PlayGameBeginDaoJiShiSound()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("������Ϸ����ʱ")[0]);
    }
    public void PlayGameEndDaoJiShiSound()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("������Ϸ����ʱ")[0]);
    }
    public void PlayDaoJiShiEndGameBegin()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("����ʱ������Ϸ��ʼ")[0]);
    }
    public void PlaySeekerAttack()
    {
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            soundAudioSource.PlayOneShot(GetSoundClip("seeker����")[0]);
        }, 1f);
       
    }

    public void PlayFootAudio()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("�Ų���")[0]);
    }
    public void PlayTakeTouZhiWu()
    {
        if (GameingMainManager.Instance.GameIsBegin)
        {
            soundAudioSource.PlayOneShot(GetSoundClip("����Ͷ����")[0]);
        }
    }
    public void PlayThrowTouZhiWu()
    {
        if (GameingMainManager.Instance.GameIsBegin)
        {
            soundAudioSource.PlayOneShot(GetSoundClip("��Ͷ����")[0]);
        }

    }
    public void PlayThrowZaZhong()
    {
        if (GameingMainManager.Instance.GameIsBegin)
        {
            soundAudioSource.PlayOneShot(GetSoundClip("������")[0]);
        }
    }
    public void PlayZangZhongXiao()
    {
        if (GameingMainManager.Instance.GameIsBegin)
        {
            soundAudioSource.PlayOneShot(GetSoundClip("������Ц")[0]);
        }
    }
    public void PlaySeekerXiao()
    {
        var x = GetSoundClip("SeekerЦ");
        soundAudioSource.PlayOneShot(x[MyRandom.RangeInt(0, x.Count - 1)]);
    }


    public void PlayGetCoin()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("���")[0]);
    }
    public void PlayWhoIsThere()
    {
        var x = GetSoundClip("˭������");
        soundAudioSource.PlayOneShot(x[MyRandom.RangeInt(0, x.Count - 1)]);
    }
    public void PlayX_Ray_Sound()
    {
        if (GameingMainManager.Instance.GameIsBegin)
        {
            soundAudioSource.PlayOneShot(GetSoundClip("X����")[0]);
        }
    }
    /// <summary>
    /// ��Ҫ����������
    /// </summary>
    public void PlayDotGetUnlockCharacter()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("��Ҫ�����Ľ�ɫ")[0]);
    }
    private void OnOneGameEnd(bool iswin)
    {
        if (iswin)
        {
            PlayGameWin();
        }
        else
        {
            PlayGameFail();
        }
    }

    public void PlayGameWin()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("��Ϸ�ɹ�")[0]);
    }
    public void PlayGameFail()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("��Ϸʧ��")[0]);
    }
    public void PlayFailStar()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("Ӯ����")[0]);
    }
    public void PlayWinStar()
    {
        soundAudioSource.PlayOneShot(GetSoundClip("������")[0]);
    }
    /// <summary>
    /// ������Ц��
    /// </summary>


    /// <summary>
    /// ��������
    /// </summary>
    public void PlayChuanSong()
    {
        if (GameingMainManager.Instance.GameIsBegin)
        {
            soundAudioSource.PlayOneShot(GetSoundClip("������")[0]);
        }
    }










    List<AudioClip> audioClips;
    private List<AudioClip> GetSoundClip(string clipName)
    {
        string aduioClipDicName = clipName + "_Sound";
        if (!audioClipDic.TryGetValue(aduioClipDicName, out audioClips))
        {
            audioClips = soundClip_SoData.GetAudioClip(clipName);
            if(audioClips==null|| audioClips.Count==0)
            {
                Debug.LogError("SoundClip_SoData û��Ƭ�Σ�" + clipName);
            }
            audioClipDic.Add(aduioClipDicName, audioClips);
        }
        return audioClips;
    }
    private List<AudioClip> GetMusicClip(string clipName)
    {
        string aduioClipDicName =clipName + "_Music";
        if (!audioClipDic.TryGetValue(aduioClipDicName, out audioClips))
        {
            audioClips = musicClip_SoData.GetAudioClip(clipName);
            if (audioClips == null || audioClips.Count == 0)
            {
                Debug.LogError("MusicClip_SoData û��Ƭ�Σ�" + clipName);
            }
            audioClipDic.Add(aduioClipDicName, audioClips);
        }
        return audioClips;
    }

    internal void StopAudio()
    {
        soundAudioSource.Pause();
        musicAudioSource.Pause();
    }

    internal void UndStopAudio()
    {
        soundAudioSource.UnPause();
        musicAudioSource.UnPause();
    }

}
