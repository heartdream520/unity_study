using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_03 : CoinBase
{
    public Vector3 rotationSpeed = new Vector3(0, 40, 0); // 旋转速度，分别对应x、y、z轴




    public Coin_03() : base(GameDefine.Coin_03_Money)
    {

    }
    public override void InitCoin()
    {
        base.InitCoin();

    }
    private void Start()
    {
        StartCoroutine(ToRotate());
    }
    IEnumerator ToRotate()
    {
        while (true)
        {
            Vector3 x = coinObject.transform.eulerAngles;
            transform.Rotate(rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

}