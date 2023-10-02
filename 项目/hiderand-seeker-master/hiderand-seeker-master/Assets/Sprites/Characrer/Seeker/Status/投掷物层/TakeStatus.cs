

using UnityEngine;

namespace SeekerStatus
{
    public class TakeStatus : SeekerStatusBase
    {

        private string setBoolString = "take";
        public string prefabsPath;
        private GameObject throwObjce;
        public TakeStatus(SeekerThrowLayerStatusControl statusControl) : base(statusControl)
        {

        }

        public override void InitStatus()
        {
            base.InitStatus();


        }

        public override void OnBegin()
        {
            base.OnBegin();
            animator.SetBool(setBoolString, true);
            animator.SetLayerWeight(throwStatusControl.throwLayerId, 1);
            throwObjce = PlayerObjectManager.Instance.
                         CreateOneGameObjectInObjectPosName(seeker.gameObject, prefabsPath,
                         GameDefine.PlayerThrowObjectPosNameString);
            throwObjce.GetComponent<PlayerThrowProp>().Init(seeker);

            

            throwObjce.GetComponent<PlayerThrowProp>().throwJianCe.OnTriggerEnterAction
                += this.OnTriggerEnter;
            

            PlayerObjectManager.Instance.SetScaleFromGoToEnd(throwObjce, seeker.gameObject);
        }

        private Hider throwToHider;
        public override void OnEnd()
        {
            base.OnEnd();
            throwObjce.GetComponent<PlayerThrowProp>().throwJianCe.OnTriggerEnterAction
                -= this.OnTriggerEnter;
            animator.SetBool(setBoolString, false);

            throwObjce.GetComponent<PlayerThrowProp>().
                ThrowToCharcater(throwToHider);

            //ObjectPoolManager.Instance.SetOneObjectInPool(throwObjce);
        }
        private void OnTriggerEnter(Collider other)
        {
            //return;
            if (other.tag != GameDefine.HiderTag) return;
            if (Mathf.Abs(other.transform.position.y - seeker.transform.position.y) > 1f)
            {
                return;
            }
            this.throwToHider = other.GetComponent<Hider>();
            if (throwToHider.HasBeenAttack) return;
            if (throwToHider.isZang) return;
            throwStatusControl.SetStatus(MyEnum.SeekerStatusEnum.Throw);
        }

        public override void OnOneGameEnd()
        {
            base.OnOneGameEnd();

        }
        public override void OnUpdata()
        {
            base.OnUpdata();
        }
    }
}