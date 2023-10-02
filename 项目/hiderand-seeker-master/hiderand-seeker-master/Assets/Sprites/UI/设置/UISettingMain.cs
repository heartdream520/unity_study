using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingMain : UIWindows
{
    public MyToggle musicToggle;
    public MyToggle soundToggle;
    private void Start()
    {
        musicToggle.IsOn = AudioManager.GetMusicIsOn();
        soundToggle.IsOn = AudioManager.GetSoundIsOn();

        musicToggle.button.onClick.AddListener(OnMusicToggleValueChange);
        soundToggle.button.onClick.AddListener(OnSoundToggleValueChange);
    }
    bool musicFirst=false;
    bool soundFirst=false;
    public void OnMusicToggleValueChange()
    {
        AudioManager.Instance.PlayOnChickButton();
        Debug.Log(musicToggle.IsOn);
        AudioManager.Instance.SetMusicAudio(musicToggle.IsOn);
    }
    public void OnSoundToggleValueChange()
    {
        AudioManager.Instance.PlayOnChickButton();
        Debug.Log(soundToggle.IsOn);
        AudioManager.Instance.SetSoundAudio(soundToggle.IsOn);   



    }
}
