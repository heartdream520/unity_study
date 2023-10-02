

using UnityEngine;

namespace HiderStatus
{
    public class TakeStatus : HiderStatusBase
    {

        private string setBoolString = "take";
        private GameObject throwObjce;
        public TakeStatus(HiderThrowLayerStatusControl statusControl) : base(statusControl)
        {

        }
        public string prefabsPath;
        public override void InitStatus()
        {
            base.InitStatus();
           

        }
   
        public override void OnBegin()
        {
            base.OnBegin();
            animator.SetBool(setBoolString, true);
            animator.SetLayerWeight(throwStatusControl.throwLayerId, 1);
            throwObjce= PlayerObjectManager.Instance.
                        CreateOneGameObjectInObjectPosName(hider.gameObject, prefabsPath,
                        GameDefine.PlayerThrowObjectPosNameString);
            throwObjce.transform.localRotation = Quaternion.identity;

            throwObjce.GetComponent<PlayerThrowProp>().Init(hider);
            throwObjce.GetComponent<PlayerThrowProp>().throwJianCe.OnTriggerEnterAction 
                += this.OnTriggerEnter;

            //throwObjce.transform.LookAt(Vector3.up);
            //throwObjce.transform.set = Vector3.one;
            PlayerObjectManager.Instance.SetScaleFromGoToEnd(throwObjce, hider.gameObject);

        }

        
        public override void OnEnd()
        {
            base.OnEnd();
            throwObjce.GetComponent<PlayerThrowProp>().throwJianCe.OnTriggerEnterAction
                -= this.OnTriggerEnter;
            animator.SetBool(setBoolString, false);
            throwObjce.GetComponent<PlayerThrowProp>().
                ThrowToCharcater(throwToSeeker);
            //ObjectPoolManager.Instance.SetOneObjectInPool(throwObjce);
        }
        private Seeker throwToSeeker;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != GameDefine.SeekerTag) return;

            if (Mathf.Abs(other.transform.position.y - hider.transform.position.y) > 1.5f)
            {
                return;
            }

            this.throwToSeeker = other.GetComponent<Seeker>();
            throwStatusControl.SetStatus(MyEnum.HiderStatusEnum.Throw);
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