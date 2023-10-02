
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UIGameOvereCharacterJinDuPanel : MonoBehaviour
{
    public Slider slider;
    public Image characterImage;

    /// <summary>
    /// 获得角色的Panel
    /// </summary>
    public GameObject getCharacrerPanel;
    private void Start()
    {
        getCharacrerPanel.SetActive(false);
    }
    private string characterString;
    internal void SetJinDu(float x)
    {
         characterString = GameDataManager.
            Instance.charactersManager.GetOneCharacterJinDuString();
        if(string.IsNullOrEmpty(characterString))
        {
            this.gameObject.SetActive(false);
            return;
        }
        if(characterString.StartsWith("Hider"))
        {
            characterImage.sprite = SO_DataManager.Instance.hiderSo_Data
                .GetCharacter(characterString).shopImageSprite;
        }
        else
        {
            characterImage.sprite = SO_DataManager.Instance.SeekerSo_Data
                .GetCharacter(characterString).shopImageSprite;
        }
        if(GameDataManager.Instance.charactersManager.CharacterJinDu>=1)
        {
            GameDataManager.Instance.charactersManager.CharacterJinDu = 0.8f;
        }
        if (x > 1) x = 1;
        slider.value = GameDataManager.Instance.charactersManager.CharacterJinDu;
        StartCoroutine(SetJinDuIEnumerator(x));
        
    }
    public void OnChickNotGetCharacterButton()
    {
        GameDataManager.Instance.charactersManager.haveNotGet = true;
        GameDataManager.Instance.charactersManager.GetOneCharacterJinDuString();
        AudioManager.Instance.PlayOnChickButton();
        StopAllCoroutines();
        getCharacrerPanel.gameObject.SetActive(false);
        StartCoroutine(SetJinDuIFrom_X_To_YEnumerator(1, 0));
        InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            AudioManager.Instance.PlayDotGetUnlockCharacter();
        }, 0.3f);


        
    }
    public void OnchickGetCharacterButton()
    {
        AudioManager.Instance.PlayOnChickButton();
        RewardedAdManager.Instance.PlayAd(RewardedAdEnum.RV_Unlock_Skins_Level, () =>
        {
            if (characterString.StartsWith("Hider"))
            {
                GameDataManager.Instance.charactersManager
                    .HiderJieSuoDic[characterString] = true;
            }
            else
            {
                GameDataManager.Instance.charactersManager
                    .SeekerJieSuoDic[characterString] = true;

            }
            getCharacrerPanel.SetActive(false);
        },()=>
        {

        });




        
    }
    IEnumerator SetJinDuIEnumerator(float x)
    {
        yield return new WaitForSeconds(0.4f);
        x = Mathf.Min(1, x);
        GameDataManager.Instance.charactersManager.CharacterJinDu = x;
        while (slider.value<x)
        {
            slider.value += 0.2f * Time.deltaTime;
            slider.value = Mathf.Min(slider.value, x);
            //Debug.Log(slider.value + "   " + x);
            yield return new WaitForEndOfFrame();
        }
        if(x>=0.95f)
        {
            GetOneJiHuiGetOneCharacter();

        }
    }
    private void GetOneJiHuiGetOneCharacter()
    {
        Debug.LogError("获得一个人物的机会");
        GameDataManager.Instance.charactersManager.CharacterJinDu = 0;

        CharacterModleManager.Instance.SetActiveModle(characterString);
        getCharacrerPanel.gameObject.SetActive(true);
    }
    IEnumerator SetJinDuIFrom_X_To_YEnumerator(float x, float y)
    {
        if (x == y) yield break;
        float xishu = y - x > 0 ? 1 : -1;
        slider.value = x;
        while (Mathf.Abs(slider.value - y) > 0.05f)
        {
            slider.value += 0.6f * Time.deltaTime * xishu;
            if (xishu == 1)
                slider.value = Mathf.Min(slider.value, y);
            else slider.value = Mathf.Max(slider.value, y);
            yield return new WaitForEndOfFrame();
        }
    }
}
