using combatSystem;
using MoreMountains.Feedbacks;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : MMSingleton<GameSceneUIManager>
{
    [Header("生命值组件")]
    public Health health;
    public Text HP_txt;
    [Header("经验值组件")]
    public Text level_txt;
    public Image exp_Image;
    [Header("摩拉组件")]
    public Text molaNum_txt;
    [Header("状态组件")]
    public GameObject statusParent;
    [Header("技能组件")]
    public Image skillIcon;
    public Button skillButton;
    [Header("填充未完成")]
    public Image unfilledOutline;
    public Image unfilledInside;//Color
    [Header("填充已完成")]
    public Image filledOutline;//Color
    public Image filledInside;//Color

    public Color _ElementColor;

    [Header("波数与时间")]
    public Text timeLeft_txt;
    public Text wave_txt;
    public MMF_Player VictoryFeedbacks;

    protected MMF_Player _molaMMF;
    protected MMF_Player _timerMMF;

    private IEnumerator Start()
    {
        //技能
        skillButton.onClick.AddListener(ClickUseSkill);
        //波数
        GenMeowEvent.UpdateTime += OnWaveTimeUpdate;
        GenMeowEvent.UpdateSkillFilling += SetSkillFill;
        //数据
        GenMeowEvent.UpdateMola += RefreshMola;
        //控制
        GenMeowEvent.WaveEnd += OnWaveEnd;

        wave_txt.text = "第" + GameCopilot.Instance.WaveNow.ToString() + "波";

        timeLeft_txt.enabled = true;
        wave_txt.enabled = true;

        _molaMMF = molaNum_txt.GetComponent<MMF_Player>();
        _timerMMF = timeLeft_txt.GetComponent <MMF_Player>();


        yield return new WaitForSeconds(0.5f);
        //Color
        InitSkillColor();
        //HP与摩拉信息
        InitHPNumber();
        InitMolaNum();

        RefreshMola();
        exp_Image.enabled = true;
        level_txt.enabled=true;
    }

    public void InitHPNumber()
    {
        HP_txt.enabled = true;
        if (health == null)
        {
            Debug.Log("没有生命值组件！");
            return;
        }
        string _currentHP = health.CurrentHealth.ToString();
        string _maxHP = health.MaximumHealth.ToString();
        HP_txt.text = _currentHP + "/" + _maxHP;
    }

    public void InitMolaNum()
    {
        molaNum_txt.enabled = true;
        molaNum_txt.text = GenMeowInventoryManager.Instance.CurrentMola.ToString();
    }

    public void SetMolaNum(int _molaNum)
    {
        molaNum_txt.text = _molaNum.ToString();
    }

    public void OnWaveTimeUpdate(int _currentTime)
    {
        timeLeft_txt.text = _currentTime.ToString();
        _timerMMF?.PlayFeedbacks();
    }

    public void SetSkillFill(float _percentage)
    {
        //TODO:优化？
        if(_percentage < 1)
        {
            unfilledOutline.enabled = true;
            unfilledInside.enabled = true;
            filledInside.enabled = false;
            filledOutline.enabled = false;
            unfilledInside.fillAmount = _percentage;
            skillIcon.color = new Color(skillIcon.color.r, skillIcon.color.g, skillIcon.color.b, 0.25f);
        }
        else
        {
            unfilledOutline.enabled = false;
            unfilledInside.enabled = false;
            filledInside.enabled = true;
            filledOutline.enabled = true;
            skillIcon.color = new Color(skillIcon.color.r, skillIcon.color.g, skillIcon.color.b, 1);
        }
    }

    public void ClickUseSkill()
    {
        GenMeowEvent.CallUseSkill();
    }


    private void OnDisable()
    {
        GenMeowEvent.UpdateTime -= OnWaveTimeUpdate;
        GenMeowEvent.UpdateSkillFilling -= SetSkillFill;
        GenMeowEvent.UpdateMola -= RefreshMola;
        GenMeowEvent.WaveEnd -= OnWaveEnd;
    }

    public void RefreshMola()
    {
        level_txt.text = "Lv. " + GenMeowInventoryManager.Instance._inventory.currentLevel.ToString();
        exp_Image.fillAmount = (float)GenMeowInventoryManager.Instance._inventory.currentExp / GenMeowInventoryManager.Instance.level_SO.levels[GenMeowInventoryManager.Instance._inventory.currentLevel].exp;
        molaNum_txt.text = GenMeowInventoryManager.Instance.CurrentMola.ToString();
        _molaMMF?.PlayFeedbacks();
    }

    public void OnWaveEnd()
    {
        VictoryFeedbacks?.PlayFeedbacks();
    }

    public void InitSkillColor()
    {
        _ElementColor = CharacterInfoManager.Instance.Character.elementColor;

        Color tempColor = unfilledInside.color;
        tempColor.r = _ElementColor.r;
        tempColor.g = _ElementColor.g;
        tempColor.b = _ElementColor.b;
        unfilledInside.color = tempColor;

        tempColor = filledOutline.color;
        tempColor.r = _ElementColor.r;
        tempColor.g = _ElementColor.g;
        tempColor.b = _ElementColor.b;
        filledOutline.color = tempColor;

        tempColor = filledInside.color;
        tempColor.r = _ElementColor.r;
        tempColor.g = _ElementColor.g;
        tempColor.b = _ElementColor.b;
        filledInside.color = tempColor;
    }


}
