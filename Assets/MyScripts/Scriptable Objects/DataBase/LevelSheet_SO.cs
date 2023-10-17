using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelList_DB", menuName = "GenMeow/DataBase/LevelSheet")]
public class LevelSheet_SO : ScriptableObject
{
    public List<LevelExpInfo> levels;
}
