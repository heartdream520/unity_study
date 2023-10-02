using UnityEngine;

public class CoinBase : MonoBehaviour
{
    public int money;
    public GameObject coinObject;
    public GameObject effectObject;
    private bool hasBeenTake=false;
    public CoinBase(int money)
    {
        this.money = money;
        InitCoin();
    }
   
    /// <summary>
    /// ³õÊ¼»¯½ð±Ò
    /// </summary>
    public virtual void InitCoin()
    {

    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (hasBeenTake) return;
        if (!GameingMainManager.Instance.GameIsBegin) return;
        if(other.tag==GameDefine.HiderTag||other.tag==GameDefine.SeekerTag)
        {
            var x = other.GetComponent<CharacterBase>();
            if (!x.isAIInput)
            {
                AudioManager.Instance.PlayGetCoin();
                hasBeenTake = true;
                x.GetMoney(this.money,()=>
                {
                    this.coinObject.SetActive(false);
                    this.effectObject.SetActive(true);
                    Destroy(this.gameObject, 3f);
                });
                

            }
        }
    }

}
