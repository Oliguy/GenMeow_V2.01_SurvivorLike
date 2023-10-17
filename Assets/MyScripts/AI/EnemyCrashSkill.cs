using DG.Tweening;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyCrashSkill : MonoBehaviour
{
    public int skillDamage;
    public float skillRadius;
    public float skillCoolDown;
    //public int crashSpeed;
    public float crashDuration;
    public ParticleSystem dazeParticle;
    public ParticleSystem dustParticle;

    public Animator anim;

    protected float _timer;

    protected AIBrain _brain;
    protected Transform _target;
    protected CharacterMovement _movement;
    protected bool isUsingSkill;

    private void OnEnable()
    {
        _brain = GetComponent<AIBrain>();
        _movement = GetComponent<CharacterMovement>();
        _timer = skillCoolDown;
    }
    private void FixedUpdate()
    {
        if (isUsingSkill) return;
        _timer += Time.fixedDeltaTime;
        if (_brain.Target == null)
        {
            return;
        }
        else
        {
            _target = _brain.Target;
        }

        if (_timer < skillCoolDown)
            return;

        if (Vector3.Distance(this.transform.position, _target.position) < skillRadius)
        {
            _timer = 0f;
            StartCoroutine(Crash());
        }

    }

    protected IEnumerator Crash()
    {
        isUsingSkill = true;
        _movement.ShouldSetMovement = false;
        anim.SetBool("isPreparingCrash", true);
        Debug.Log("准备开始冲撞！！！");
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("isPreparingCrash", false);
        anim.SetBool("isCrashing", true);
        dustParticle?.Play();
        Vector3 _dir = (_target.position - this.transform.position).normalized;
        transform.DOMove(_dir* skillRadius * 1.5f, crashDuration).SetEase(Ease.OutQuad);
        dazeParticle?.gameObject.SetActive(true);
        dazeParticle?.Play();
        yield return new WaitForSeconds(crashDuration);
        //dazeParticle?.Play();
        Debug.Log("冲撞完成歇歇！！！");
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("isCrashing", false);
        dazeParticle?.Stop();
        dazeParticle?.gameObject.SetActive(false);
        _movement.ShouldSetMovement = true;


        isUsingSkill = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, skillRadius);
    }



}
