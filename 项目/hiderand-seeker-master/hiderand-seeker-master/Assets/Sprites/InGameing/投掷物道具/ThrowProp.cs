
using UnityEngine;

public class ThrowProp : MonoBehaviour
{
    /// <summary>
    /// 旋转速度
    /// </summary>
    public float aroundSpeed;
    /// <summary>
    /// 投掷物的名字
    /// </summary>
    public string throwPropNameString;

    public bool canGet = false;
    private string prefabsPath => PathDefine.ThrowPropPrefabsPath + throwPropNameString;

    public bool isDesTory;
    private void Update()
    {
        this.transform.RotateAround(transform.position, transform.up, aroundSpeed * Time.deltaTime);
    }
    private void OnEnable()
    {
        isDesTory = false;
        EventManager.Instance.BeforeTimeBeginFlowAction += OnBeforeTimeBeginFlow;
    }
    private void OnDisable()
    {
        EventManager.Instance.BeforeTimeBeginFlowAction -= OnBeforeTimeBeginFlow;

    }

    private void OnBeforeTimeBeginFlow()
    {
        canGet = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Chick(other);


    }
    private void OnTriggerStay(Collider other)
    {
        Chick(other);
    }
    private void Chick(Collider other)
    {
        if (!canGet) return;
        //ReviveObjectManager.Instance.ReviveOneObjectAfterSecend(gameObject, GameDefine.ThrowObjectReviveTime);
        CharacterBase characterBase;
        if (GameingMainManager.Instance.oneGameIsEnd) return;
        if (!other.TryGetComponent(out characterBase))
        {
            return;
        }
        if (characterBase.isAIInput) return;
        if (characterBase is Hider)
        {
            Hider hider = characterBase as Hider;
            if (hider.isAIInput) return;

            if (!hider.throwLayerStatusControl.JudgeCanGetThrowObject())
            {
                return;
            }
            isDesTory = true;
            hider.throwLayerStatusControl.SetTakeStatus(prefabsPath);
            ReviveObjectManager.Instance.ReviveOneObjectAfterSecend
                (this.gameObject, GameDefine.ThrowObjectReviveTime);
            //播放拿起投掷物声音
            if (!hider.isAIInput)
                AudioManager.Instance.PlayTakeTouZhiWu();

        }
        else if (characterBase is Seeker)
        {
            Seeker seeker = characterBase as Seeker;
            if (seeker.isAIInput) return;
            if (!seeker.throwLayerStatusControl.JudgeCanGetThrowObject())
            {
                return;
            }
            isDesTory = true;
            seeker.throwLayerStatusControl.SetTakeThrowStatus(prefabsPath);
            ReviveObjectManager.Instance.ReviveOneObjectAfterSecend
               (this.gameObject, GameDefine.ThrowObjectReviveTime);

            //播放拿起投掷物声音
            if (!seeker.isAIInput)
                AudioManager.Instance.PlayTakeTouZhiWu();

        }
    }
}