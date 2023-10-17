using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : LivingsStatus
{

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyManager.Instance.RegisterEnemyList(this);
    }
    private void OnDisable()
    {
        EnemyManager.Instance.QuitEnemyList(this);
    }


}
