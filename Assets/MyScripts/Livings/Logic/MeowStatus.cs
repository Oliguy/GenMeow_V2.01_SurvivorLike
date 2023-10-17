using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowStatus : MonoBehaviour
{

    public Meow_SO SO_TMP;
    public Meow_SO SO
    { //每次进入主战斗场景的时候，都去CharacterInfoManager处拿SO 。实际上应该放到静态脚本去做。
        get {
            if (_so != null) return _so;
            if (CharacterInfoManager.Instance.meowInfo_SO.meowSO != null)
            {
                _so = CharacterInfoManager.Instance.meowInfo_SO.meowSO;
                return _so;
            }
            else
            {
                _so = Instantiate(SO_TMP);
                return _so;
            }
        } 
    }

    protected Meow_SO _so;

    Health _healthComponent;
    DamageResistance _damageResistanceComponent;
    CharacterMovement characterMovement;


    private void Awake()
    {
        _healthComponent = GetComponent<Health>();
        _damageResistanceComponent = GetComponent<DamageResistance>();
        characterMovement = GetComponent<CharacterMovement>();

        GenMeowEvent.WaveStart += OnWaveStart;
        GenMeowEvent.AfterWaveStart += OnAfterWaveStart;
        GenMeowEvent.WaveEnd += OnWaveEnd;
        GenMeowEvent.WaveEnd += OnGameStart;
        GenMeowEvent.RefreshStatus += SetInitProp;
    }

    protected virtual void OnGameStart()
    {
        Debug.Log("GameStartInStatus");
        GenMeowInventoryManager.Instance?.BuffItemLoad(this);
    }


    protected virtual void OnWaveStart()
    {
        Debug.Log("MeowStatus - OnWaveStart");
        GameCopilot.Instance.MeowRegister(this);
        GenMeowEvent.CallAfterWaveStart();
    }


    //应当在BuffItem加载之后，MeowBall加载之前
    protected virtual void OnAfterWaveStart()
    {
        SetInitProp(true);
        SO.CurrentHealth = SO.MaxHealth;
    }

    private void OnWaveEnd()
    {
        characterMovement.InputAuthorized = false;

        GenMeowEvent.WaveStart -= OnWaveStart;
        GenMeowEvent.AfterWaveStart -= OnAfterWaveStart;
        GenMeowEvent.WaveEnd -= OnWaveEnd;
        GenMeowEvent.WaveEnd -= OnGameStart;
        GenMeowEvent.RefreshStatus -= SetInitProp;

        GameCopilot.Instance.MeowQuit();
    }

    #region Init
    //直接组件赋值
    protected virtual void SetInitProp(bool _gameStart)
    {
        if (SO == null) return;
        //设定初始速度
        characterMovement.MovementSpeed = SO.Speed;
        //设定初始生命值
        _healthComponent.InitialHealth = SO.MaxHealth;
        if (_gameStart)
        {
            _healthComponent.CurrentHealth = SO.MaxHealth;
        }
        _healthComponent.MaximumHealth = SO.MaxHealth;
        _healthComponent.UpdateHealthBar(true);
        GameSceneUIManager.Instance?.InitHPNumber();
        //设定防御力
        //_damageResistanceComponent.DamageMultiplier = (float)(1.0-((double) SO.Defence) / (15.0 + SO.Defence));

    }
    //战斗相关 - 武器自取
    #endregion



    public void SetCurrentHealth(int _currentHealth)
    {
        _healthComponent.CurrentHealth = _currentHealth;
    }
}
