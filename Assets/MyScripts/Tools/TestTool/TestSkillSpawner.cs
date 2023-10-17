using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestSkillSpawner : MonoBehaviour
{

    public MeowSkillBase _skill;

    private void Update()
    {
        if (_skill == null) return;

        if (Input.GetKeyDown(KeyCode.V))
        {
            _skill.ActiveSkill();
        }
    }

}
