using UnityEngine;
using UnityEngine.Windows;

namespace HiderStatus
{
    public class BeenAttackStatus : HiderStatusBase
    {
        public BeenAttackStatus(HiderStatusControl statusControl) : base(statusControl)
        {

        }

        private string setBoolString = "beenAttack";
        public override void OnBegin()
        {
            base.OnBegin();
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = 0;
            }
            animator.SetBool(setBoolString, true);
            //被攻击后转变到不能被攻击的层
            hider.gameObject.layer = LayerMask.NameToLayer(GameDefine.CanNotAttack_Layer);
            //Debug.LogWarning($"{this.name} 被攻击，设置为 CanNotAttack_Layer");
            hider.CanNotMove();
            if (hider.isAIInput)
            {
                hider.aiInput.CanInput = false;
                hider.aiInput.navMeshAgent.speed = 0;
            }
            else
            {
                hider.playerInput.CanInput = false;
            }
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
            {
                if (hider != null)
                    EffectManager.Instance.CreateAttackYan(hider.transform.position);
            }, GameDefine.PlayerCagePlayTime);
            InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
        {
            if (hider == null) return;

            hider.longZiObject = ObjectPoolManager.Instance
            .GetOneObjectFromPool(hider.playerCageGameObject);
            hider.longZiObject.GetComponent<PlayerCage>().InitPlayerCage(hider);
            hider.longZiObject.transform.SetParent(hider.transform, false);
            hider.longZiObject.transform.localPosition = Vector3.zero;
        }, GameDefine.PlayerCagePlayTime);
            if (!hider.isZang)
                hider.SetMaterBase();
        }

        public override void OnEnd()
        {
            base.OnEnd();
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed =GameDefine.NavMeshAgentSpeed;
            }
            animator.SetBool(setBoolString, false);
            hider.isCanMove = true;
            if (hider.isAIInput)
            {
                hider.aiInput.CanInput = true;
                hider.aiInput.navMeshAgent.speed = GameDefine.NavMeshAgentSpeed;
            }
            else
            {
                hider.playerInput.CanInput = true;
            }
            hider.gameObject.layer = LayerMask.NameToLayer(GameDefine.Player_Layer);
           


        }

        public override void OnUpdata()
        {
            base.OnUpdata();
            if (hider.isAIInput)
            {
                hider.aiInput.navMeshAgent.speed = 0;
            }
        }
    }
}