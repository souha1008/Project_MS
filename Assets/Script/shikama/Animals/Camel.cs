using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : Animal
{
    [SerializeField] CamelStatus status;
    
    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float hpHealOneDist = 10.0f;
    float hpHealRangeDist = 3.0f;

    float attackUpMag = 3.0f;
    float hpHealOne = 1.1f;
    float hpHealRange= 1.1f;
    float speedUp = 1.05f;

    override protected void Start()
    {
        cost = status.cost;
        maxHp = hp =status.maxHP;
        attack = status.attack;
        speed = status.speed;
        attackSpeed = status.attackSpeed;
        attackDist = status.attackDist;
        dir = status.dir;
        
        attackUpMag = status.attackUpMag;
        hpHealOne = status.hpHealOne;
        hpHealRange = status.hpHealRange;
        speedUp = status.speedUp;

        base.Start();
    }

    override public void MeteoEvolution()
    {
        if (!earthquakeEvolution && !meteoEvolution)
        {
            int mode = Random.Range(0, 3);
            switch (mode)
            {
                case 0: // 攻撃力アップ
                    AttackUp();
                    break;
                case 1: // 範囲回復
                    HealRange();
                    break;
                case 2: // スピードアップ
                    SpeedUp();
                    break;
            }
        }
    }

    override public void EarthquakeEvolution()
    {
        if (!earthquakeEvolution && !meteoEvolution)
        {
            HealOne();
        }
    }

    private void AttackUp()
    {
        attack = (int)(attack * attackUpMag);
        meteoEvolution = true;
    }

    private void HealRange()
    {
        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;
            float dist = Vector2.Distance(transform.position, animal.transform.position);
            if (dist <= hpHealRangeDist)
            {
                Debug.Log(animal.hp);
                animal.hp += (int)(animal.maxHp * hpHealRange);
                if (animal.maxHp < animal.hp) animal.hp = animal.maxHp;
                Debug.Log(animal.hp);
            }
        }
        meteoEvolution = true;
    }

    private void HealOne()
    {
        Animal healAnimal = null;
        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;
            float dist = Vector2.Distance(transform.position, animal.transform.position);
            if (dist <= hpHealOneDist)
            {
                if (!healAnimal) healAnimal = animal;
                else
                {
                    float dist_ = Vector2.Distance(transform.position, healAnimal.transform.position);
                    if (dist > dist_) healAnimal = animal;
                }
            }
        }

        if (healAnimal)
        {
            Debug.Log(healAnimal.hp);
            healAnimal.hp += (int)(healAnimal.maxHp * hpHealOne);
            if (healAnimal.maxHp < healAnimal.hp) healAnimal.hp = healAnimal.maxHp;
            Debug.Log(healAnimal.hp);
        }
        earthquakeEvolution = true;
    }

    private void SpeedUp()
    {
        speed *= speedUp;
        meteoEvolution = true;
    }
}
