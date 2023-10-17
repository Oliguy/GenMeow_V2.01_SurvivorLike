using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTypeDataDetails : MonoBehaviour
{
    [Header("道具基础信息")]
    public int itemID;
    public Sprite itemIcon;
    public string itemName;
    public ItemRarity itemRarity;
    public ItemType itemType;
    public int itemBasePrice;

    public MeowStatusItem effect;

    public string propertyDescription;
    public string effectDescription;
    public string attachDescription;

}
