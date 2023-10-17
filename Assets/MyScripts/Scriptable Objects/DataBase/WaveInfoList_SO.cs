using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WaveList_DB",menuName = "GenMeow/DataBase/WaveList")]
public class WaveInfoList_SO : ScriptableObject
{
    public List<WaveInfo_SO> LevelList;
}
