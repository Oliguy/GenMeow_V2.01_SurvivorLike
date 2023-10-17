using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int itemID;
    protected ItemType itemType;
    public Image itemImg;

    public Image rarityImg;
    public Image selectedImg;

    public GameObject cardFront;

    public ItemData ItemData { 
        get { return _itemData; }
        set { _itemData = value; }
        }
    protected ItemData _itemData;

    private void Start()
    {
        selectedImg.enabled = false;
    }
    /// <summary>
    /// 根据itemID与itemType，去场景中的InventoryManager匹配相应的数据信息
    /// </summary>
    /// <param name="_itemID"></param>
    /// <param name="_itemType"></param>
    public void SetItemInfo(int _itemID, ItemType _itemType)
    {
        itemID = _itemID;
        ItemDataBase_SO _DB;
        switch (_itemType)
        { 
            case ItemType.BuffItem:
                _DB = GenMeowInventoryManager.Instance.buffItem_DB;
                GenMeowEvent.DestroyAllBuffItemUIElement += DestroyEvent;
                break;
            case ItemType.MeowBall:
                _DB = GenMeowInventoryManager.Instance.meowBall_DB;
                GenMeowEvent.DestroyAllMeowBallUIElement += DestroyEvent;
                break;
            default:
                _DB = GenMeowInventoryManager.Instance.buffItem_DB;
                GenMeowEvent.DestroyAllBuffItemUIElement += DestroyEvent;
                break;
        }
        ItemData _item = _DB.itemDataList.Find( _myitem => _myitem.itemID == _itemID );
        if(_item.itemID == 0)
        {
            Debug.LogError("未能与数据库中元素匹配！");
            return ;
        }
        itemImg.sprite = _item.itemIcon;
        rarityImg.sprite = MeowDataBase.GetItemRarity(_item.itemRarity);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedImg.enabled = true;
        DescriptionUIManager.Instance.ShowItemDetails(ItemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectedImg.enabled = false;
        DescriptionUIManager.Instance.UnshowItemDetails();
    }

    public void DestroyEvent()
    {
        Debug.Log("摧毁游戏物品" + ItemData.itemName);
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        GenMeowEvent.DestroyAllMeowBallUIElement -= DestroyEvent;
        GenMeowEvent.DestroyAllBuffItemUIElement -= DestroyEvent;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if(_itemData.itemType == ItemType.MeowBall && SceneManager.GetActiveScene().name == "Shop")
        {
            Debug.Log("鼠标点击" + _itemData.itemType + "场景名" + SceneManager.GetActiveScene().name);
            DescriptionUIManager.Instance.SaleItemPanel(ItemData);
        }
    }
}
