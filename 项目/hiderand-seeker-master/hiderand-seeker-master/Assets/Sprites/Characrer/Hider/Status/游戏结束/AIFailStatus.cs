



using UnityEngine;

namespace HiderStatus
{
    public class AIFailStatus : HiderStatusBase
    {
        public AIFailStatus(HiderStatusControl statusControl) : base(statusControl)
        {

        }
        private string statusString = "idle";
        public override void OnBegin()
        {
            base.OnBegin();
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = 0;
            }
            //hider.canRoundToMainCamera = true;
            hider.animator.CrossFade(statusString, 0.3f);
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }

        public override void OnUpdata()
        {
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = 0;
            }
            base.OnUpdata();
        }
    }
}