using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardGenerator : MMSingleton<CardGenerator>
{

    public CardUI cardPrefab;
    public Button refreshButton;

    public List<CardUI> cards;

    private void Start()
    {
        RefreshCard();
    }

    public void RefreshCard()
    {
        GenMeowEvent.CallDestroyCardEvent();
        cards = new List<CardUI>();
        for (int i = 0; i < 4; i++)
        {
            CardUI card = Instantiate(cardPrefab, this.transform);
            //TODO:触发两个骰子，第一个骰子决定从武器or道具中取道具
            float _firstDice = Random.Range(0, 1.0f);
            float _secondDice = Random.Range(0, 1.0f);
            //第二个骰子决定刷新出的商品品质。
            //if(
            card.SetItemData(GetRandomItem(_firstDice,0));
            Debug.Log("卡片名字：" + card.ItemData.itemName);
            cards.Add(card);
        }
    }

    public void CardQuit(CardUI _card)
    {
        cards.Remove(_card);
    }
    /// <summary>
    /// 根据骰子产生的道具品质，去搜寻相关的道具
    /// </summary>
    /// <param name="_secondDice"></param>
    /// <returns></returns>
    public ItemData GetRandomItem(float _firstDice, float _secondDice)
    {
        List<ItemData> _list = new();
        if (_firstDice <=0.35f)
        {
            _list = MeowDataBase.meowBall_DB.itemDataList;
        }
        else
        {
            _list = MeowDataBase.buffItem_DB.itemDataList;
        }

        int _count = _list.Count;//TODO:根据_secondDice 决定哪一个List MeowDataBase.buffItem_DB.GrayItems
        int _num = Random.Range(0, _count);
        ItemData _data = _list[_num];
        Debug.Log("<color=green>当前ItemData的随机数为" + _num + "道具为" + _data.itemGameObject.name + "</color>");
        Debug.Log("(\"<color=yellow>GetRandomBuffItem: item name is " + _data.itemGameObject.itemName + "</color>");
        return _data;

    }
}
