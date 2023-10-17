using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
/// <summary>
/// 生物状况
/// </summary>
public class LivingsStatus : MonoBehaviour
{
    public  Livings_SO SO_TMP;
    protected Livings_SO SO;

    Health _health;
    CharacterMovement _movement;

    protected virtual void OnEnable()
    {
        SO = Instantiate(SO_TMP);
        SO.CurrentHealth = SO.MaxHealth;
        _health = GetComponent<Health>();
        _movement = GetComponent<CharacterMovement>();
        SetInitProp();
    }

    public void GetDamage(int damage)
    {
        SO.CurrentHealth -= damage;
        UpdateHealth();
        if (SO.CurrentHealth < 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        //TODO:播放Feedbacks
        GameCopilot.Instance.WaveNow = 1;
        Destroy(gameObject, 0.5f);
    }

    public void UpdateHealth()
    {
        _health.CurrentHealth = SO.CurrentHealth;
    }
    #region Init
    protected virtual void SetInitProp()
    {
        if (SO == null) return;
        //设定初始速度
        if(_movement != null)
            _movement.WalkSpeed = SO.Speed;
        _health.InitialHealth = SO.MaxHealth;
        _health.MaximumHealth = SO.MaxHealth;
    }

    #endregion
}
