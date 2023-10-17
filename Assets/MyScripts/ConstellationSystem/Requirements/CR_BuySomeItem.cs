using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_BuySomeItem : ConstellationRequirement
{
    public int itemID;
    public int requireNum;

    protected int currentNum;
    private void OnEnable()
    {
        GenMeowEvent.AddItem2Inventory += RequireBuySomething;
        currentNum = 0;
    }

    private void OnDisable()
    {
        GenMeowEvent.AddItem2Inventory -= RequireBuySomething;
    }

    public void RequireBuySomething(ItemData _itemData)
    {
        if (_itemData.itemID == itemID)
        {
            currentNum++;
            if(currentNum >= requireNum)
                this.ActiveConstellation();
        }
    }

    public override string SetRequirementDescription()
    {
        ItemData _item = MeowDataBase.GetItemData(itemID);
        requirementDescription = "需要获取" + requireNum + "份" + _item.itemName + "。";
        Debug.Log("已更改" + requirementDescription);
        return requirementDescription;
    }

}
