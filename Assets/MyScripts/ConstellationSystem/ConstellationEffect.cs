using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationEffect : MonoBehaviour
{
    public string effectName;
    public string effectDescription { get {
            if (EffectIdInSet == 0)
            {
                return DescriptionEffect();
            }
            else
            {
                if (ConstellationManager.Instance.IsConstellationActived(EffectIdInSet))
                { 
                    return "[已生效]" + DescriptionEffect();
                }
                else
                {
                    return "[未解锁]" + ConstellationManager.Instance.requires[EffectIdInSet-1].RequirementDescription;
                }
            }
        
        } }
    public int EffectIdInSet { get; set; }

    public virtual void ApplyEffect()
    {

    }

    public virtual string DescriptionEffect()
    {
        return null;
    }

}
