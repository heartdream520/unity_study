using UnityEngine;
namespace SeekerStatus
{
    public class WinStatus : SeekerStatusBase
    {
        public WinStatus(SeekerStatusControl statusControl) : base(statusControl)
        {

        }

        private string statusString = "win";
        public override void OnBegin()
        {
            base.OnBegin();
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
                    seeker.canRoundToMainCamera = true; 
                    seeker.animator.CrossFade(statusString, 0.3f);
                }, waitTime);
            }, 0.3f);
            seeker.tanZhaoDeng.SetActive(false);

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