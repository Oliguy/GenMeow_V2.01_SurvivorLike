using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageUIManager : MMSingleton<MessageUIManager>
{
    public Text CenterInfo;
    public Text MediumSizeInfo;
    public AudioClip ui_Fail;


    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // 如果已经有一个实例存在并且不是当前实例，那么销毁当前实例
            Destroy(gameObject);
        }
        else
        {
            // 否则，这个实例就是我们的单例，不要在加载新场景时销毁它。
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
    #region 购买道具
    public void Buy_Success()
    {
        if (SceneManager.GetActiveScene().name != "Shop") return;
        Text _Info = Instantiate(CenterInfo, this.transform);
        _Info.text = "购买成功";
    }

    public void Sale_Success()
    {
        Text _Info = Instantiate(CenterInfo, this.transform);
        _Info.text = "售出成功";
    }
    public void Buy_Fail()
    {

        Text _Info = Instantiate(CenterInfo,this.transform);
        _Info.text = "摩拉不够了喵";
        MMSoundManagerSoundPlayEvent.Trigger(ui_Fail, MMSoundManager.MMSoundManagerTracks.UI, this.transform.position);
    }

    public void Buy_Fail_NeedSlot()
    {

        Text _Info = Instantiate(CenterInfo, this.transform);
        _Info.text = "栏位到达上限喵~";
        MMSoundManagerSoundPlayEvent.Trigger(ui_Fail, MMSoundManager.MMSoundManagerTracks.UI, this.transform.position);
    }
    public void Buy_Fail_MaxLevel()
    {

        Text _Info = Instantiate(CenterInfo, this.transform);
        _Info.text = "已是最高等级喵~";
        MMSoundManagerSoundPlayEvent.Trigger(ui_Fail, MMSoundManager.MMSoundManagerTracks.UI, this.transform.position);
    }

    public void Syn_Success()
    {
        Text _Info = Instantiate(CenterInfo, this.transform);
        _Info.text = "合成成功喵~";
    }

    public void Syn_Fail()
    {
        Text _Info = Instantiate(CenterInfo, this.transform);
        _Info.text = "合成材料不足，不行喵~";
        MMSoundManagerSoundPlayEvent.Trigger(ui_Fail, MMSoundManager.MMSoundManagerTracks.UI, this.transform.position);
    }
    #endregion

    #region 属性提升
    public void PropertyUpdate(ItemMainProperty property)
    {
        //TODO:改用在角色身上变化的方式。
        return;
        Text _Info = Instantiate(CenterInfo, this.transform);
        string _color = "";
        if (property.propertyValue > 0)
        {
            _color = "<color=green>增加</color>";
        }
        else
        {
            _color = "<color=red>减少</color>";
        }
        _Info.text = EnumPropertyName.GetPropertyName(property.mainProperty) + _color + property.propertyValue.ToString() ;
    }
    #endregion

    #region 命座激活
    public void ConstellationActiveMessage(ConstellationEffect _effect)
    {
        Text _Info = Instantiate(MediumSizeInfo, this.transform);
        _Info.text = "<color=blue>命座已点亮</color>\n" + _effect.effectDescription;
    }
    #endregion
}
