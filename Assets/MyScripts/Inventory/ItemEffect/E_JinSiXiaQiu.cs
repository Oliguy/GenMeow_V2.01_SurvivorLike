using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_JinSiXiaQiu : MeowStatusItem
{
    public override void ProbabilityEvent()
    {
        base.ProbabilityEvent();
        SkillSpawner.Instance.Timer += SkillSpawner.Instance.CoolDown * 0.1f;
        Debug.Log("<color=yellow>金丝虾球效果执行</color>");
    }


}
