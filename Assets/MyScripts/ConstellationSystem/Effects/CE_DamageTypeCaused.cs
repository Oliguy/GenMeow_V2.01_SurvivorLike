using MoreMountains.Feedbacks;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_DamageTypeCaused : ConstellationEffect
{
    public DamageTypeEnum damageType;
    public int damageAmount;
    public MMF_Player feedbacks;
    protected MMF_Player _feedbacks;
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        GenMeowEvent.DamageTypeEventSubscribe(damageType, Effect);
        _feedbacks = Instantiate(feedbacks, transform);
    }

    public virtual void Effect(Health _health)
    {
        _health.Damage(damageAmount, GameCopilot.Instance.MeowStatus.gameObject, 0, 0,Vector3.zero);
        //_health.CurrentHealth -= damageAmount;
        _feedbacks.PlayFeedbacks(_health.gameObject.transform.position);
    }

    private void OnDisable()
    {
        GenMeowEvent.DamageTypeEventDisSubscribe(damageType, Effect);
    }

    public override string DescriptionEffect()
    {
        //TODO:Enum名 时 生成 feedbacks 名
        return "技能命中时生成" + feedbacks.name;
    }
}
