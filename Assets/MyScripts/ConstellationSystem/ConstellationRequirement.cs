using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationRequirement : MonoBehaviour
{
    public string RequirementDescription { 
        set { requirementDescription = value; }
        get {
            if (requirementDescription == "") 
            {
                return SetRequirementDescription();
            }
            Debug.Log("正在获取需求描述！" + requirementDescription);
            return requirementDescription;
        }
    }
    public string requirementDescription;
    public int RequireIdInSet;
    public bool isActived = false;

    public virtual void ActiveConstellation()
    {
        if(isActived) return;
        isActived = true;
        ConstellationManager.Instance?.ActiveConstellation(RequireIdInSet);
    }

    public virtual string SetRequirementDescription()
    {
        return null;
    }
}
