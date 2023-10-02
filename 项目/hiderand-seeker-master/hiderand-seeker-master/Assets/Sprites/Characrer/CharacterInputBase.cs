using UnityEngine;

public class CharacterInputBase
{
    public bool CanInput;
    /// <summary>
    /// 默认可以进行输入
    /// </summary>
    public CharacterInputBase()
    {
        CanInput = true;
    }
    public virtual void OnOneGameEnd()
    {
        CanInput = false;
    }
    public virtual void Input()
    {

    }
    public virtual void UpData()
    {
        if (CanInput) Input();
    }
    public virtual void OnCollisionEnter(Collision collision)
    {

    }
    public virtual void OnCollisionStay(Collision collision)
    {

    }
    public virtual void OnCollisionExit(Collision collision)
    {

    }
}