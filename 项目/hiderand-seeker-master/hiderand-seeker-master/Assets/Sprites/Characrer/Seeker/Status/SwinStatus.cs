using UnityEngine;
namespace SeekerStatus
{
    public class SwinStatus : SeekerStatusBase
    {
        public SwinStatus(SeekerStatusControl statusControl) : base(statusControl)
        {
            LookForHiderSpaceTime = 0;
        }
        private float youyongEffectLastTime;
        private string setBoolString = "swin";
        public override void OnBegin()
        {
          
            base.OnBegin();
            canSeekForHider = true;
            animator.SetBool(setBoolString, true);

            seeker.youyongTuoWei.SetActive(true);

            youyongEffectLastTime = GameDefine.YouYongEffectLastTime;
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = seeker.NowSpeed;
            }

            seeker.tanZhaoDeng.SetActive(true);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            canSeekForHider = false;
            animator.SetBool(setBoolString, false);
            seeker.youyongTuoWei.SetActive(false);
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }
        }

        public override void OnUpdata()
        {
            base.OnUpdata();
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = seeker.NowSpeed;
            }
            youyongEffectLastTime -= Time.deltaTime;

            if (youyongEffectLastTime < 0) seeker.youyongTuoWei.SetActive(false);



            Vector2 vector = seeker.InputXY;
            Vector3 direction = new Vector3(vector.x, 0f, vector.y).normalized;
            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                seeker.transform.rotation = Quaternion.Slerp(seeker.transform.rotation, targetRotation,
                    statusControl.seeker.rotationSpeed * Time.deltaTime);

            }
            if (!seeker.isAIInput)
                this.seeker.transform.position +=
                    (seeker.NowSpeed * seeker.transform.forward) * Time.deltaTime;
            base.LookForHider();
        }
    }
}