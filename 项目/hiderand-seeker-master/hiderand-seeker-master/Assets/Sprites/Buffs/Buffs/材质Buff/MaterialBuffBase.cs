using UnityEngine;

public class MaterialBuffBase : BuffBase
{

    public Hider hider;
    public Seeker seeker;
    public MaterialBuffBase(Hider hider, float lastTime) : base(lastTime)
    {
        this.hider = hider;
    }
    public MaterialBuffBase(Seeker seeker, float lastTime) : base(lastTime)
    {
        this.seeker = seeker;
    }

    public sealed override void OnBuffBegin()
    {
        base.OnBuffBegin();
        if (hider != null) OnHiderBuffBegin();
        else OnSeekerBuffBegin();
    }

    public sealed override void OnBuffEnd()
    {
        base.OnBuffEnd();
        if (hider != null) OnHiderBuffEnd();
        else OnSeekerBuffEnd();
    }
    public virtual void OnHiderBuffBegin()
    {

    }
    public virtual void OnSeekerBuffBegin()
    {

    }
    public virtual void OnHiderBuffEnd()
    {

    }
    public virtual void OnSeekerBuffEnd()
    {

    }

    public sealed override void OnBuffUpdata()
    {
        base.OnBuffUpdata();
    }
}