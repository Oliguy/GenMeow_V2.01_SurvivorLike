using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowStatusItem : MonoBehaviour
{
    public CombatEventTriggerTimeEnum triggerTime;
    public int Percentage = 100;
    /// <summary>
    /// 特殊效果道具专用。在游戏的各种时机，比如使用技能时等，在事件系统种订阅效果。
    /// </summary>
    public void AddEvent2System()
    {
        GenMeowEvent.SubscribeCombatEvent(triggerTime, ProbabilityEvent);
    }

    public virtual void ProbabilityEvent()
    {
        int _randomNum = Random.Range(0, 100);
        if (Percentage < _randomNum)
        {
            return;
        }

        Debug.Log("<color=yellow>" +gameObject.name + "效果执行 " + _randomNum + "</color>");
    }

}
