using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoManager : MMSingleton<CharacterInfoManager>
{
    public MeowInventorySO meowInfo_SO;

    public CharacterTemplate Character 
    {   get { return _character; }
        set {
            foreach(Transform _trans in transform)
            {
                Destroy(_trans.gameObject);
            }

            _character = Instantiate(value, transform);
            meowInfo_SO.meowCharacterTmp = _character;
        } 
    }
    protected CharacterTemplate _character;


    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // 如果已经有一个实例存在并且不是当前实例，那么销毁当前实例
            Destroy(gameObject);
        }
        else
        {
            // 否则，这个实例就是我们的单例，不要在加载新场景时销毁它。
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }


    public void TemporaryPropertyChange(ItemMainProperty property)
    {
        Meow_SO _meow = GameCopilot.Instance.MeowStatus.SO;
        ChangeProperty(property, _meow);
        GenMeowEvent.CallRefreshStatus(false);
    }

    public void PermanentPropertyChange(ItemMainProperty property)
    {
        Meow_SO _meow = GenMeowInventoryManager.Instance._inventory.meowSO;
        ChangeProperty(property, _meow);
        GenMeowEvent.CallRefreshStatus(false);
        MessageUIManager.Instance.PropertyUpdate(property);
    }

    public void ChangeProperty(ItemMainProperty property , Meow_SO _meow)
    {
        //TODO:InfoManager
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
