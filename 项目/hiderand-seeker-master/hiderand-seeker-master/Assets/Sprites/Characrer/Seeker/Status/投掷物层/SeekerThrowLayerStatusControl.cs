
using MyEnum;
using SeekerStatus;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SeekerStatus
{
    public class SeekerThrowLayerStatusControl
    {
        public Seeker seeker;
        public int throwLayerId;
        private Dictionary<MyEnum.SeekerStatusEnum, SeekerStatusBase> statusDic;

        public SeekerStatusBase nowStatus = null;
        public SeekerStatusEnum nowStatusEnum;
        private Animator animator => seeker.animator;
        public SeekerThrowLayerStatusControl(Seeker seeker)
        {
            this.seeker = seeker;
            throwLayerId = MyTools.GetAnimatorLayerIdByLayerName(animator, GameDefine.AnimatorThrowLayerName);
            Init();
        }
        private void Init()
        {
            nowStatusEnum = MyEnum.SeekerStatusEnum.None;
            statusDic = new Dictionary<SeekerStatusEnum, SeekerStatusBase>
            {
                { SeekerStatusEnum.Throw, new ThrowStatus(this) },
                { SeekerStatusEnum.TakeThrow, new TakeStatus(this) },
                { SeekerStatusEnum.ThrowNone, new ThrowNoneStatus(this) },
            };
            this.SetStatus(SeekerStatusEnum.ThrowNone);
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
            /*
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetStatus(SeekerStatusEnum.ThrowNone);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetStatus(SeekerStatusEnum.TakeThrow);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetStatus(SeekerStatusEnum.Throw);
            }
            */


        }
        public void SetStatus(SeekerStatusEnum statusEnum)
        {
            if (nowStatus != null && nowStatusEnum == statusEnum)
            {
                return;
            }
            //Debug.Log(seeker.name + "ÇÐ»»×´Ì¬£º" + statusEnum);
            if (nowStatus != null)
                nowStatus.OnEnd();
            nowStatus = this.statusDic[statusEnum];
            nowStatus.OnBegin();
            nowStatusEnum = statusEnum;
        }

        public void SetTakeThrowStatus(string prefabsPath)
        {
            (statusDic[SeekerStatusEnum.TakeThrow] as TakeStatus).prefabsPath = prefabsPath;
            SetStatus(SeekerStatusEnum.TakeThrow);
        }
        public bool JudgeCanGetThrowObject()
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(throwLayerId);
            return stateInfo.IsName("None");
        }
    }
}