using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class ItemListTestClass
{
    public List<string> items;
}


public class ButtonLoadTest : MonoBehaviour
{
    public ItemListTestClass testGO;
    public ItemListTestClass testGO2;

    /*
    public void OnSaveAndLoad()
    {
        MMSaveLoadManager.Save(testGO, "test");
        Instantiate((GameObject)MMSaveLoadManager.Load(typeof(GameObject), "test"));
    }
    */
    public void OnSaveAndLoad()
    {


        string jsonData = JsonConvert.SerializeObject(testGO);

        if (!File.Exists(Application.persistentDataPath + "/users"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/users");
        }
        File.WriteAllText(Application.persistentDataPath + string.Format("/users/{0}.json" , "Test"), jsonData);

        string jsonDataLoad = File.ReadAllText(Application.persistentDataPath + string.Format("/users/{0}.json", "Test"));
        testGO2 = JsonConvert.DeserializeObject<ItemListTestClass>(jsonDataLoad);
    }
}
