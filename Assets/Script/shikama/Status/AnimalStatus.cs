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

    /// <summary>
    /// コストを初期値に戻す
    /// </summary>
    private void ResetCost()
    {
        baseStatus.cost = cost;
    }

    /// <summary>
    /// スピードを初期値に戻す
    /// </summary>
    public void ResetSpeed()
    {
        speed = baseStatus.speed;
    }

    /// <summary>
    /// 攻撃を初期値に戻す
    /// </summary>
    public void ResetAttack()
    {
        attack = baseStatus.attack;
    }

    /// <summary>
    /// 攻撃スピードを初期値に戻す
    /// </summary>
    public void ResetAttackSpeed()
    {
        attackSpeed = baseStatus.attackSpeed;
    }

    /// <summary>
    /// 攻撃範囲を初期値に戻す
    /// </summary>
    public void ResetAttackDist()
    {
        attackDist = baseStatus.attackDist;
    }

    public void ResetHitRate()
    {
        hitRate = baseStatus.hitRate;
    }

    /// <summary>
    /// HPを除く全ステータスを初期値に戻す
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
        ZebraAttack(ref _hp, animal_);

        AddHp(_hp);

        BuffaloPlaDeath(animal_);
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

    private void BuffaloPlaDeath(Animal animal_)
    {
        if (animal_ is Buffalo == false) return;

        Buffalo buffalo = (Buffalo)animal_;
        if (buffalo.evolution == EVOLUTION.PLAGUE)
        {
            Destroy(buffalo.gameObject);
        }
    }

    private void CamelAttack(Animal animal_)
    {
        if (animal_ is Camel == false) return;

        Camel camel = (Camel)animal_;
        if (camel.evolution.Equals(EVOLUTION.HURRICANE))
            camel.barrierCount++;
        else if (animal.evolution == EVOLUTION.ICEAGE)
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

    private void ZebraAttack(ref int hp_, Animal animal_)
    {
        if (animal_ is Zebra == false) return;

        Zebra zebra = (Zebra)animal_;
        if (zebra.evolution.Equals(EVOLUTION.HURRICANE))
        {
            hp_ = (int)(hp_ * 0.5f);
            if (Random.Range(1,100) <= 20)
            {
                hitRate -= 40;
                Invoke("ResetHitRate", 4.0f);
            }

            zebra.evolution = EVOLUTION.NONE;
        }
        else if (zebra.evolution.Equals(EVOLUTION.THUNDERSTORM))
        {
            if (!animal.zebraSE)
            {
                hp_ = (int)(hp_ * 1.02f);
                animal.zebraSE = true;
            }
        }
        else if(zebra.evolution.Equals(EVOLUTION.ERUPTION))
        {
            hp_ = (int)(hp_ * 1.5f);
            animal_.status.speed = 0;
            animal_.status.Invoke("ResetSpeed", 6.0f);
            zebra.evolution = EVOLUTION.NONE;
        }

    }
}
