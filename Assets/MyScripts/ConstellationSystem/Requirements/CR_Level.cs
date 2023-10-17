using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_Level : ConstellationRequirement
{
    public int requireNum;

    protected int currentNum;
    private void OnEnable()
    {
        GenMeowEvent.LevelUp += RequireLevel;
    }

    private void OnDisable()
    {
        GenMeowEvent.LevelUp -= RequireLevel;
    }

    public void RequireLevel()
    {
        if(GameCopilot.Instance.MeowStatus.SO.Level == requireNum)
        {
            this.ActiveConstellation();
        }
    }

    public override string SetRequirementDescription()
    {
        requirementDescription = "需要提升等级至" + requireNum + "级"  + "。";
        return requirementDescription;
    }

}
