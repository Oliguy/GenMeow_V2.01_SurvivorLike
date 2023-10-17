using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum DamageTypeEnum
{
    Common,
    Melee,
    Ranged,
    Skill
}

public class MeowDamageType : MonoBehaviour
{
    public DamageTypeEnum damageType;


    protected DamageOnTouch _damageOnTouch;

    private void OnEnable()
    {
        if(_damageOnTouch == null)
        {
            _damageOnTouch = GetComponentInParent<DamageOnTouch>();
        }
        //_damageOnTouch.HitDamageableEvent.AddListener(DamageEffect);
    }

    public virtual void DamageEffect(Health _targetHealth)
    {
        GenMeowEvent.TriggerDamageTypeEvent(damageType, _targetHealth);
        Debug.Log("触发伤害事件" + _targetHealth.gameObject.name);
    }

}
