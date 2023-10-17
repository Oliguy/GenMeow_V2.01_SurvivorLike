using MoreMountains.TopDownEngine;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

public static class GenMeowEvent 
{
    public static event Action MainMenu;
    public static void CallMainMenu()
    {
        MainMenu?.Invoke();
    }


    public static event Action GameStart;
    public static void CallGameStart()
    {
        GameStart?.Invoke();
    }


    public static event Action WaveStart;
    public static void CallWaveStart()
    {
        WaveStart?.Invoke();
    }

    public static event Action AfterWaveStart;
    public static void CallAfterWaveStart()
    {
        AfterWaveStart?.Invoke();
    }

    public static event Action WaveEnd;
    public static void CallWaveEnd()
    {
        WaveEnd?.Invoke();
    }


    public static event Action WaveVictory;
    public static void CallWaveVictory()
    {
        WaveVictory?.Invoke();
    }

    public static event Action LevelUp;
    public static void CallLevelUp()
    {
        LevelUp?.Invoke();
    }

    public static event Action<bool> RefreshStatus;
    public static void CallRefreshStatus(bool ifGameStart = false)
    {
        RefreshStatus?.Invoke(ifGameStart);
    }


    public static event Action<ItemData> AddItem2Inventory;
    public static void CallAddItem2Inventory(ItemData _itemData)
    {
        AddItem2Inventory?.Invoke(_itemData);
    }

    #region UI组件处理
    public static event Action DestroyCardEvent;
    public static void CallDestroyCardEvent()
    {
        DestroyCardEvent?.Invoke();
    }

    public static event Action DestroyAllBuffItemUIElement;
    public static void CallDestroyAllBuffItemUIElement()
    {
        DestroyAllBuffItemUIElement?.Invoke();
    }


    public static event Action DestroyAllMeowBallUIElement;
    public static void CallDestroyAllMeowBallUIElement()
    {
        DestroyAllMeowBallUIElement?.Invoke();
    }

    public static event Action<int> UpdateTime;
    public static void CallUpdateTime(int _time)
    {
        UpdateTime?.Invoke(_time);
    }

    public static event Action<float> UpdateSkillFilling;
    public static void CallUpdateSkillFilling(float _percentage)
    {
        UpdateSkillFilling?.Invoke(_percentage);
    }

    public static event Action UpdateItemBag;
    public static void CallUpdateItemBag()
    {
        UpdateItemBag?.Invoke();
    }

    public static event Action UpdateMola;
    public static void CallUpdateMola()
    {
        UpdateMola?.Invoke();
    }

    #endregion


    public static event Action UseSkill;
    public static void CallUseSkill()
    {
        UseSkill?.Invoke();
    }

    #region 战斗系统

    private static Dictionary<CombatEventTriggerTimeEnum, Action> combatEventDictionary = new Dictionary<CombatEventTriggerTimeEnum, Action>();
    public static void SubscribeCombatEvent(CombatEventTriggerTimeEnum _eventTime, Action _funcPercentageNum)
    {
        if (combatEventDictionary.ContainsKey(_eventTime))
        {
            combatEventDictionary[_eventTime] += _funcPercentageNum;
        }
        else
        {
            combatEventDictionary.Add(_eventTime, _funcPercentageNum);
        }
    }
    public static void ClearAllCombatEventSubscriptions()
    {
        combatEventDictionary.Clear();
    }
    public static void TriggerCombatEvent(CombatEventTriggerTimeEnum _eventTriggerTime)
    {
        if (combatEventDictionary.ContainsKey(_eventTriggerTime))
        {
            combatEventDictionary[_eventTriggerTime]?.Invoke();
        }
    }


    #endregion


    #region DamageType
    private static Dictionary<DamageTypeEnum, Action<Health>> damageTypeEventDictory = new Dictionary<DamageTypeEnum, Action<Health>>();
    public static void DamageTypeEventSubscribe(DamageTypeEnum _enum,Action<Health> _health)
    {
        Debug.Log("<color=yellow>" + _enum + "成功订阅!</color>");
        if (damageTypeEventDictory.ContainsKey(_enum))
        {
            damageTypeEventDictory[_enum] += _health;
            
        }
        else
        {
            damageTypeEventDictory.Add(_enum, _health);
        }
    }
    public static void ClearAllDamageTypeEventSubscriptions()
    {
        damageTypeEventDictory.Clear();
    }
    public static void TriggerDamageTypeEvent(DamageTypeEnum _enum,Health _health)
    {
        
        if (damageTypeEventDictory.ContainsKey(_enum))
        {

            Debug.Log($"Invoking event handler for damage type {_enum}: {damageTypeEventDictory[_enum]}");
            damageTypeEventDictory[_enum]?.Invoke(_health);
        }
        
    }


    public static void DamageTypeEventDisSubscribe(DamageTypeEnum _enum, Action<Health> _health)
    {
        
        if (damageTypeEventDictory.ContainsKey(_enum))
        {

            damageTypeEventDictory[_enum] -= _health;
        }
        
    }
    #endregion

    #region 命座激活
    public static event Action<ConstellationData> ContellationActive;
    public static void CallConstellationActive(ConstellationData _contellationdata)
    {
        ContellationActive.Invoke(_contellationdata);
    }
    #endregion
}
