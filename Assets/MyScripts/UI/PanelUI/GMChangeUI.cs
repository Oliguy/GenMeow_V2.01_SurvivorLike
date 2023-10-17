using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public enum ChangedValueEnum
{
    speed,
}


public class GMChangeUI : MonoBehaviour
{
    public InputField _speedInput;
    public InputField _itemInput;
    public ChangedValueEnum _type;

    protected int _valueInt;

    private void Start()
    {
        _speedInput.onValueChanged.AddListener(SpeedValueChanged);
        _itemInput.onValueChanged.AddListener(ItemValueGet);
    }

    public void SpeedValueChanged(string _valueString)
    {
        if (int.TryParse(_valueString, out int _int))
        {
            _valueInt = _int;
        }
        else
        {
            Debug.LogError("Invalid speed input! Please input a number.");
            _speedInput.text = _speedInput.ToString();
        }


        switch (_type)
        {
            case ChangedValueEnum.speed:
                GameCopilot.Instance.MeowStatus.GetComponent<CharacterMovement>().MovementSpeed = _valueInt;
                break;

        }
    }

    public void ItemValueGet(string itemID)
    {
        if(itemID.Length != 4)
        {
            Debug.Log("请输入4位道具ID！");
            return;
        }
        if (int.TryParse(itemID, out int _int))
        {
            _valueInt = _int;
        }
        else
        {
            Debug.LogError("Invalid speed input! Please input a number.");
            _speedInput.text = _speedInput.ToString();
        }
        GenMeowEvent.CallAddItem2Inventory(MeowDataBase.GetItemData(_valueInt));
        //GenMeowInventoryManager.Instance.AddMeowItem(MeowDataBase.GetItemData(_valueInt));

    }

}
