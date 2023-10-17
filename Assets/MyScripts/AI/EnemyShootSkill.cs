using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootSkill : MonoBehaviour
{
    public int skillDamage;
    public float skillRange;
    public float skillCoolDown;
    public Projectile projectile;


    protected float _timer;

    protected AIBrain _brain;
    protected Transform _target;

    private void OnEnable()
    {
        _brain = GetComponent<AIBrain>();
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if (_brain.Target == null)
        {
            return;
        }
        else
        {
            _target = _brain.Target;
        }

        if(_timer < skillCoolDown)
            return;

        if(Vector3.Distance(this.transform.position, _target.position) < skillRange)
        {
            _timer = 0f;
            Shoot();
        }

    }

    protected void Shoot()
    {
        Debug.Log("<color=yellow>子弹发射</color>");
        Projectile _projectile = Instantiate(projectile, transform.position, transform.rotation);
        _projectile.SetDirection((_target.position - this.transform.position).normalized,Quaternion.identity);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position,skillRange);
    }

}
