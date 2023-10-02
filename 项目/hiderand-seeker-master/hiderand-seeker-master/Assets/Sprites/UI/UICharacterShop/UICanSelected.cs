using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICanSelected : MonoBehaviour//, ISelectHandler,IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// ±³¾°Image
    /// </summary>
    public Image beiJingImager;
    [HideInInspector]
    public Sprite unSelectedSprite;
    public Sprite selectedSprite;
    public Action<UICanSelected> OnSelectedAction;
    public Action OnUnSelectedAction;
    public Button button;
    private void Awake()
    {
        if (unSelectedSprite == null)
            unSelectedSprite = beiJingImager.sprite;
        
    }
    private void Start()
    {
        button = this.AddComponent<Button>();
        button.onClick.AddListener(OnSelected);
        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayOnChickButton();
        });
    }
    /*
    private bool isEnter;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown:" + name);
        //OnSelected();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter:" + name);
        isEnter = true;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit:" + name);
        isEnter = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp:" + name);
        if (isEnter)
        {
            //OnSelected();
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        
    }
    */
    public virtual void OnSelected()
    {
        beiJingImager.sprite = selectedSprite;
        OnSelectedAction?.Invoke(this);
       
    }
    public virtual void OnUnSelectd()
    {
        OnUnSelectedAction?.Invoke();
        beiJingImager.sprite = unSelectedSprite;
    }
}
