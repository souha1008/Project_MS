using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : Animal
{
    CamelStatus status_;
    
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
        status_ = (CamelStatus)status;

        cost = status_.cost;
        maxHp = hp =status_.maxHP;
        attack = status_.attack;
        speed = status_.speed;
        attackSpeed = status_.attackSpeed;
        attackDist = status_.attackDist;
        dir = status_.dir;
        
        attackUpMag = status_.attackUpMag;
        hpHealOne = status_.hpHealOne;
        hpHealRange = status_.hpHealRange;
        speedUp = status_.speedUp;

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
