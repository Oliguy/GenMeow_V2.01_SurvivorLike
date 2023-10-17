using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilucSkill_Liming : MonoBehaviour
{
    public float acceleration;
    public float lifeTime;
    protected Collider2D _collider;
    protected Rigidbody2D _rigidbody2D;

    protected Vector3 _forceDir;

    void OnEnable()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void SetInit(Vector3 _setForce)
    {
        _forceDir = _setForce;
        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);

        // Animate the scale over 0.5 seconds, easing out for a slow start and fast finish
        transform.DOScaleY(1, 0.5f).SetEase(Ease.OutQuad).OnComplete(()=>StartCoroutine(Fly()));
    }

    IEnumerator Fly()
    {
        float timer = 0;
        while (lifeTime >= timer)
        {
            yield return new WaitForSeconds(0.05f);
            timer += 0.05f;
            _rigidbody2D.AddForce(_forceDir * acceleration);
        }
        Destroy(gameObject);
    }

}
