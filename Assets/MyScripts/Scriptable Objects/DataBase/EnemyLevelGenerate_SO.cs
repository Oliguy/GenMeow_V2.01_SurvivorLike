using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_num",menuName = "GenMeow/Wave/LevelInfo")]
public class WaveInfo_SO : ScriptableObject
{
    public int LevelTime;
    public List<EnemyGenerateInfo> enemyGenerateInfo;

}
