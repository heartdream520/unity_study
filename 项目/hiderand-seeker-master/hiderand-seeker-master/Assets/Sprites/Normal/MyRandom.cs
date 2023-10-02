
using UnityEngine;

public static class MyRandom
{
    public static float RangeFloat(float min,float max)
    {
        //Random.InitState(Time);
        return Random.Range(min,max);
    }
    public static int RangeInt(int min,int max)
    {
        //Random.InitState(Time.captureFramerate);
        return Random.Range(min,max+1);
    }

    internal static Vector3 RangeVector3()
    {
        return new Vector3(RangeInt(0,360), RangeInt(0, 360), RangeInt(0, 360));
    }
}
