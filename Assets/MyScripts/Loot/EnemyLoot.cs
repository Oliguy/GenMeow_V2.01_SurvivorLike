using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootChanceData
{
    public int lootChancePercent;
    public MeowLoot lootGameObject;
}

public class EnemyLoot : MonoBehaviour
{
    public List<LootChanceData> _list;
    public void Loot()
    {
        int randomNum = Random.Range(0, 100);
        foreach(var loot in _list)
        {
            if(loot.lootChancePercent >= randomNum)
            {
                Instantiate(loot.lootGameObject, transform.position, Quaternion.identity);
            }
        }
    }


}

