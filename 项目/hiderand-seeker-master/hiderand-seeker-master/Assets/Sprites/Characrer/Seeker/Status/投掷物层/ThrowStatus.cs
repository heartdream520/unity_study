using HiderStatus;
using UnityEngine;

namespace SeekerStatus
{
    public class ThrowStatus : SeekerStatusBase
    {

        private string setBoolString = "throw";
        private float waitBaseTime;
        private float waitTime;
        public ThrowStatus(SeekerThrowLayerStatusControl statusControl) : base(statusControl)
        {

        }

        public override void InitStatus()
        {
            base.InitStatus();
            waitBaseTime = 0.5f;

        }

        public override void OnBegin()
        {
            base.OnBegin();
            animator.SetBool(setBoolString, true);
            animator.SetLayerWeight(throwStatusControl.throwLayerId, 1);
            waitTime = waitBaseTime;
        }

        public override void OnEnd()
        {
            base.OnEnd();
            animator.SetBool(setBoolString, false);
        }


        public override void OnOneGameEnd()
        {
            base.OnOneGameEnd();

        }
        public override void OnUpdata()
        {
            base.OnUpdata();
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                return;
            }
            if (judge())
            {
                throwStatusControl.SetStatus(MyEnum.SeekerStatusEnum.ThrowNone);
            }

        }
        public bool judge()
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(throwStatusControl.throwLayerId);
            return stateInfo.IsName("None");
        }
    }
}