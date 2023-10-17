using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_UseSkillSpawn : ConstellationEffect
{
    public MMF_Player feedbacks;

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        GenMeowEvent.SubscribeCombatEvent(CombatEventTriggerTimeEnum.UseSkill, InstantiateEffect);
    }

    public void InstantiateEffect()
    {
        SkillSpawner.Instance.MeowSkill.damagePart.HitAnythingFeedback = feedbacks;
    }

    private void OnDisable()
    {
        GenMeowEvent.UseSkill -= InstantiateEffect;
    }

}
