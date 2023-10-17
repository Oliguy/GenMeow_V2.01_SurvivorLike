using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenMeowInventoryManager : MMSingleton<GenMeowInventoryManager>
{
    private void OnEnable()
    {
        GenMeowEvent.AddItem2Inventory += AddMeowItem;
        GenMeowEvent.WaveStart += MeowBallLoad;
    }
    private void OnDisable()
    {
        GenMeowEvent.AddItem2Inventory -= AddMeowItem;
        GenMeowEvent.WaveStart -= MeowBallLoad;
    }
    [Header("DataBase")]
    public ItemDataBase_SO buffItem_DB;
    public ItemDataBase_SO meowBall_DB;
    [Header("InventorySO")]
    public MeowInventorySO _inventory;
    [Header("LevelSO")]
    public LevelSheet_SO level_SO;

    public List<ItemData> InventoryBuffItem { get { return _inventory.inventoryBuffItem; }set { _inventory.inventoryBuffItem = value; } }
    public List<ItemData> InventoryMeowBall { get { return _inventory.inventoryMeowBall; } set { _inventory.inventoryMeowBall = value; } }
    public int CurrentMola { get { return _inventory.currentMola; } set { _inventory.currentMola = value; } }
    public int CurrentExp { get { return _inventory.currentExp; } set { _inventory.currentExp = value; } }

    public float PriceCount { get { return _priceCount; } set { _priceCount = value; } }
    protected float _priceCount = 1;
    

    #region Load
    public void BuffItemLoad(MeowStatus _meow) //在游戏开始时，加载道具的方法。
    {
        if (InventoryBuffItem != null)
        {
            foreach(var _item in InventoryBuffItem)
            {
                _item.itemGameObject.GetComponent<BuffItemType>().ItemLoad(_inventory.meowSO);
            }
        }
    }
    public void MeowBallLoad()
    {
        if (GameCopilot.Instance.WaveNow == 1)
        {
            AddMeowItem(MeowDataBase.GetMeowBallData(CharacterInfoManager.Instance.Character.initMeowballID));
        }

        int numItems = InventoryMeowBall.Count;
        float angleInterval = 2 * Mathf.PI / numItems; // 获取每个项目间的角度间隔

        for (int i = 0; i < numItems; i++)
        {
            float angle = i * angleInterval; // 计算当前物品的角度
            Vector3 playerPosition = GameCopilot.Instance.MeowStatus.transform.position;
            Vector3 spawnPosition = new Vector3(
                playerPosition.x + Settings.meowball_Radius * Mathf.Cos(angle),
                playerPosition.y + Settings.meowball_Radius * Mathf.Sin(angle),
                playerPosition.z
            );

            Instantiate(InventoryMeowBall[i].itemGameObject, spawnPosition, Quaternion.identity, GameCopilot.Instance.MeowStatus.transform);
        }
    }
    #endregion


    public void AddMeowItem(ItemData _itemData)
    {
        Debug.Log("添加物品" + _itemData.itemName);
        switch (_itemData.itemType)
        {
            case ItemType.BuffItem:
                InventoryBuffItem.Add(_itemData);
                _itemData.itemGameObject.GetComponent<BuffItemType>().ItemLoad(_inventory.meowSO);
                MessageUIManager.Instance.Buy_Success();
                break;
            case ItemType.MeowBall:
                if (InventoryMeowBall.Count < 6)
                {
                    InventoryMeowBall.Add(_itemData);
                    MessageUIManager.Instance.Buy_Success();
                }
                else
                {
                    if(_itemData.itemID <=6400 && InventoryMeowBall.Find(i => i.itemID == _itemData.itemID) != null)
                    {
                        InventoryMeowBall.Remove(_itemData);
                        InventoryMeowBall.Add(MeowDataBase.GetMeowBallData(_itemData.itemID + 100));
                        MessageUIManager.Instance.Buy_Success();
                    }
                    else
                    {
                        MessageUIManager.Instance.Buy_Fail_NeedSlot();
                    }
                }
                break ;
        }
        MeowStatusItem _effect = _itemData.itemGameObject.GetComponent<ItemTypeDataDetails>()?.effect;
        if (_effect == null) return;
        MeowStatusItem _newEff = Instantiate(_effect, CharacterInfoManager.Instance.transform);
        _newEff.AddEvent2System();
        GenMeowEvent.CallUpdateItemBag();
    }

    public void GainMola(int molaNum)
    {
        //TODO:在回合结束的时候，应该怎么做？
        this.CurrentMola += molaNum;
        this.CurrentExp += 100;
        if(CurrentExp>= level_SO.levels[_inventory.currentLevel].exp)
        {
            CurrentExp -= level_SO.levels[_inventory.currentLevel].exp;
            _inventory.currentLevel += 1;
            GenMeowEvent.CallLevelUp();
        }
        GenMeowEvent.CallUpdateMola();
    }

    public bool SynMeowball(ItemData _itemData)
    {
        if(InventoryMeowBall.Count(i => i.itemID == _itemData.itemID)>=2)
        {
            for(int i = 0; i < 2; i++)
            {
                InventoryMeowBall.Remove(_itemData);
            }
            InventoryMeowBall.Add(MeowDataBase.GetMeowBallData(_itemData.itemID + 100));
            MessageUIManager.Instance.Syn_Success();
        GenMeowEvent.CallUpdateItemBag();
            return true;
        }
        MessageUIManager.Instance.Syn_Fail();
        return false;
    }
}
