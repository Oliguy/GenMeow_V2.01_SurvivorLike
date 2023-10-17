using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LivingScaleController : MonoBehaviour
{

    public float scaleTransSpeedOri;
    [SerializeField]
    private float scaleTransSpeed;
    public float scaleMax;
    public float scaleMin;

    private Vector3 targetScale;

    private void Update()
    {
        if (MathF.Abs(Input.GetAxisRaw("Player1_Horizontal")) + MathF.Abs(Input.GetAxisRaw("Player1_Vertical"))>0.5)
        {
            scaleTransSpeed = scaleTransSpeedOri * 2;
        }
        else
        {
            scaleTransSpeed = scaleTransSpeedOri;
        }
    }
    private void FixedUpdate()
    {
        
        ScaleTrans(scaleTransSpeed);
    }

    void ScaleTrans(float _scaleTransSpeed)
    {
        targetScale.x = Mathf.PingPong(Time.time * _scaleTransSpeed, scaleMax-scaleMin) + scaleMin;
        targetScale.y = -Mathf.PingPong(Time.time * _scaleTransSpeed, scaleMax - scaleMin) + scaleMax;

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.fixedDeltaTime);

    }
}
