using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataBase_DB", menuName = "GenMeow/DataBase/SkillDataBase")]
public class SkillDataBase_SO : ScriptableObject
{
    public List<MeowSkillDetails> skillDatabase;
}
