using HiderStatus;
using UnityEngine;

namespace SeekerStatus
{
    public class ThrowNoneStatus : SeekerStatusBase
    {

        //private string setBoolString = "throw";
        public ThrowNoneStatus(SeekerThrowLayerStatusControl statusControl) : base(statusControl)
        {

        }

        public override void InitStatus()
        {
            base.InitStatus();

        }

        public override void OnBegin()
        {
            base.OnBegin();
            animator.SetLayerWeight(throwStatusControl.throwLayerId, 0);
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }


        public override void OnOneGameEnd()
        {
            base.OnOneGameEnd();

        }
        public override void OnUpdata()
        {
            base.OnUpdata();

        }
    }
}