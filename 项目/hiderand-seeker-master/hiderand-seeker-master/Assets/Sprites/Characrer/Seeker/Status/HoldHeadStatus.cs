using UnityEngine;
namespace SeekerStatus
{
    public class HoldHeadStatus : SeekerStatusBase
    {
        public HoldHeadStatus(SeekerStatusControl statusControl) : base(statusControl)
        {

        }
        private string setBoolString = "hodeHead";
        public override void OnBegin()
        {
            base.OnBegin();
            if (seeker.isAIInput)
            {
                if (seeker.aiInput.navMeshAgent != null)
                    seeker.aiInput.navMeshAgent.speed = 0;
            }
            canSeekForHider = false;
            if(animator!=null)
            animator.SetBool(setBoolString, true);

            seeker.tanZhaoDeng.SetActive(false);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }
            if (animator != null)
                animator.SetBool(setBoolString, false);

        }

        public override void OnUpdata()
        {
            base.OnUpdata();


        }
    }
}