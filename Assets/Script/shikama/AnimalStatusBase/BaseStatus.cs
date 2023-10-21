using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : ScriptableObject
{
    public int cost = 20;

    public int maxHP = 100;
    [SerializeField] int attack = 30;
    [SerializeField] float speed = 1.5f;
    [SerializeField] float attackSpeed = 1.5f;
    [SerializeField] float attackDist = 1.0f;

    public DIRECTION dir;

    [System.NonSerialized] public int hp_ = 100;
    [System.NonSerialized] public int attack_ = 30;
    [System.NonSerialized] public float speed_ = 1.5f;
    [System.NonSerialized] public float attackSpeed_ = 1.5f;
    [System.NonSerialized] public float attackDist_ = 1.0f;

    protected Animal animal;
    
    virtual public void Init(Animal animal_)
    {
        hp_ = maxHP;
        attack_ = attack;
        speed_ = speed;
        attackSpeed_ = attackSpeed;
        attackDist_ = attackDist;
        animal = animal_;
    }

    /// <summary>
    /// スピードを初期値に戻す
    /// </summary>
    public void ResetSpeed()
    {
        speed_ = speed;
    }

    /// <summary>
    /// 攻撃を初期値に戻す
    /// </summary>
    public void ResetAttack()
    {
        attack_ = attack;
    }

    /// <summary>
    /// 攻撃スピードを初期値に戻す
    /// </summary>
    public void ResetAttackSpeed()
    {
        attackSpeed_ = attackSpeed;
    }

    /// <summary>
    /// 攻撃範囲を初期値に戻す
    /// </summary>
    public void ResetAttackDist()
    {
        attackDist_ = attackDist;
    }

    /// <summary>
    /// HPを除く全ステータスを初期値に戻す
    /// </summary>
    public void ResetAll()
    {
        attack_ = attack;
        speed_ = speed;
        attackSpeed_ = attackSpeed;
        attackDist_ = attackDist;
    }

    virtual public void AddHp(int _hp)
    {
        hp_ += _hp;
    }
}
