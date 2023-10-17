using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeowDataBase
{
    public static ItemDataBase_SO buffItem_DB = Resources.Load<ItemDataBase_SO>("BuffItemDataBase_DB");
    public static ItemDataBase_SO meowBall_DB = Resources.Load<ItemDataBase_SO>("MeowBallDataBase_DB");
    public static UIElementDataBase UIElement_DB = Resources.Load<UIElementDataBase>("UIElementDataBase_DB");



    public static ItemData GetItemData(int _itemID)
    {
        foreach (ItemData item in buffItem_DB.itemDataList)
        {
            if (item.itemID == _itemID)
            {
                return item;
            }
        }
        return null;
    }
    public static ItemData GetMeowBallData(int _itemID)
    {
        foreach (ItemData item in meowBall_DB.itemDataList)
        {
            if (item.itemID == _itemID)
            {
                return item;
            }
        }
        return null;
    }

    public static Sprite GetItemRarity(ItemRarity _rarity)
    {
        return _rarity switch
        {
            ItemRarity.Green => UIElement_DB.sprite_ItemRarity_green,
            ItemRarity.Blue => UIElement_DB.sprite_ItemRarity_blue,
            ItemRarity.Purple => UIElement_DB.sprite_ItemRarity_purple,
            ItemRarity.Golden => UIElement_DB.sprite_ItemRarity_gold,
            _ => UIElement_DB.sprite_ItemRarity_gray,
        };
    }
    public static Color GetCardTopRarity(ItemRarity _rarity)
    {
        return _rarity switch
        {
            ItemRarity.Gray => UIElement_DB.color_CardTopRarity_gray,
            ItemRarity.Green => UIElement_DB.color_CardTopRarity_green,
            ItemRarity.Blue => UIElement_DB.color_CardTopRarity_blue,
            ItemRarity.Purple => UIElement_DB.color_CardTopRarity_purple,
            _ => UIElement_DB.color_CardTopRarity_gold,
        };
    }
    public static Color GetCardHeadRarity(ItemRarity _rarity)
    {
        return _rarity switch
        {
            ItemRarity.Gray => UIElement_DB.color_CardHeadRarity_gray,
            ItemRarity.Green => UIElement_DB.color_CardHeadRarity_green,
            ItemRarity.Blue => UIElement_DB.color_CardHeadRarity_blue,
            ItemRarity.Purple => UIElement_DB.color_CardHeadRarity_purple,
            _ => UIElement_DB.color_CardHeadRarity_gold,
        };
    }
}