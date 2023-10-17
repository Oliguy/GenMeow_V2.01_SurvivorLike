using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTemplate : MonoBehaviour
{
    [Header("需填入参数")]
    public int characterID;//用于和数据库相连
    public string characterName;
    public Sprite characterIconSprite;
    public Sprite characterSkillUseBGSprite;
    public MeowSkillBase characterSkill;
    public ConstellationSet constellationSet;
    [Header("需更新目标")]
    public Text characterNameText;
    public Image characterIconImage;
    [Header("游戏内")]
    public Sprite characterCombatSprite;
    public Color elementColor;
    public int initMeowballID;


    protected Button _button;
    /// <summary>
    /// 初始化
    /// </summary>
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        _button = GetComponentInParent<Button>();
        if (_button == null) return;
        characterNameText.text = characterName;
        characterIconImage.sprite = characterIconSprite;
        _button.onClick.AddListener(Select);
        _button.onClick.AddListener(DoClickInterval);
    }


    /// <summary>
    /// 点击时向单例发送选中信息
    /// </summary>
    public void Select()
    {
        CharacterSelectManager.Instance.SelectCharacter(this);
    }

    public void DoClickInterval()
    {
        StartCoroutine(ClickInterval());
        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.StopTrack, MMSoundManager.MMSoundManagerTracks.UI);
    }

    IEnumerator ClickInterval()
    {
        Debug.Log(_button.targetGraphic.name);
        _button.interactable = false;
        yield return new WaitForSeconds(0.5f);
        _button.interactable = true;
    }

}
