using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InherentConstellationData : MonoBehaviour
{
    public int ConstellationID;
    public ConstellationEffect effect;
    public string EffectDescription { get { if (effect == null) return null; return this.effect.effectDescription; } }
}
