using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin_01 : CoinBase
{
    public Vector3 rotationSpeed = new Vector3(60, 65, 70); // 旋转速度，分别对应x、y、z轴
    public Coin_01() : base(GameDefine.Coin_01_Money)
    {

    }
    private void Start()
    {
        //this.transform.eulerAngles = MyRandom.RangeVector3();
        StartCoroutine(ToRotate());
    }
    public override void InitCoin()
    {
        base.InitCoin();

    }
    
    IEnumerator ToRotate()
    {
        while (true)
        {
            Vector3 x = coinObject.transform.eulerAngles;
            coinObject.transform.Rotate(rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
