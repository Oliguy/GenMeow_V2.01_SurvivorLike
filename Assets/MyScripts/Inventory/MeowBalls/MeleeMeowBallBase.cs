using MoreMountains.TopDownEngine;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MeleeMeowBallBase : MeowBallType
{
    public MeleeMeowBallAttackMethod attackMethod;
    public float attackDuration;

    protected DamageOnTouch _damageOnTouchConponent;


    protected override IEnumerator Start()
    {
        yield return base.Start();
        _damageOnTouchConponent = GetComponent<DamageOnTouch>();
        _damageOnTouchConponent.enabled = false;

        //_originalPos = transform.localPosition;
    }

    public override void Attack(Vector3 _targetPos)
    {
        base.Attack(_targetPos);
        switch (attackMethod)
        {
            case MeleeMeowBallAttackMethod.Stick:
                Stick(_targetPos);
                break;
            case MeleeMeowBallAttackMethod.Swap:
                Swap(_targetPos);
                break;
        }
    }

    protected void Stick(Vector3 _targetPos)
    {
        _damageOnTouchConponent.enabled = true;

        _targetPos = transform.position +(_targetPos - transform.position).normalized * finalRange;


        transform.DOMove(_targetPos, attackDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(MeowBallReturn);

    }

    protected void Swap(Vector3 _targetPos)
    {
        _damageOnTouchConponent.enabled = true;

        _targetPos = new Vector3((_targetPos.x - transform.position.x) * 0.8f + transform.position.x, _targetPos.y, _targetPos.z);

        transform.DOMove(_targetPos, 0.1f);
        Vector3 _targetRotation = Vector3.zero;
        if ( transform.localPosition.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180-(transform.rotation.z - 60));

            _targetRotation = new Vector3(0, 0, 180-(transform.rotation.z + 60));
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z - 60);

            _targetRotation = new Vector3(0, 0, transform.rotation.z + 60);
        }

        transform.DORotate(_targetRotation,attackDuration)
            .OnComplete(MeowBallReturn);
    }



     void MeowBallReturn()
    {
        transform.DOLocalMove(_originalPos, 0.2f)
            .SetEase(Ease.OutQuad);
        _damageOnTouchConponent.enabled = false;
        FinishAttack();
    }

}
