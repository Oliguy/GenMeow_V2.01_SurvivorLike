using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageUI : MonoBehaviour
{
    public ItemType itemType;
    public ItemUI itemIconPrefab;

    protected List<ItemData> _itemList;

    private void OnEnable()
    {
        RefreshItem();
        GenMeowEvent.UpdateItemBag += RefreshItem;
    }

    private void OnDisable()
    {
        GenMeowEvent.UpdateItemBag -= RefreshItem;
    }

    public void RefreshItem()
    {
        _itemList = new();
        switch (itemType)
        {
            case ItemType.BuffItem:
                _itemList = GenMeowInventoryManager.Instance.InventoryBuffItem;
                break;
            case ItemType.MeowBall:
                _itemList = GenMeowInventoryManager.Instance.InventoryMeowBall;
                break;
        }

        foreach (Transform itemTrans in transform)
        {
            Destroy(itemTrans.gameObject);
        }

        foreach (var _item in _itemList)
        {
            ItemUI _itemUI = Instantiate(itemIconPrefab, this.transform);
            _itemUI.ItemData = _item;
            _itemUI.SetItemInfo(_item.itemID, _item.itemType);
        }
    }
}
