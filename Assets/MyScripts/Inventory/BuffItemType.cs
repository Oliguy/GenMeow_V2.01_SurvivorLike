using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItemType : ItemTypeDataDetails
{
    public List<ItemMainProperty> itemMainPropertyList;
    public void ItemLoad(Meow_SO _meow)
    {
        if(itemMainPropertyList != null)
        {
            foreach(ItemMainProperty property in itemMainPropertyList)
            {
                switch (property.mainProperty)
                {
                    case MainProperty.HP:
                        _meow.MaxHealth += property.propertyValue;
                        break;
                    case MainProperty.DamageBase:
                        _meow.DamageBase += property.propertyValue;
                        break;
                    case MainProperty.Defence:
                        _meow.Defence += property.propertyValue;
                        break;

                    case MainProperty.DamageGlobal:
                        _meow.DamageGlobal += property.propertyValue;
                        break;
                    case MainProperty.HPRegeneration:
                        _meow.HPRegeneration += property.propertyValue;
                        break;
                    case MainProperty.HPSteal:
                        _meow.HPSteal += property.propertyValue;
                        break;
                    case MainProperty.AttackSpeed:
                        _meow.AttackSpeed += property.propertyValue;
                        break;
                    case MainProperty.CritChance:
                        _meow.CritChance += property.propertyValue;
                        break;
                    case MainProperty.Range:
                        _meow.Range += property.propertyValue;
                        break;
                    case MainProperty.Speed:
                        _meow.Speed += property.propertyValue;
                        break;
                    case MainProperty.Luck:
                        _meow.Luck += property.propertyValue;
                        break;
                    case MainProperty.Harvesting:
                        _meow.Harvesting += property.propertyValue;
                        break;
                    case MainProperty.ElementMaster:
                        _meow.ElementMaster += property.propertyValue;
                        break;
                    case MainProperty.ElementEffeciency:
                        _meow.ElementEffeciency += property.propertyValue;
                        break;
                    case MainProperty.ElementBurstRange:
                        _meow.ElementBurstRange += property.propertyValue;
                        break;

                }
            }
        }
    }

}
