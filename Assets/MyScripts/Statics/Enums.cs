
[System.Serializable]
public enum ItemRarity
{
    Gray,Green,Blue,Purple,Golden
}

[System.Serializable]
public enum ItemType
{
    BuffItem, MeowBall, Consumable
}

[System.Serializable]
public enum MainProperty
{
    HP,
    DamageBase,
    Defence,

    DamageGlobal,
    HPRegeneration,
    HPSteal,
    AttackSpeed,
    CritChance,
    Range,
    Speed,
    Luck,
    Harvesting, 

    ElementMaster,//技能伤害
    ElementEffeciency,//技能充能效率
    ElementBurstRange,//技能范围
}

[System.Serializable]
public enum SecondaryProperty
{

}

[System.Serializable]
public enum DamageBonus
{
    MeowBaseDamage,
    MeowElementMaster,
    MeowHP,
    MeowDefence,
    MeowCrit,
    MeowLuck,
    MeowRange
}

[System.Serializable]
public enum MeowBallAttackType
{
    Melee,
    Ranged
}

[System.Serializable]
public enum MeleeMeowBallAttackMethod
{
    Stick,
    Swap
}


[System.Serializable]
public enum RangedMeowBallAttackMethod
{
    Projectile,
    Razer
}

[System.Serializable]
public enum ProjectileTargetMode
{
    Nearest,
    Random,
}


[System.Serializable]
public enum CombatEventTriggerTimeEnum
{
    UseSkill,
    GetHurt,
    CauseDamage,
    GainMola,
    WaveStart
}

[System.Serializable]
public enum BuffEnum
{
    ChangeMeowStatus,
    GainMola,
    SkillCoolDownPercent
}
