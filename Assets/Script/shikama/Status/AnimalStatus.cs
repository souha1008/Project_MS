using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStatus
{
    [System.NonSerialized] public int cost = 20;
    [System.NonSerialized] public int maxHP = 100;
    [System.NonSerialized] public int hp = 100;
    [System.NonSerialized] public int attack = 30;
    [System.NonSerialized] public float speed = 1.5f;
    [System.NonSerialized] public float attackSpeed = 1.5f;
    [System.NonSerialized] public float attackDist = 1.0f;
    [System.NonSerialized] public int hitRate = 100;
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

    public void AllStatusUp(float statusUpMag)
    {
        attack = Mathf.RoundToInt(attack * statusUpMag);
        speed = Mathf.RoundToInt(speed * statusUpMag);
        attackSpeed = Mathf.RoundToInt(attackSpeed * statusUpMag);
        attackDist = Mathf.RoundToInt(attackDist * statusUpMag);
    }

   
    protected void AddHp(int _hp)
    {
        hp += _hp;
        if (hp > maxHP) hp = maxHP;
    }

    /// <summary>
    /// animal_は攻撃した側、自分は攻撃された側
    /// 攻撃してきた相手を取得する必要がある場合があるので引数にthisを指定する。
    /// </summary>
    /// <param name="animal_">thisを指定</param>
    virtual public void AddHp(int _hp, Animal animal_)
    {
        ElephantAttack(animal_);
        BuffaloAttack(animal_);
        CamelAttack(animal_);

        AddHp(_hp);
    }

    private void ElephantAttack(Animal animal_)
    {
        if (animal_ is Elephant == false) return;

        Elephant elephant = (Elephant)animal_;
        if (elephant.evolution.Equals(EVOLUTION.ICEAGE))
        {
            speed = 0;
            animal.CancelInvoke("ResetSpeed");
            animal.Invoke("ResetSpeed", ((ElephantStatus)elephant.status).iceAgeStopTime);
        }
    }

    private void BuffaloAttack(Animal animal_)
    {
        if (animal_ is Buffalo == false) return;

        Buffalo buffalo = (Buffalo)animal_;

        if (buffalo.evolution == EVOLUTION.HURRICANE)
        {
            speed = 0;
            animal.Invoke("ResetSpeed", 4.0f);
        }
        else if (buffalo.evolution == EVOLUTION.ICEAGE)
        {
            Debug.Log(buffalo.iceCount.ToString() + " " + buffalo.status.hp.ToString());
            if (buffalo.iceCount == ((BuffaloStatus)buffalo.status).IceAgeCount)
            {
                buffalo.iceCount = 0;
                buffalo.status.hp += (int)(buffalo.status.maxHP * ((BuffaloStatus)buffalo.status).IceAgeHealMag * 0.01f);
                if (buffalo.status.hp > buffalo.status.maxHP) buffalo.status.hp = buffalo.status.maxHP;

                return;
            }
            buffalo.iceCount++;
        }
        else if(buffalo.evolution == EVOLUTION.BIGFIRE)
        {
            List<Animal> enemy = Animal.animalList.FindAll(enemy => enemy.tag == "Enemy");

            if (enemy.Count == 0) return;

            for(int i = 0; i < 2; i++)
            {
                int r = Random.Range(0, enemy.Count - 1);
                enemy[r].status.hp -= (int)(buffalo.status.attack * 0.5f);
            }
        }
    }

    private void CamelAttack(Animal animal_)
    {
        if (animal_ is Camel == false) return;

        Camel camel = (Camel)animal_;
        if (animal.evolution == EVOLUTION.ICEAGE)
        {
            foreach (Animal _animal in Animal.animalList)
            {
                if (_animal.tag == "Enemy") continue;

                float dist = Vector2.Distance(camel.transform.position, _animal.transform.position);
                if (dist <= 3.0f)
                {
                    _animal.status.hp = Mathf.Clamp(Mathf.RoundToInt(_animal.status.hp + _animal.status.maxHP * 0.07f),
                        0, _animal.status.maxHP);
                }
            }
        }
    }
}
