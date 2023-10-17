using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MeowSkillBase : MonoBehaviour
{
    //TODO:参数
    public int skillID;
    public float coolDown;
    public int baseDamage;
    public int baseProjectileNum;
    public int baseReleaseTime;
    public int baseRange;
    public Sprite skillIcon;
    public AudioClip skillSound;
    public DamageOnTouch damagePart;
    
    protected float _range;



    public virtual void ActiveSkill(float damageBonus = 0, float projectileNumBonus = 0, float releaseTimeBonus = 0, float rangeBonus = 0f)
    {
        Debug.Log("技能释放");
    }
}
