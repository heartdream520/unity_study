using UnityEngine;
namespace SeekerStatus
{
    public class RunStatus : SeekerStatusBase
    {


        public RunStatus(SeekerStatusControl statusControl) : base(statusControl)
        {
            LookForHiderSpaceTime=0;
        }
        private float paobuEffectLastTime;
        private string setBoolString = "run";
        public override void OnBegin()
        {
            //
            base.OnBegin();
            //Debug.LogError("RunStatus-> OnBegin");
            LookForHiderSpaceTime = 0;
            canSeekForHider = true;
            animator.SetBool(setBoolString, true);

            seeker.paobuTuoWei.SetActive(true);

            paobuEffectLastTime = GameDefine.PaoBuEffectLastTime;
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = seeker.NowSpeed;
            }

            seeker.tanZhaoDeng.SetActive(true);

        }

        public override void OnEnd()
        {
            base.OnEnd();
            //Debug.LogError("RunStatus-> OnEnd");
            animator.SetBool(setBoolString, false);

            seeker.paobuTuoWei.SetActive(false);
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }

        }
        float footAduioTime;
        public override void OnUpdata()
        {
            base.OnUpdata();

            footAduioTime -= Time.deltaTime;
            if (!seeker.isAIInput)
            {
                if (footAduioTime <= 0)
                {
                    AudioManager.Instance.PlayFootAudio();
                    footAduioTime = 0.5f;
                }
               
            }
           

            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = seeker.NowSpeed;
            }

            paobuEffectLastTime -= Time.deltaTime;
            if (paobuEffectLastTime < 0) seeker.paobuTuoWei.SetActive(false);

            Vector2 vector = seeker.InputXY;
            Vector3 direction = new Vector3(vector.x, 0f, vector.y).normalized;
            if (direction.magnitude > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                //if (vector.x > 0.1f && vector.y > 0.1f)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, statusControl.seeker.rotationSpeed * Time.deltaTime);

            }

            // 在坡度上应用额外的力以克服重力
            Vector3 slopeDirection = TryGetSlopeDirection();

            // hider.rigidbody__.velocity = hider.NowSpeed * slopeDirection;
            if (slopeDirection != seeker.transform.forward)
            {
                seeker.rigidbody__.velocity = Vector3.zero;
            }
            //statusControl.SetStatus(MyEnum.SeekerStatusEnum.Run);

            if (!seeker.isAIInput)
                this.transform.position +=
               (seeker.NowSpeed * slopeDirection) * Time.deltaTime;
            base.LookForHider();
        }
        Vector3 TryGetSlopeDirection()
        {
            RaycastHit hit;
            //Debug.DrawLine(transform.position, transform.position + transform.forward * 10f, Color.red);
            if (!Physics.Raycast(transform.position, -transform.up,
                out hit, 0.5f, 1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
            {
                return transform.forward;
                //没有碰到物体
            }
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            var x = Vector3.Cross(transform.right, hit.normal).normalized;
            if (Vector3.Angle(transform.forward, x) >= 85) return transform.forward;
            return x;
        }
    }
}