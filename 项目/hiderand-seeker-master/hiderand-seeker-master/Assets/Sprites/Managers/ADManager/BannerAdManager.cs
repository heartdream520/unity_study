using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAdManager : Singleton<BannerAdManager>
{
    public void PlayBanner()
    {
        if (TestManager.Instance.noAds)
        {
            return;
        }
        MaxManager.Instance.ShowBanner();
    }
    public void HideBanner()
    {
        MaxManager.Instance.HideBanner();
    }
    public void DestroyBanner()
    {
        MaxManager.Instance.DestroyBanner();
    }

}
