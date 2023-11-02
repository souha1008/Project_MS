using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStatus : MonoBehaviour
{
    [System.NonSerialized] public int cost = 20;
    [System.NonSerialized] public int maxHP = 100;
    [System.NonSerialized] public int hp = 100;
    [System.NonSerialized] public int attack = 30;
    [System.NonSerialized] public float speed = 1.5f;
    [System.NonSerialized] public float attackSpeed = 1.5f;
    [System.NonSerialized] public float attackDist = 1.0f;
    [System.NonSerialized] public DIRECTION dir;
    protected Animal animal;
 
    BaseStatus baseStatus;

    public AnimalStatus(BaseStatus baseStatus, Animal animal)
    {
        this.baseStatus = baseStatus;
        this.animal = animal;

        cost = baseStatus.cost;
        hp = maxHP = baseStatus.maxHP;
        attack = baseStatus.attack;
        speed = baseStatus.speed;
        attackSpeed = baseStatus.attackSpeed;
        attackDist = baseStatus.attackDist;
        dir = baseStatus.dir;
    }

    /// <summary>
    /// �R�X�g�������l�ɖ߂�
    /// </summary>
    private void ResetCost()
    {
        baseStatus.cost = cost;
    }

    /// <summary>
    /// �X�s�[�h�������l�ɖ߂�
    /// </summary>
    public void ResetSpeed()
    {
        speed = baseStatus.speed;
    }

    /// <summary>
    /// �U���������l�ɖ߂�
    /// </summary>
    public void ResetAttack()
    {
        attack = baseStatus.attack;
    }

    /// <summary>
    /// �U���X�s�[�h�������l�ɖ߂�
    /// </summary>
    public void ResetAttackSpeed()
    {
        attackSpeed = baseStatus.attackSpeed;
    }

    /// <summary>
    /// �U���͈͂������l�ɖ߂�
    /// </summary>
    public void ResetAttackDist()
    {
        attackDist = baseStatus.attackDist;
    }

    /// <summary>
    /// HP�������S�X�e�[�^�X�������l�ɖ߂�
    /// </summary>
    public void ResetAll()
    {
        attack = baseStatus.attack;
        speed = baseStatus.speed;
        attackSpeed = baseStatus.attackSpeed;
        attackDist = baseStatus.attackDist;
    }

    protected void AddHp(int _hp)
    {
        hp += _hp;
    }

    /// <summary>
    /// animal_�͍U���������A�����͍U�����ꂽ��
    /// �U�����Ă���������擾����K�v������ꍇ������̂ň�����this���w�肷��B
    /// </summary>
    /// <param name="animal_">this���w��</param>
    virtual public void AddHp(int _hp, Animal animal_)
    {
        ElephantAttack(animal_);
        CamelAttack(animal_);
        AddHp(_hp);
    }

    private void ElephantAttack(Animal animal_)
    {
        if (animal is Elephant == false) return;

        Elephant elephant = (Elephant)animal;
        if (elephant.evolution.Equals(EVOLUTION.ICEAGE))
        {
            animal_.baseStatus.speed = 0;
            animal_.Invoke("ResetSpeed", 2.0f);
        }

    }

    private void CamelAttack(Animal animal_)
    {
        if (animal_ is Camel == false) return;

        Camel camel = (Camel)animal_;
        if (camel.evolution.Equals(EVOLUTION.HURRICANE))
            camel.barrierCount++;
    }
}
