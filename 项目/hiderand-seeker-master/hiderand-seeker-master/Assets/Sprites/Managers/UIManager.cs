using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public Canvas[] canvas;
    private Dictionary<string, UIElement> uiElementDictionary = new Dictionary<string, UIElement>();
    public void Init()
    {
        uiElementDictionary.Add(typeof(UISelectedIdentity).ToString(),
            new UIElement("InGameing/UISelectedIdentity", false, 1));

        uiElementDictionary.Add(typeof(UIGamePlaying).ToString(),
            new UIElement("InGameing/游戏进行中/UIGamePlaying", false, 2));

        uiElementDictionary.Add(typeof(WorldUISeekerDaoJiShi).ToString(),
            new UIElement("WorldUI/WorldUISeekerDaoJiShi", false, 1));

        uiElementDictionary.Add(typeof(UICharacterShop).ToString(),
            new UIElement("CharacterShopUI/UICharacterShop", false, 4));
        
        uiElementDictionary.Add(typeof(UISettingMain).ToString(),
            new UIElement("设置/UISettingMain", false, 5));
        uiElementDictionary.Add(typeof(UISettingGameing).ToString(),
            new UIElement("设置/UISettingGameing", false, 5));


    }
    public T Show<T>() where T:UIWindows
    {
        string type = typeof(T).ToString();
        UIElement uIElement;
        if (uiElementDictionary.TryGetValue(type, out uIElement))
        {
            if (uIElement.uiWindow != null)
            {
                //uIElement.uiWindow.OnChickClose();
                uIElement.uiWindow.OnEnd();
                uIElement.uiWindow.InitUI();
                if (!uIElement.uiWindow.gameObject.activeSelf)
                {

                    uIElement.uiWindow.gameObject.SetActive(true);
                }
                else Debug.LogWarning($"UIManager->Show : UI:{type} Have Been Open ！！！");

            }
            else
            {
                var go = GameObject.Instantiate(Resloader.Load<GameObject>(uIElement.sourcesPath));

                go.transform.SetParent(canvas[uIElement.canvasId].transform, false);
                go.name = type;
                T uiWindow = null;
                if (!go.TryGetComponent<T>(out uiWindow))
                {
                    Debug.LogError($"预制体：{uIElement.sourcesPath} 没有搭载脚本T");
                }

                uIElement.uiWindow = uiWindow;
                uIElement.uiWindow.InitUI(uIElement);
            }
            return (T)uIElement.uiWindow;
        }
        else
        {
            Debug.LogError($"UIManager->Show: UI:{type} Not Register!!!");
        }
        return null;

    }
    public void OnCloseUI(UIElement uiElement, MyEnum.UICloseType closeType)
    {
        switch (closeType)
        {
            case MyEnum.UICloseType.None:
                uiElement.uiWindow.OnCloseAction?.Invoke();
                break;
            case MyEnum.UICloseType.Yes:
                uiElement.uiWindow.OnYesAction?.Invoke();
                break;
            case MyEnum.UICloseType.No:
                uiElement.uiWindow.OnNoAction?.Invoke();
                break;
            default:
                break;
        }
        uiElement.uiWindow.OnEnd();
        if (uiElement.isCache)
        {
            uiElement.uiWindow.gameObject.SetActive(false);
        }
        else
        {
            Destroy(uiElement.uiWindow.gameObject);
            uiElement.uiWindow = null;
        }
    }
    public T TryGetNowActiveUI<T>() where T : UIWindows
    {
        string type = typeof(T).ToString();
        UIElement uIElement;
        if (uiElementDictionary.TryGetValue(type, out uIElement))
        {
            return uIElement.uiWindow as T;
        }
        return null;
    }
    public class UIElement
    {
        public UIWindows uiWindow;
        public string sourcesPath;
        public bool isCache = false;
        public int canvasId;

        public UIElement(string sources, bool isCache,int canvasId)
        {
            this.uiWindow = null;
            this.sourcesPath = PathDefine.UIPrefabsPath + sources;
            this.isCache = isCache;
            this.canvasId = canvasId;
        }
    }
}