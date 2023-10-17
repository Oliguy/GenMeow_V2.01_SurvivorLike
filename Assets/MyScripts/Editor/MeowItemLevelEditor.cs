using UnityEditor;
using UnityEngine;

public class MeowItemLevelEditor : Editor
{
    [MenuItem("MeowEditorMenu/AddItems2IndividualList")]
    private static void ClassifyItemList()
    {
        ClassifyItem(MeowDataBase.buffItem_DB); 
        ClassifyItem(MeowDataBase.meowBall_DB); 
    }

    private static void ClassifyItem(ItemDataBase_SO _SO)
    {
        if (_SO != null)
        {
            _SO.GrayItems.Clear();
            _SO.GreenItems.Clear();
            _SO.PurpleItems.Clear();
            _SO.BlueItems.Clear();
            _SO.GoldenItems.Clear();
            foreach (ItemData itemData in _SO.itemDataList)
            {
                switch (itemData.itemRarity)
                {
                    case ItemRarity.Gray:
                        _SO.GrayItems.Add(itemData);
                        break;
                    case ItemRarity.Green:
                        _SO.GreenItems.Add(itemData);
                        break;
                    case ItemRarity.Blue:
                        _SO.BlueItems.Add(itemData);
                        break;
                    case ItemRarity.Purple:
                        _SO.PurpleItems.Add(itemData);
                        break;
                    case ItemRarity.Golden:
                        _SO.GoldenItems.Add(itemData);
                        break;
                }
            }
            Debug.Log("道具分类完成！");
        }
        else
        {
            Debug.LogError("文件不存在！");
        }
    }

}
