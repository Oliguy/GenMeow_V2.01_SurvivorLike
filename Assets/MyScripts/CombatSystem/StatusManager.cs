using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class StatusManager : MMSingleton<StatusManager>
{
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
        GenMeowEvent.GameStart += ClearAllCombatEvent;
        GenMeowEvent.MainMenu += AutoDestroy;
    }

    public void NewPercentageEvent()
    {

    }


    public void ClearAllCombatEvent()
    {
        GenMeowEvent.ClearAllCombatEventSubscriptions();
        foreach(Transform _t in transform)
        {
            Destroy(_t.gameObject);
        }
    }

    private void AutoDestroy()
    {
        Destroy(this);
    }

    private void OnDisable()
    {
        GenMeowEvent.GameStart -= ClearAllCombatEvent;
        GenMeowEvent.MainMenu -= AutoDestroy;
    }
}
