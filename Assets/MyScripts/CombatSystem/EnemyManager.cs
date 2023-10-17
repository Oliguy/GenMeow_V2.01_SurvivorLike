using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MMSingleton<EnemyManager>
{
    public List<EnemyStatus> EnemyLivingList;

    public void RegisterEnemyList(EnemyStatus _enemy)
    {
        EnemyLivingList.Add(_enemy);
    }

    public void QuitEnemyList(EnemyStatus _enemy)
    {
        EnemyLivingList.Remove(_enemy);
    }

    public Vector3 NearestEnemyPos(Vector3 _selfPos)
    {
        if(EnemyLivingList.Count == 0)
            return Vector3.zero;
        Vector3 _nearestPos = Vector3.zero;
        float _nearestDistance = 999999f;
        foreach (var _enemy in EnemyLivingList)
        {
            if(_nearestDistance > (_enemy.transform.position - _selfPos).magnitude)
            {
                _nearestPos = _enemy.transform.position;
                _nearestDistance = _enemy.transform.position.magnitude;
            }
        }
        return _nearestPos;
    }


    public Vector3 RandomEnemy()
    {
        if (EnemyLivingList.Count == 0)
            return Vector3.zero;
        
        return EnemyLivingList[Random.Range(0,EnemyLivingList.Count)].transform.position;
    }

}
