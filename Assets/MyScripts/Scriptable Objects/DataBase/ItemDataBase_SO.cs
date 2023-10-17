using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ItemDataBase_DB",menuName ="GenMeow/DataBase/ItemDataBase")]
public class ItemDataBase_SO : ScriptableObject
{
    public List<ItemData> itemDataList;

    public List<ItemData> GrayItems;
    public List<ItemData> GreenItems;
    public List<ItemData> BlueItems;
    public List<ItemData> PurpleItems;
    public List<ItemData> GoldenItems;

}
