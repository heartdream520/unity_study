



using UnityEngine;

namespace HiderStatus
{
    public class FailStatus : HiderStatusBase
    {
        public FailStatus(HiderStatusControl statusControl) : base(statusControl)
        {

        }
        GameObject cry;
        private string statusString = "fail";
        public override void OnBegin()
        {
            base.OnBegin();
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = 0;
            }
            hider.canRoundToMainCamera = true;
            hider.animator.CrossFade(statusString, 0.3f);
            cry = MyTools.BfsGetObjectPosNameGameObject(hider.gameObject, "Cry");
            cry.SetActive(true);
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