using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionUIManager : MMSingleton<DescriptionUIManager>
{

    [Header("道具详情解释Panel")]
    public DescriptionPanelUI descriptionPanel;
    public GameObject salePanel;

    #region 当玩家指针移入道具时触发，展示道具详情
    public void ShowItemDetails(ItemData _itemData)
    {
        descriptionPanel.ItemID = _itemData.itemID;
        descriptionPanel.ItemData = _itemData;
        descriptionPanel.gameObject.SetActive(true);
        descriptionPanel.transform.position = Input.mousePosition + new Vector3(-180,20,0);
    }

    public void UnshowItemDetails()
    {
        descriptionPanel.gameObject.SetActive(false);
    }
    #endregion
    
    public void SaleItemPanel(ItemData _itemData)
    {
        salePanel.SetActive(true);
        foreach(Transform trans in salePanel.transform)
        {
            Destroy(trans.gameObject);
        }
        DescriptionPanelUI card = Instantiate(descriptionPanel, salePanel.transform);
        card.ItemID = _itemData.itemID;
        card.ItemData = _itemData;
        card.gameObject.SetActive(true);
    }
    public void CloseSalePanel()
    {
        salePanel.SetActive(false);
    }
}
