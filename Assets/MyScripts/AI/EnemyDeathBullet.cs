using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathBullet : MonoBehaviour
{
    public Projectile projectile;

    protected AIBrain _brain;
    protected Transform _target;

    private void OnEnable()
    {
        _brain = GetComponent<AIBrain>();
    }

    public void Shoot()
    {
        _target = _brain.Target;
        Debug.Log("<color=yellow>子弹发射</color>");
        Projectile _projectile = Instantiate(projectile, transform.position, transform.rotation);
        _projectile.SetDirection((_target.position - this.transform.position).normalized, Quaternion.identity);
    }
}
