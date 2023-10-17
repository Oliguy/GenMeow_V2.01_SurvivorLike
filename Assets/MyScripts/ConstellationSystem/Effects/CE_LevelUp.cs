using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_LevelUp : ConstellationEffect
{
    public List<ItemMainProperty> mainPropertyBuffs;
    public override void ApplyEffect()
    {
        GenMeowEvent.LevelUp += Effect;
    }

    protected void Effect()
    {
        ConstellationBuff(GenMeowInventoryManager.Instance._inventory.meowSO);
        GenMeowEvent.CallRefreshStatus();
    }

    public override string DescriptionEffect()
    {
        string _des = "等级提升时，";

        foreach (var _buff in mainPropertyBuffs)
        {
            _des += "增加" + EnumPropertyName.GetPropertyName(_buff.mainProperty) + _buff.propertyValue + "点。";
        }
        return _des;

    }
    private void OnDisable()
    {
        GenMeowEvent.LevelUp -= Effect;
    }
    public void ConstellationBuff(Meow_SO _meow)
    {
        if (mainPropertyBuffs != null)
        {
            foreach (ItemMainProperty property in mainPropertyBuffs)
            {
                CharacterInfoManager.Instance.PermanentPropertyChange(property);
            }
        }
    }
}
