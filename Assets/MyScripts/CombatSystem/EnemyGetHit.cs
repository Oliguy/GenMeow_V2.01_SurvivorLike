using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    public AnimationClip hitAnim;
    public string animName;
    protected Animator anim;
    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetHit();
        }
    }
    public void GetHit()
    {
        anim.Play(animName);
    }

}
