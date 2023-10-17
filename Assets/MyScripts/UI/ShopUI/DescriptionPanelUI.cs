using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DescriptionPanelUI : MonoBehaviour
{

    [Header("图片修改")]
    public Image itemImage_img;

    [Header("颜色修改")]
    public Image cardHeadRarityColor;
    public Image cardTopRarityColor;
    public Image cardTopGradient;

    [Header("文本")]
    public Text itemName_txt;
    public Text itemType_txt;
    public Text itemDescription_txt;
    public Text itemSecondaryDescription_txt;

    [Header("出售按钮组")]
    public GameObject saleButtons;
    public Text saleButtonText;

    public int ItemID { get; set; }

    public ItemData ItemData
    {
        get { return _itemData; }
        set { _itemData = value; }
    }

    protected UIElementDataBase _uiDB;
    public ItemData _itemData;




    private void OnEnable()
    {
        ShowPanel();
    }

    public void ShowPanel()
    {
        _uiDB = MeowDataBase.UIElement_DB;
        Debug.Log("ShowPanel" + _itemData.itemIcon);
        //设置图片
        itemImage_img.sprite = _itemData.itemIcon;
        //设置稀有度颜色
        cardTopRarityColor.color = MeowDataBase.GetCardTopRarity(_itemData.itemRarity);
        cardHeadRarityColor.color = MeowDataBase.GetCardHeadRarity(_itemData.itemRarity);
        itemName_txt.text = _itemData.itemName;
        itemType_txt.text = _itemData.itemType switch
        {
            ItemType.MeowBall => "毛线球",
            ItemType.Consumable => "消耗品",
            _ => "道具"
        };

        cardTopGradient.color = cardHeadRarityColor.color;
        //设置描述
        itemDescription_txt.text = _itemData.propertyDescription;
        itemSecondaryDescription_txt.text = _itemData.attachDescription;

        if (_itemData.itemType == ItemType.MeowBall && SceneManager.GetActiveScene().name == "Shop")
        {
            saleButtons.SetActive(true);
            saleButtonText.text = "售出（" + (int)(ItemData.itemBasePrice * 0.25f) + "）";
        }
    }

    public void OnClickCancel()
    {
        DescriptionUIManager.Instance.CloseSalePanel();
    }
    public void OnClickSyn()
    {
        //合成的代码
        GenMeowInventoryManager.Instance.SynMeowball(_itemData);

        DescriptionUIManager.Instance.CloseSalePanel();
    }
    public void OnClickSale()
    {
        DescriptionUIManager.Instance.CloseSalePanel();
        GenMeowInventoryManager.Instance._inventory.inventoryMeowBall.Remove(ItemData);
        GenMeowInventoryManager.Instance.CurrentMola += (int) (ItemData.itemBasePrice * 0.25f);
        Debug.Log((int)(ItemData.itemBasePrice * 0.25f));
        GenMeowEvent.CallUpdateMola();
        GenMeowEvent.CallUpdateItemBag();

        MessageUIManager.Instance.Sale_Success();
    }

}
