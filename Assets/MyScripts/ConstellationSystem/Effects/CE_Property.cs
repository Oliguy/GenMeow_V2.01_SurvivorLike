using MoreMountains.InventoryEngine;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_Property : ConstellationEffect
{
    public List<ItemMainProperty> mainPropertyBuffs;

    public override void ApplyEffect()
    {
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        yield return new WaitForSeconds(0.3f);

        ConstellationBuff(GenMeowInventoryManager.Instance._inventory.meowSO);
        GenMeowEvent.CallRefreshStatus();
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



    public override string DescriptionEffect()
    {
        string _des = "";
        foreach(var _buff in mainPropertyBuffs)
        {
            _des += "增加" + EnumPropertyName.GetPropertyName(_buff.mainProperty) + _buff.propertyValue + "点。";
        }
        return _des;
    }
}
