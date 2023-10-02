
using MyEnum;
using SeekerStatus;
using System.Collections.Generic;
using UnityEngine;

namespace HiderStatus
{
    public class HiderThrowLayerStatusControl
    {
        public Hider hider;
        public int throwLayerId;
        private Dictionary<MyEnum.HiderStatusEnum, HiderStatusBase> statusDic;

        public HiderStatusBase nowStatus = null;
        public HiderStatusEnum nowStatusEnum;
        private Animator animator => hider.animator;
        public HiderThrowLayerStatusControl(Hider hider)
        {
            this.hider = hider;
            throwLayerId = MyTools.GetAnimatorLayerIdByLayerName(animator, GameDefine.AnimatorThrowLayerName);
            Init();
        }
        private void Init()
        {
            nowStatusEnum = HiderStatusEnum.None;
            statusDic = new Dictionary<HiderStatusEnum, HiderStatusBase>
            {
                { HiderStatusEnum.Throw, new ThrowStatus(this) },
                { HiderStatusEnum.TakeThrow, new TakeStatus(this) },
                { HiderStatusEnum.ThrowNone, new ThrowNoneStatus(this) },
            };
            this.SetStatus(HiderStatusEnum.ThrowNone);
        }
        public void OnOneGameEnd()
        {
            foreach (var x in this.statusDic.Values)
            {
                x.OnOneGameEnd();
            }
        }
        internal void Update()
        {

            nowStatus.OnUpdata();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetStatus(HiderStatusEnum.ThrowNone);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetStatus(HiderStatusEnum.TakeThrow);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetStatus(HiderStatusEnum.Throw);
            }


        }
        public void SetStatus(HiderStatusEnum statusEnum)
        {
            if (nowStatus != null && nowStatusEnum == statusEnum)
            {
                return;
            }
            //Debug.Log(hider.name+ "ÇÐ»»×´Ì¬£º" + statusEnum);
            if (nowStatus != null)
                nowStatus.OnEnd();
            nowStatus = this.statusDic[statusEnum];
            nowStatus.OnBegin();
            nowStatusEnum = statusEnum;
        }
        public void SetTakeStatus(string prefabsPath)
        {
            (statusDic[HiderStatusEnum.TakeThrow] as TakeStatus).prefabsPath = prefabsPath;
            SetStatus(HiderStatusEnum.TakeThrow);
        }
        public bool JudgeCanGetThrowObject()
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(throwLayerId);
            return stateInfo.IsName("None");
        }
    }
}