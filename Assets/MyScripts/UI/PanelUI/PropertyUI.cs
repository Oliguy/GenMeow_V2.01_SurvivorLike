using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyUI : MonoBehaviour
{
    public Text HP;
    public Text DamageBase;
    public Text Defence;

    public Text DamageGlobal;
    public Text HPRegeneration;
    public Text HPSteal;
    public Text AttackSpeed;
    public Text CritChance;
    public Text Range;
    public Text Speed;
    public Text Luck;
    public Text Harvesting;

    public Text ElementMaster;//技能伤害
    public Text ElementEffeciency;//技能充能效率
    public Text ElementBurstRange;//技能范围

    protected Meow_SO _so;

    private void OnEnable()
    {
        Debug.Log("<color=green>属性面板被调用</color>");
        _so = GenMeowInventoryManager.Instance._inventory.meowSO;
        if (_so == null)
        {
            Debug.LogError("SO为空，无法更新属性，请检查！");
            return;
        }
        RefreshValue();
    }

    public void RefreshValue()
    {
        GetPropertyValue(HP, _so.MaxHealth);
        GetPropertyValue(DamageBase, _so.DamageBase);
        GetPropertyValue(Defence, _so.Defence);
        GetPropertyValue(DamageGlobal, _so.DamageGlobal);

        GetPropertyValue(HPRegeneration, _so.HPRegeneration);
        GetPropertyValue(HPSteal, _so.HPSteal);

        GetPropertyValue(AttackSpeed, _so.AttackSpeed);
        GetPropertyValue(CritChance, _so.CritChance);
        GetPropertyValue(Range, _so.Range);
        GetPropertyValue(Speed, _so.Speed);
        GetPropertyValue(Luck, _so.Luck);
        GetPropertyValue(Harvesting, _so.Harvesting);
        GetPropertyValue(ElementMaster, _so.ElementMaster);
        GetPropertyValue(ElementEffeciency, _so.ElementEffeciency);
        GetPropertyValue(ElementBurstRange, _so.ElementBurstRange);
    }

    public void GetPropertyValue(Text _targetTxt,int valueNum)
    {
        if (_targetTxt == null)
        {
            Debug.Log(_targetTxt.name + "为空，请检查！");
            return;
        }
        _targetTxt.text = valueNum.ToString();      
    }


}
