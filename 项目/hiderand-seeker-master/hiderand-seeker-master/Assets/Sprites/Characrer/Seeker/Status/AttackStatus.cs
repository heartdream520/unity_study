using UnityEngine;
using UnityEngineInternal;

namespace SeekerStatus
{
    public class AttackStatus : SeekerStatusBase
    {
        private float waitTime = GameDefine.SeekerZhuiJiTime-0.2f;
        public AttackStatus(SeekerStatusControl statusControl) : base(statusControl)
        {
            LookForHiderSpaceTime = 0;
        }
        private string setBoolString = "attack";

        public void AttackHider(GameObject hider)
        {
            //Debug.LogError("AttackStatus-> AttackHider");
            hider.GetComponent<Hider>().AfterGetAttack();
            GameingMainManager.Instance.attackIng=true;
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                GameingMainManager.Instance.attackIng = false;
            }, GameDefine.SeekerZhuiJiTime);
           
        }
        public override void OnBegin()
        {

            base.OnBegin();
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = 0;
            }
            waitTime = 1f;
            //Debug.LogError("AttackStatus-> OnBegin");
            animator.SetBool(setBoolString, true);

            if(!seeker.isAIInput&&
                GameingMainManager.Instance.
                GetHasBeenAttackHiderCount()>
                GameDefine.GameHiderCount-GameDefine.GameAddAttackCount)
            {
                AudioManager.Instance.PlayGetCoin();
                EffectManager.Instance.CreateOneJinBi(seeker.transform.position);
                seeker.GetMoney(GameDefine.PlayerHelpOtherMonty);
            }
            seeker.tanZhaoDeng.SetActive(false);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }
            //Debug.LogError("AttackStatus-> OnEnd");
            animator.SetBool(setBoolString, false);

           
          
        }

        public override void OnUpdata()
        {
            base.OnUpdata();

            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                return;
            }
            if (seeker.isAIInput)
            {
                seeker.aiInput.navMeshAgent.speed = 0;
            }
            if (GetAnimatorNowPlaying() == "idle")
                statusControl.SetStatus(MyEnum.SeekerStatusEnum.Idle);
            
        }
    }
}