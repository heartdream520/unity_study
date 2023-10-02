using UnityEngine;
using UnityEngine.UI;

public class UICoin : MonoBehaviour
{
    public Text coinCountText;

    private void Start()
    {
        coinCountText.text = GameDataManager.Instance.moneyManager.Money.ToString();
        EventManager.Instance.OnCoinCountChangeAction += this.OnCoinCountChange;

        //Debug.Log("OnEnable");
    }
    private void OnDestroy()
    {
        EventManager.Instance.OnCoinCountChangeAction -= this.OnCoinCountChange;

    }
    private void OnCoinCountChange(int money)
    {
        //Debug.Log("SetMoney:" + money);
        this.coinCountText.text = money.ToString();
    }
}
