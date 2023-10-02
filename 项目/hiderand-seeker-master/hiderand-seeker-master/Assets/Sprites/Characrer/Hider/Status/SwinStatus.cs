

using Unity.VisualScripting;
using UnityEngine;
namespace HiderStatus
{
    public class SwinStatus : HiderStatusBase
    {
        public SwinStatus(HiderStatusControl statusControl) : base(statusControl)
        {

        }
        private float youyongEffectLastTime;
        private string setBoolString = "swin";
        public override void OnBegin()
        {
            base.OnBegin();
            animator.SetBool(setBoolString, true);

            hider.youyongTuoWei.SetActive(true);

            youyongEffectLastTime = GameDefine.YouYongEffectLastTime;

            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = hider.NowSpeed;
            }
        }

        public override void OnEnd()
        {
            base.OnEnd();
            animator.SetBool(setBoolString, false);
            hider.youyongTuoWei.SetActive(false);
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed =GameDefine.NavMeshAgentSpeed;
            }
        }

        public override void OnUpdata()
        {
            base.OnUpdata();
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = hider.NowSpeed;
            }
            youyongEffectLastTime -= Time.deltaTime;

            if (youyongEffectLastTime < 0) hider.youyongTuoWei.SetActive(false);



            Vector2 vector = hider.InputXY;
            Vector3 direction = new Vector3(vector.x, 0f, vector.y).normalized;
            
                if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                hiderTransform.rotation = Quaternion.Slerp(hiderTransform.rotation, targetRotation,
                    statusControl.hider.rotationSpeed * Time.deltaTime);

            }
            if (!hider.isAIInput)
                this.hiderTransform.position +=
                (hider.NowSpeed * hider.transform.forward) * Time.deltaTime;
            

        }
    }
}