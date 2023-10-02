using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_02 : CoinBase
{
    public Vector3 rotationSpeed = new Vector3(35, 40, 45); // 旋转速度，分别对应x、y、z轴




    public Coin_02() : base(GameDefine.Coin_02_Money)
    {

    }
    public override void InitCoin()
    {
        base.InitCoin();
       

    }
    private void Start()
    {
        //this.transform.eulerAngles = MyRandom.RangeVector3();
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