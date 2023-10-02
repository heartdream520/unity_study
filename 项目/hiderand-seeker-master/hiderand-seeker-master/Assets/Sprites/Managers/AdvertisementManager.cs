using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AdvertisementManager:Singleton<AdvertisementManager>
{
    public void SeeOneAdvertisement(Action action)
    {
        DoOneAdvertisement();
        action?.Invoke();
    }
    private void DoOneAdvertisement()
    {

    }

}

