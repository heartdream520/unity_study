using UnityEngine;
namespace HiderStatus
{
    public class IdleStatus : HiderStatusBase
    {
        public IdleStatus(HiderStatusControl statusControl) : base(statusControl)
        {

        }

        private string setBoolString = "idle";
        public override void OnBegin()
        {
            base.OnBegin();
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }
            animator.SetBool(setBoolString, true);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            animator.SetBool(setBoolString, false);
        }

        public override void OnUpdata()
        {
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }
            base.OnUpdata();
        }
    }
}