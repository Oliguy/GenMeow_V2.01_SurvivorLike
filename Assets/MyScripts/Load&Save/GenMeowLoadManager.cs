using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct SaveData
{

}

public static class GenMeowLoadManager 
{
    public static void SaveGame()
    {
        SaveData saveData = new SaveData();
        string json = JsonUtility.ToJson(saveData);
        //File.WriteAllText("save.json", json);
    }

    //public static void LoadGame()


}
