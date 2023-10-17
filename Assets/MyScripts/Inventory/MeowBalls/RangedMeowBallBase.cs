using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedMeowBallBase : MeowBallType
{
    public RangedMeowBallAttackMethod attackMethod;
    public ProjectileTargetMode projectileTargetMode;
    public MMF_Player mmf_Plaer;
    //public Projectile projectile;
    public MMObjectPooler ObjectPooler;
    #region 武器个性参数
    public int projectileNum = 1;

    #endregion


    public override void Attack(Vector3 _targetPos)
    {
        base.Attack(_targetPos);
        mmf_Plaer?.PlayFeedbacks();

        switch (attackMethod)
        {
            case RangedMeowBallAttackMethod.Projectile:
                StartCoroutine(ProjectileAttack(_targetPos));
                break;
        }
    }
    /*
    IEnumerator ProjectileAttack(Vector3 _targetPos)
    {
        for(int i = 0; i < projectileNum; i++)
        {
            ProjectileController _projectile = SpawnProjectile(transform.position, i,  true)?.GetComponent<ProjectileController>();
            //ProjectileController _projectile = Instantiate(projectile, transform.position,Quaternion.identity).GetComponent<ProjectileController>();
            _projectile.SetMeowBall(this);
            yield return new WaitForSeconds(0.1f);
        }
        FinishAttack();
    }*/
    IEnumerator ProjectileAttack(Vector3 _targetPos)
    {
        for (int i = 0; i < projectileNum; i++)
        {
            ProjectileController _projectileController = SpawnProjectile(transform.position, i, true)?.GetComponent<ProjectileController>();
            _projectileController.SetMeowBall(this);
            _projectileController.gameObject.SetActive(true);
            _projectileController.Init();
            _projectileController.SetDamage();
            yield return new WaitForSeconds(0.1f);
        }
        FinishAttack();
    }


    public virtual GameObject SpawnProjectile(Vector3 spawnPosition, int projectileIndex,  bool triggerObjectActivation = true)
    {
        GameObject nextGameObject = ObjectPooler.GetPooledGameObject();

        if (nextGameObject == null) { return null; }
        if (nextGameObject.GetComponent<MMPoolableObject>() == null)
        {
            throw new Exception(gameObject.name + " is trying to spawn objects that don't have a PoolableObject component.");
        }

        nextGameObject.transform.position = spawnPosition;

        Projectile projectile = nextGameObject.GetComponent<Projectile>();

        nextGameObject.gameObject.SetActive(true);

        


        return (nextGameObject);
    }




}
