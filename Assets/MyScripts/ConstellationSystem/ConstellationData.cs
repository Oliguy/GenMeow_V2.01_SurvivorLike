using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationData : MonoBehaviour
{
    public int ConstellationID;
    public string ConstellationName { get { return effect.effectName; } }
    public ConstellationRequirement requirement;
    public ConstellationEffect effect;

    public string RequirementDescription { get { if (requirement == null) return null; return this.requirement.SetRequirementDescription(); } }//TODO:可能需要更改，有一些是手动的。
    public string EffectDescription { get { if (effect == null) return null; return this.effect.effectDescription; } }

    public string ConstellationDescription
    {
        get
        {
            string _description = this.ConstellationName;
            if (this.requirement.isActived == false)
            {
                _description = "[未解锁]" + RequirementDescription + "\n";
            }
            else
            {
                _description = "[已生效]" + EffectDescription + "\n";
            }
            return _description;
        }
    }
}
