

using Unity.VisualScripting;
using UnityEngine;
namespace HiderStatus
{
    public class RunStatus : HiderStatusBase
    {
        public RunStatus(HiderStatusControl statusControl) : base(statusControl)
        {
        }
        private float paobuEffectLastTime;
        private string setBoolString = "run";
        public override void OnBegin()
        {
            base.OnBegin();



            animator.SetBool(setBoolString, true);

            hider.paobuTuoWei.SetActive(true);

            paobuEffectLastTime = GameDefine.PaoBuEffectLastTime;

            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = hider.NowSpeed;
            }
        }

        public override void OnEnd()
        {
            base.OnEnd();
            animator.SetBool(setBoolString, false);
            hider.paobuTuoWei.SetActive(false);
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }

        }
        float footAduioTime;
        public override void OnUpdata()
        {
            base.OnUpdata();

            footAduioTime -= Time.deltaTime;
            if (!hider.isAIInput)
            {
                if (footAduioTime <= 0)
                {
                    AudioManager.Instance.PlayFootAudio();
                    footAduioTime = 0.5f;
                }
                
            }

            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = hider.NowSpeed;
            }
            paobuEffectLastTime -= Time.deltaTime;
            if (paobuEffectLastTime < 0) hider.paobuTuoWei.SetActive(false);

            Vector2 vector = hider.InputXY;
            Vector3 direction = new Vector3(vector.x, 0f, vector.y).normalized;

            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                hiderTransform.rotation = Quaternion.Slerp(hiderTransform.rotation, targetRotation, statusControl.hider.rotationSpeed * Time.deltaTime);

            }

            Vector3 slopeDirection = TryGetSlopeDirection();

            // hider.rigidbody__.velocity = hider.NowSpeed * slopeDirection;
            if (slopeDirection != hider.transform.forward)
            {
               // hider.rigidbody__.useGravity = false;
                hider.rigidbody__.velocity = Vector3.zero;
            }
            if (!hider.isAIInput)
                this.hiderTransform.position +=
                    (hider.NowSpeed * slopeDirection) * Time.deltaTime;

           

        }

        Vector3 TryGetSlopeDirection()
        {
            RaycastHit hit;
            //Debug.DrawLine(hiderTransform.position, hiderTransform.position + hiderTransform.forward * 10f, Color.red);

            if (!Physics.Raycast(hiderTransform.position, -hiderTransform.up,
                out hit, 0.5f, 1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
            {
                if (!Physics.Raycast(hiderTransform.position, hiderTransform.forward,
               out hit, 1.5f, 1 << LayerMask.NameToLayer(GameDefine.Qiang_Layer)))
                {
                    Debug.DrawLine(hiderTransform.position, hiderTransform.position + hiderTransform.forward * 1.5f, Color.red);

                    return hiderTransform.forward;
                    //没有碰到物体
                }
                //没有碰到物体
            }
           


          

           
            //Debug.DrawRay(hit.point, hit.normal, Color.green);
            var xx = Vector3.Cross(hiderTransform.right, hit.normal).normalized;
            if (Vector3.Angle(hiderTransform.forward, xx) >= 85) return hiderTransform.forward;
            return xx;
        }
    }
}