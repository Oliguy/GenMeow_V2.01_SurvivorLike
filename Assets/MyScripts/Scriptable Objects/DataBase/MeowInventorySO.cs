using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MeowInventorySO", menuName = "GenMeow/DataBase/MeowInventorySO")]
public class MeowInventorySO : ScriptableObject
{
    public List<ItemData> inventoryBuffItem;//玩家现在拥有的道具
    public List<ItemData> inventoryMeowBall;//玩家现在拥有的武器
    public int currentMola;
    public int currentMeowLevel;
    public int currentExp;

    public int currentWave;

    public int currentLevel;

    public Meow_SO meowSO;
    public CharacterTemplate meowCharacterTmp;
    public void InitSettings()
    {
        inventoryBuffItem.Clear();
        inventoryMeowBall.Clear();
        currentMola = 100;
        currentMeowLevel = 0;
        currentExp = 0;
        meowSO = null;
    }

}
