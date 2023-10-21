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

    override protected void Start()
    {
        status_ = (CamelStatus)status;

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
        status_.attack_ = (int)(status_.attack_ * status_.attackUpMag);
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
                animal.status.hp_ += (int)(animal.status.maxHP * status_.hpHealRange);
                if (animal.status.maxHP < animal.status.hp_) animal.status.hp_ = animal.status.maxHP;
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
            healAnimal.status.hp_ += (int)(healAnimal.status.maxHP * status_.hpHealOne);
            if (healAnimal.status.maxHP < healAnimal.status.hp_) healAnimal.status.hp_ = healAnimal.status.maxHP;
        }
        earthquakeEvolution = true;
    }

    private void SpeedUp()
    {
        status_.speed_ *= status_.speedUp;
        meteoEvolution = true;
    }
}
