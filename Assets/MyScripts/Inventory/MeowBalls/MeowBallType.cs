using combatSystem;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// 武器模板 - 包含所有共用（非特殊效果）的数据获取。
/// 包含攻击执行的逻辑顺序。
/// </summary>
public class MeowBallType : ItemTypeDataDetails
{
    [Header("武器基础信息")]
    public MeowBallAttackType attackType;
    public float knockbackForce;

    #region Damage

    public int baseDamage;

    protected int bonusDamage;
    public List<MeowBallDamageBonus> damageBonusList;
    #endregion

    #region OtherProperty
    public float baseAttackInterval;
    public int baseRange;
    public float baseCritDamage;
    public float baseHPSteal;


    #endregion

    #region FinalProperty

    protected float attackInterval;
    [HideInInspector]public int finalDamage;
    protected float finalRange;
    #endregion


    #region Scriptable
    protected MeowStatus _meow;
    protected Vector2 _originalPos;
    public string _effectDescription = "";
    //protected Vector3 _targetPos;
    protected Vector3 _direction;
    protected float _flipX;
    protected float _attackTimer;
    protected bool _canAttack;
    public bool _isAttacking;
    #endregion

    protected virtual IEnumerator Start()
    {
        /*
        if (transform.localPosition.x > 0)
        {
            _flipX = transform.localScale.x;
        }
        else
        {
            _flipX = -transform.localScale.x;
        }
        transform.localScale = new Vector3(_flipX, 1, 1);*/
        yield return new WaitForSeconds(1f);
        _meow = GameCopilot.Instance.MeowStatus;
        Init();
    }
    private void OnEnable()
    {
        GenMeowEvent.WaveEnd += DestroyMe;
    }

    private void OnDisable()
    {
        GenMeowEvent.WaveEnd -= DestroyMe;
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    protected virtual void FixedUpdate()
    {
        //武器充能
        if(_attackTimer <= attackInterval && !_isAttacking)
        {
            _attackTimer += Time.fixedDeltaTime;
        }

        //找寻目标
        Vector3 _tar = FindTarget(finalRange);
        Vector3 _searchtar = FindTarget(finalRange * 1.2f);
        //如果不是正在攻击 => 可以改变方向
        if (!_isAttacking)
        {
            float _rot = 0;
            Vector3 _pos = EnemyManager.Instance.NearestEnemyPos(transform.position);
            if (_searchtar == Vector3.zero)
            {
                _pos = GameCopilot.Instance.MeowStatus.transform.position;
                _rot = Mathf.Atan2(-_pos.y + transform.position.y, -_pos.x + transform.position.x);
            }
            else
            {
                _rot = Mathf.Atan2(_pos.y - transform.position.y, _pos.x - transform.position.x);
            }
            float degreeValue = _rot * (180f / Mathf.PI);
            transform.rotation = Quaternion.Euler(0, 0, degreeValue);
        }
        
        if(_canAttack && _attackTimer >= attackInterval)
        {

            _direction =new Vector3 (_tar.x - transform.position.x , _tar.y - transform.position.y , _tar.z - transform.position.z).normalized;
            //transform.right = _direction * _flipX;
            if (_tar != Vector3.zero)
            {
                Attack(_tar);
            }
        }

    }

    //只在生成时计算一次，除非被要求调用。


    public virtual void EffectDescription()
    {
        //修改_effectDescription
    }

    public void Init()
    {
        //TODO:计算各种伤害，并赋值给相应的组件。
        CalculateDamageBonus(_meow);
        CalculateFinalDamage(_meow);


        //TODO:计算其他各种数据
        finalRange = baseRange + _meow.SO.Range;
        _originalPos = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);
        attackInterval = baseAttackInterval * 100 / (100 + _meow.SO.AttackSpeed);
        _canAttack = true;
    }
    #region 伤害计算
    public void CalculateDamageBonus(MeowStatus _meow)
    {
        if (damageBonusList != null)
        {
            foreach (var _bonus in damageBonusList)
            {
                switch (_bonus.damageBonus)
                {
                    case DamageBonus.MeowBaseDamage:
                        bonusDamage += _meow.SO.DamageBase * _bonus.propertyPercentValue;
                        break;
                    case DamageBonus.MeowHP:
                        bonusDamage += _meow.SO.MaxHealth * _bonus.propertyPercentValue;
                        break;
                    case DamageBonus.MeowLuck:
                        bonusDamage += _meow.SO.Luck * _bonus.propertyPercentValue;
                        break;
                    case DamageBonus.MeowRange:
                        bonusDamage += _meow.SO.Range * _bonus.propertyPercentValue;
                        break;
                    case DamageBonus.MeowElementMaster:
                        bonusDamage += _meow.SO.ElementMaster * _bonus.propertyPercentValue;
                        break;
                }
            }
        }
    }

    public void CalculateFinalDamage(MeowStatus _meow)
    {
        finalDamage = (int)((baseDamage + bonusDamage) * (_meow.SO.DamageGlobal +100) / 100.0);
    }

    #endregion

    #region Attack
    public virtual void Attack(Vector3 _targetPos)
    {
        if (_canAttack == false) return;
        _canAttack = false;
        _isAttacking = true;
    }

    public virtual void FinishAttack()
    {
        _attackTimer = 0;
        _canAttack = true;
        _isAttacking = false;
    }

    protected virtual Vector3 FindTarget(float _range)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, finalRange); // 检测范围内的碰撞体

        if (hitColliders.Length > 0)
        {
            foreach(Collider2D collider in hitColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    return collider.transform.position;
                }
            }
        }
        return Vector3.zero;
    }
    #endregion
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, baseRange);
    }

}
