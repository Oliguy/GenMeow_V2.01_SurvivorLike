using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelfcopySkill : MonoBehaviour
{
    public int copyNum=1;
    public float copyRadius=2f;
    public GameObject generatePoint;
    public AIBrain _brain;

    private void Start()
    {
        _brain = GetComponent<AIBrain>();
    }

    private void FixedUpdate()
    {
        if (_brain.enabled == false)
            Destroy(this);
    }

    private void OnEnable()
    {
        if (copyNum <= 0) return;
        StartCoroutine(SelfCopy());
    }

    private IEnumerator SelfCopy()
    {
        yield return new WaitForSeconds(0.2f);
        if (copyNum <= 0) yield break;

        while(copyNum >= 1)
        {

            Vector2 pos = GetRandomPos();
            Instantiate(generatePoint, new Vector2(pos.x, pos.y), Quaternion.identity, this.transform.parent);
            yield return new WaitForSeconds(1f);
            EnemySelfcopySkill newObj = Instantiate(this, new Vector2(pos.x, pos.y), Quaternion.identity, this.transform.parent);
            newObj.copyNum = copyNum - 1;
            copyNum -= 1;
        }

    }

    private Vector2 GetRandomPos()
    {
        return (Vector2)transform.position + (Random.insideUnitCircle * copyRadius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, copyRadius);
    }
}
