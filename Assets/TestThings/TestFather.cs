using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFather : MonoBehaviour
{
    public LevelSheet_SO SO;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InitLevelInfo();
        }
    }

    public void InitLevelInfo()
    {
        var list = SO.levels;
        for(int i = 0; i < list.Count; i++)
        {
            list[i].level = i;
            list[i].exp = (i + 4) * (i + 4) * 100;
        }
        SO.SetDirty();
    }
}
