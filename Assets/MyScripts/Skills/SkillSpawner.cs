using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillSpawner : MMSingleton<SkillSpawner>
{
    public SkillDataBase_SO _skillDB;
    public Image _skillImg;
    public AudioClip _skillsfx;



    public RectTransform _pos1;
    public RectTransform _pos2;


    public MeowSkillBase MeowSkill { get { return _meowSkill; }set { _meowSkill = value; } }
    protected MeowSkillBase _meowSkill;
    public float CoolDown { get; set; }
    public float Timer { get { return timer; } set { timer = value; } }
    private float timer;



    private void OnEnable()
    {
        GenMeowEvent.UseSkill += UseSkill;
        _skillImg.enabled = false;
    }

    private void Start()
    {
        timer = 0f;
        SetMeowSkill(CharacterInfoManager.Instance.Character.characterSkill.skillID);
        Debug.Log("初始化技能" + CharacterInfoManager.Instance.Character.characterSkill.skillID);

        //链接属性
        InitMeowSkill();

        //初始化图片和语音
        GameSceneUIManager.Instance.skillIcon.sprite = CharacterInfoManager.Instance.Character.characterSkill.skillIcon;
        _skillsfx = CharacterInfoManager.Instance.Character.characterSkill.skillSound;
        _skillImg.sprite = CharacterInfoManager.Instance.Character.characterSkillUseBGSprite;

    }

    public void SetMeowSkill(int skillID)
    {
        foreach(var _skill in _skillDB.skillDatabase)
        {
            if(skillID == _skill.skillID)
            {
                if(MeowSkill != null) Destroy(MeowSkill);
                MeowSkill = Instantiate(_skill.meowSkill,transform);
                ForceSkillCoolDown();
                return;
            }
        }
    }

    public void InitMeowSkill()
    {
        CoolDown = MeowSkill.coolDown;
        MeowSkill.damagePart.HitAnythingFeedback = null;
        //TODO : 链接Meow中属性
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenMeowEvent.CallUseSkill();
        }
    }

    private void FixedUpdate()
    {
        if(MeowSkill != null)
        {
            timer += Time.fixedDeltaTime;
            GenMeowEvent.CallUpdateSkillFilling(Mathf.Clamp(timer / CoolDown, 0, 1));
        }
    }

    public void ForceSkillCoolDown()
    {
        timer = CoolDown;
    }

    public void UseSkill()
    {
        if (MeowSkill != null)
        {
            if (timer >= CoolDown)
            {
                MeowSkill.ActiveSkill();
                timer = 0f;
                GenMeowEvent.TriggerCombatEvent(CombatEventTriggerTimeEnum.UseSkill);
                SkillSFX();
            }
            else
            {
                Debug.Log("技能冷却中！剩余时间：" + (CoolDown - timer));
            }
        }
        else
        {
            Debug.Log("技能为空！");
        }
    }

    public void SkillSFX()
    {
        _skillImg.enabled = true;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_skillImg.rectTransform.DOMove(_pos1.position,0.5f).SetEase(Ease.OutQuad));
        sequence.AppendInterval(1f);
        sequence.Append(_skillImg.rectTransform.DOMove(_pos2.position, 0.5f).SetEase(Ease.InQuad).OnComplete(() =>
        _skillImg.enabled = false));
        sequence.Play();

        /// 将在SFX音轨上播放一个剪辑（这里我们的剪辑名为ExplosionSfx），在调用它的对象位置上播放。
        MMSoundManagerSoundPlayEvent.Trigger(_skillsfx, MMSoundManager.MMSoundManagerTracks.Sfx, this.transform.position);


    }


    private void OnDisable()
    {
        GenMeowEvent.UseSkill -= UseSkill;
    }
}
