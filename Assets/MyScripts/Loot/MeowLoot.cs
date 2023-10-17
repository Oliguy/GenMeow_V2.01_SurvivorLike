using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowLoot : MonoBehaviour
{
    public string targetTag = "Player";
    public float triggerRadius;
    public int usableTimes = 1;

    public MMF_Player feedback;
    public float initFlySpeed = 1f;

    [Range(0,100)]public int lootRange; 

    protected int _remainUsabletimes;
    protected CircleCollider2D _collider;
    protected float _speed;
    protected bool _findTarget;
    protected float _stopDistence = 0.1f;
    protected virtual void OnEnable()
    {
        _remainUsabletimes = usableTimes;
        _collider = GetComponent<CircleCollider2D>();
        if(triggerRadius != 0)
            _collider.radius = triggerRadius;
        _speed = initFlySpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(_remainUsabletimes <= 0 ) return;
        if (collision.CompareTag(targetTag))
        {
            TargetEnter();
        }
    }

    protected virtual void TargetEnter()
    {
        _remainUsabletimes -= 1;
    }

    public void FlyToPlayer()
    {
        if (!_findTarget)
        {
            _findTarget = true;
            StartCoroutine(Fly(GameCopilot.Instance.MeowStatus.transform));
        }
    }

    IEnumerator Fly(Transform _tranform)
    {
        while (Vector3.Distance(transform.position, _tranform.position) > _stopDistence)
        {
            Debug.Log("剩余次数" + _remainUsabletimes);
            Vector3 _dir = (_tranform.position - transform.position).normalized;
            _speed *= 1.05f;
            transform.position += _dir * _speed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        if (_tranform.CompareTag("Player"))
        {
            transform.SetParent(_tranform);
            transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            if (_tranform != null)
            {
                transform.position = _tranform.position;
            }
        }
    }

}
