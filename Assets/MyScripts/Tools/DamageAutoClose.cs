using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAutoClose : MonoBehaviour
{
    public float startTime;
    public float endTime;
    DamageOnTouch _damageOnTouch;

    private void OnEnable()
    {
        if(_damageOnTouch==null)
            _damageOnTouch = GetComponent<DamageOnTouch>();
    }

    IEnumerator HitOnDamageControl()
    {
        if(_damageOnTouch!=null)
            _damageOnTouch.enabled = false;
        yield return new WaitForSeconds(startTime);
        if (_damageOnTouch != null)
            _damageOnTouch.enabled = true;
        yield return new WaitForSeconds(endTime);
        if (_damageOnTouch != null)
            _damageOnTouch.enabled = false;
    }

}
