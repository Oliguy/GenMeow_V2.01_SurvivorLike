using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mola : MeowLoot
{

    public int molaNum = 1;
    protected override void TargetEnter()
    {
        base.TargetEnter();
        GenMeowInventoryManager.Instance.GainMola(molaNum);
        GenMeowEvent.TriggerCombatEvent(CombatEventTriggerTimeEnum.GainMola);
        feedback.PlayFeedbacks();
    }



}
