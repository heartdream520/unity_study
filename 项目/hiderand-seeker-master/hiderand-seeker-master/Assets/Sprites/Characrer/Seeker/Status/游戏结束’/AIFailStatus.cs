using UnityEngine;

namespace SeekerStatus
{
    public class AIFailStatus : SeekerStatusBase
    {
        public AIFailStatus(SeekerStatusControl statusControl) : base(statusControl)
        {

        }
        private string statusString = "idle";
        public override void OnBegin()
        {
            base.OnBegin();
            seeker.tanZhaoDeng.SetActive(false);
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = 0;
            }

            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                float waitTime = 0;
                var x = seeker.animator.GetCurrentAnimatorStateInfo(
                    MyTools.GetAnimatorLayerIdByLayerName(seeker.animator, "Base Layer"));
                if (x.IsName("attack"))
                {
                    waitTime = 1.2f;
                }
                InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
                {
                    seeker.animator.CrossFade(statusString, 0.3f);
                    //seeker.canRoundToMainCamera = true;
                }, waitTime);
            }, 0.3f);
            
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }

        public override void OnUpdata()
        {
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = 0;
            }
            base.OnUpdata();
        }
    }
}