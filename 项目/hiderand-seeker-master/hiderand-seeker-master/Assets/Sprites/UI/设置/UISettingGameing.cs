using MainScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingGameing : UIWindows
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
    bool musicFirst = false;
    bool soundFirst = false;
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

    public override void InitUI(UIManager.UIElement uIElement = null)
    {
        base.InitUI(uIElement);
        AudioManager.Instance.soundAudioSource.Pause();
        Time.timeScale = 0;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        AudioManager.Instance.soundAudioSource.UnPause();
        Time.timeScale = 1;
    }
    public void OnChickHomeButton()
    {
        UIMain.Instance.gameObject.SetActive(true);
        GameingMainManager.Instance.OnOneGameEnd(false);

        this.OnChickClose();
        AudioManager.Instance.soundAudioSource.Stop();
        UIManager.Instance.TryGetNowActiveUI<UIGamePlaying>().OnChickClose();
        GameingMainManager.Instance.BeginOneNewGame();
    }
}
