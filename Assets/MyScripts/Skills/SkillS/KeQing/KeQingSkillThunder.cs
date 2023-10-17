using DG.Tweening;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeQingSkillThunder : MonoBehaviour
{
    public DamageOnTouch _damageOnThouch;
    private void Start()
    {
        _damageOnThouch.enabled = true;
    }

    public void RandomEject(int _ejectTimes,List<GameObject> _list,int _currentNum)
    {
        if(_list.Count <=3) return;
        var se = DOTween.Sequence();
        int _num1;
        int _num2;
        do
        {
            _num1 = Random.Range(0, _list.Count);
        }while(_num1 == _currentNum);

        do
        {
            _num2 = Random.Range(0, _list.Count);
        } while (_num2 == _num1);

        for (int i = 0; i < _ejectTimes-2; i++)
        {
            if(i == 0)
            {
                se.Append(transform.DOMove(_list[_num1].transform.position, 0.3f));
            }

            if (i == 1)
            {
                se.Append(transform.DOMove(_list[_num2].transform.position, 0.3f));
            }

            se.Append(transform.DOMove(EnemyManager.Instance.RandomEnemy(), 0.1f));
        }
        se.OnComplete(() => { this.DelayDestroy(0.2f, true); });
    }

    public void DelayDestroy(float _time,bool _findEnemy)
    {

        if( _findEnemy)
        {
            var _targetPos = EnemyManager.Instance.RandomEnemy();
            Vector3 _dir = (_targetPos - transform.position).normalized;
            transform.DOMove(new Vector3(_dir.x * 40, _dir.y * 40, _dir.z), _time).SetRelative();
        }
        else
        {
            _damageOnThouch.enabled = false;
        }

        Destroy(gameObject, _time);
    }

}
