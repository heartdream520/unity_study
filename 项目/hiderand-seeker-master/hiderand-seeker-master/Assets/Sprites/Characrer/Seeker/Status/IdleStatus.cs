using UnityEngine;
namespace SeekerStatus
{
    public class IdleStatus : SeekerStatusBase
    {
        public IdleStatus(SeekerStatusControl statusControl) : base(statusControl)
        {
            LookForHiderSpaceTime = 0;
        }
        private string setBoolString = "idle";
        public override void OnBegin()
        {
            base.OnBegin();
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }
            //Debug.LogError("IdleStatus-> OnBegin");
            LookForHiderSpaceTime = 0;
            canSeekForHider = true;
            animator.SetBool(setBoolString, true);
            if (GameingMainManager.Instance.GameIsBegin)
                seeker.tanZhaoDeng.SetActive(true);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }
            //Debug.LogError("IdleStatus-> OnEnd");
            if (animator != null)
                animator.SetBool(setBoolString, false);
            
        }

        public override void OnUpdata()
        {
            base.OnUpdata();
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }
            base.LookForHider();
        }
    }
}