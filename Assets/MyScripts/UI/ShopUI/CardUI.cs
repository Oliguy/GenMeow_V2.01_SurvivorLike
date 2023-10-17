using MoreMountains.Feedbacks;
using MoreMountains.InventoryEngine;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
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

    [Header("购买按钮")]
    public Button buyButton_btn;
    public Text buyPrice_txt;


    [Header("FeedBack")]
    public MMFeedbacks revealFeedback;

    [Header("正反面")]
    public GameObject cardBack;
    public GameObject cardFront;


    protected ItemData _itemData;
    public ItemData ItemData { set { _itemData = value; }  get { return _itemData; } }

    protected UIElementDataBase _uiDB;
    protected int _price;
    private IEnumerator Start()
    {
        cardBack.SetActive(true);
        cardFront.SetActive(false);
        GenMeowEvent.DestroyCardEvent += DestroyCard;
        yield return new WaitForSeconds(0.5f);
        revealFeedback?.PlayFeedbacks();
    }


    public void SetItemData(ItemData itemData)
    {
        _itemData = itemData;
        InitCard();
    }


    private void InitCard()
    {
        Debug.Log("InitCard called with item: " + _itemData.itemName);
        _uiDB = MeowDataBase.UIElement_DB;
        if (_itemData.itemID == 0)
        {
            Debug.LogError("创建卡片游戏对象为空！");
            return;
        }
        //设置图片
        itemImage_img.sprite = _itemData.itemIcon;
        Debug.Log("itemImage_img.sprite set to: " + _itemData.itemIcon);
        //设置稀有度颜色

        cardTopRarityColor.color = MeowDataBase.GetCardTopRarity(_itemData.itemRarity);
        cardHeadRarityColor.color = MeowDataBase.GetCardHeadRarity(_itemData.itemRarity);
        itemName_txt.text = _itemData.itemName;
        Debug.Log("itemName_txt.text set to: " + _itemData.itemName);
        itemType_txt.text = _itemData.itemType switch
        {
            ItemType.MeowBall => "毛线球",
            ItemType.Consumable =>"消耗品",
            _ => "道具"
        };
        cardTopGradient.color = cardHeadRarityColor.color;
        //设置描述
        itemDescription_txt.text = _itemData.propertyDescription;
        itemSecondaryDescription_txt.text = _itemData.attachDescription;

        //设置价格
        CalculatePrice();
        if (buyPrice_txt != null)
            buyPrice_txt.text = _price.ToString();

    }
    
    public void CalculatePrice()
    {
        Debug.Log(_itemData.itemBasePrice);
        if (GenMeowInventoryManager.Instance != null)
        {
            _price =(int) GenMeowInventoryManager.Instance.PriceCount * _itemData.itemBasePrice;
        }
        else
        {
            _price = _itemData.itemBasePrice;
        }
    }


    //被按钮组件调用
    public void OnButtonClick_Buy()
    {

        if (GenMeowInventoryManager.Instance.CurrentMola >= _itemData.itemBasePrice)
        {

            if((_itemData.itemType == ItemType.MeowBall &&
                GenMeowInventoryManager.Instance.InventoryMeowBall.Count>=6 && 
                GenMeowInventoryManager.Instance.InventoryMeowBall.Find(i=> i == _itemData) == null)
                ||
                (_itemData.itemType == ItemType.MeowBall &&
                GenMeowInventoryManager.Instance.InventoryMeowBall.Count >= 6 &&
                _itemData.itemID > 6400)
                )
            {
                MessageUIManager.Instance.Buy_Fail_NeedSlot();
            }
            else
            {
                
                GenMeowInventoryManager.Instance.CurrentMola -= _itemData.itemBasePrice;
                GenMeowEvent.CallUpdateMola();
                GenMeowEvent.CallAddItem2Inventory(_itemData);
                GenMeowEvent.CallUpdateItemBag();
                DestroyCard();
            }


        }
        else
        {
            MessageUIManager.Instance.Buy_Fail();
        }
    }

    void OnDestroy()
    {
        GenMeowEvent.DestroyCardEvent -= DestroyCard;
    }

    public void DestroyCard()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }
}
