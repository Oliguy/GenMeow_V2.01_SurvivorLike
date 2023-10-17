using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 给予玩家对于一定范围内掉落物体的吸附能力
/// </summary>
public class MeowObtain : MonoBehaviour
{
    public float obtainRadius;

    protected CircleCollider2D _collider;
    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.radius = obtainRadius;
    }
    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.CompareTag("Loot"))
        {
            _collider.GetComponent<MeowLoot>().FlyToPlayer();
            Debug.Log("找到掉落物" + _collider.gameObject.name);
        }
    }

}
