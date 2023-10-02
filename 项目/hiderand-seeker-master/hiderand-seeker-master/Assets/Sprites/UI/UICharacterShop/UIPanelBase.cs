using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelBase : MonoBehaviour
{
    public virtual void OnPanelSelected()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void OnPanelNotSelected()
    {
        this.gameObject.SetActive(false);
    }
}
