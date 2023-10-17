using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Projectile projectile;
    public MeowBallType meowBall;
    public ProjectileTargetMode projectileTargetMode;

    protected DamageOnTouch _damage;

    void OnEnable()
    {
        projectile = GetComponent<Projectile>();
        _damage = GetComponent<DamageOnTouch>();
        Init();
    }

    public void Init()
    {
        SetProjectileAttackTarget();
    }


    public void SetProjectileAttackTarget()
    {
        switch (projectileTargetMode)
        {
            case ProjectileTargetMode.Nearest:
                Vector3 _dir = (EnemyManager.Instance.NearestEnemyPos(transform.position) - transform.position).normalized;
                projectile.SetDirection(_dir, Quaternion.identity);
                break;
        }
    }

    public void SetDamage()
    {
        _damage.MinDamageCaused = meowBall.finalDamage;
        _damage.MaxDamageCaused = meowBall.finalDamage;
    }

    public void SetMeowBall(MeowBallType _meowBall)
    {
        this.meowBall = _meowBall;
    }
}
