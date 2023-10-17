public static class EnumPropertyName 
{
    public static string GetPropertyName(MainProperty _mainproperty)
    {
        return _mainproperty switch
        {

            MainProperty.HP => "最大生命值",
            MainProperty.DamageBase => "攻击力",
            MainProperty.Defence => "防御力",
            MainProperty.DamageGlobal => "伤害",

            MainProperty.HPRegeneration => "生命复苏",
            MainProperty.HPSteal => "生命窃取",

            MainProperty.AttackSpeed => "攻击速度",
            MainProperty.CritChance => "暴击几率",
            MainProperty.Range => "攻击范围",
            MainProperty.Speed => "移动速度",
            MainProperty.Luck => "幸运",
            MainProperty.Harvesting => "收获",

            MainProperty.ElementMaster => "元素精通",
            MainProperty.ElementEffeciency => "元素充能效率",
            MainProperty.ElementBurstRange => "元素影响范围",

            _ => ""
        };
    }

}
