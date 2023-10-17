using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MeowUITextEditor : Editor
{
    [MenuItem("MeowEditorMenu/ModifyAllItemText")]
    private static void ModifyItemPropertyText()
    {
        ModifyBuffItemText(MeowDataBase.buffItem_DB);
        ModifyBuffItemText(MeowDataBase.meowBall_DB);
    }



    private static void ModifyBuffItemText(ItemDataBase_SO _SO)
    {
        if( _SO != null)
        {
            for (int i = 0; i < _SO.itemDataList.Count; i++)
            { 
                var itemData = _SO.itemDataList[i];

                var _buffItem = itemData.itemGameObject.GetComponent<BuffItemType>();
                if( _buffItem == null)
                {
                    Debug.LogError("请检查BuffItem挂载" + itemData.itemName);
                }
                string _description = "";
                foreach (var _property in _buffItem.itemMainPropertyList)
                {
                    string _propertyDir = "";
                    string _propertyString = "";
                    int _propertyValue;
                    bool _isPercent = false;
                    if (_property.propertyValue < 0 )
                    {
                        _propertyDir = "<color=red>减少</color>";
                        _propertyValue = -_property.propertyValue;
                    }
                    else
                    {
                        _propertyDir = "<color=green>增加</color>";
                        _propertyValue = _property.propertyValue;
                    }
                    _propertyString = EnumPropertyName.GetPropertyName(_property.mainProperty);
                    switch (_property.mainProperty)
                    {
                        case MainProperty.DamageGlobal:
                            _isPercent = true;
                            break;
                        case MainProperty.Speed:
                            _isPercent = true;
                            break;
                        case MainProperty.AttackSpeed:
                            _isPercent = true;
                            break;
                        case MainProperty.CritChance:
                            _isPercent = true;
                            break;
                    }
                    if (_isPercent == true)
                    {
                        _description += _propertyString + _propertyDir + _propertyValue + "%\n";
                    }
                    else
                    {
                        _description += _propertyString + _propertyDir + _propertyValue + "\n";
                    }
                }
                //itemData.ModifyProperyDescription(_description);
                itemData.propertyDescription = _description;
                _SO.itemDataList[i] = itemData;
                Debug.Log(itemData.propertyDescription);

                EditorUtility.SetDirty(_SO.itemDataList[i].itemGameObject);
            }
            EditorUtility.SetDirty(_SO);
            AssetDatabase.SaveAssets();
            Debug.LogError("<color=green>道具描述信息初始化成功</color>");
        }
        else
        {
            Debug.LogError("文件不存在！");
        }
    }

}
