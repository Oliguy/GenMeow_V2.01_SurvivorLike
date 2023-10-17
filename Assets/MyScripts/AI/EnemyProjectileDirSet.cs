using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileDirSet : MonoBehaviour
{
    private void Start()
    {
        Vector3 _pos = GameCopilot.Instance.MeowStatus.transform.position;
        float _rot = Mathf.Atan2(_pos.y - transform.position.y, _pos.x - transform.position.x);
        float degreeValue = _rot * (180f / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, degreeValue);
    }

}
