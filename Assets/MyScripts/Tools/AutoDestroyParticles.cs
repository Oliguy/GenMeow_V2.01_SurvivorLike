using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour
{
    public float particleLifeTime;
    public ParticleSystem particle;

    private void OnEnable()
    {
        if(particle == null)
            particle = GetComponent<ParticleSystem>();
        particle?.Play();
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(particleLifeTime);
        Destroy(this.gameObject);
    }


    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
