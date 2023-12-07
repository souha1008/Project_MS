using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : Animal
{
    CamelStatus status_;

    float activeTimer = 0.0f;
    float coolTimer = 0.0f;

    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float hpHealOneDist = 10.0f;

    public int barrierCount { get; set; } = 0;
    float barrierTimer = 0;

    static private bool costDown  = false;

    private bool particleStop = false;
    
    override protected void Start()
    {
        base.Start();

        status = new CamelStatus(baseStatus as CamelBaseStatus, this);
        status_ = status as CamelStatus;
    }

    protected override void Update()
    {
        base.Update();

        Debug.Log(status.speed);

        if (evolution.Equals(EVOLUTION.HURRICANE) && barrierCount >= 5)
        {
            if(barrierTimer <= status_.barrierTime)
            {
                barrierTimer += Time.deltaTime;
            }
            else
            {
                barrierTimer = 0;
                barrierCount = 0;
            }
        }

        if (activeTimer != 0.0f)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0.0f)
            {
                evolution = EVOLUTION.NONE;
                activeTimer = 0.0f;
            }
        }

        if (coolTimer > 0)
        {
            coolTimer -= Time.deltaTime;
        }
        else
        {
            coolTimer = 0.0f;
        }

        if (particleStop)
        {
            particle.Stop();
            if (particle.particleCount == 0)
            {
                particle.gameObject.SetActive(false);
                particle.Play();
                evolution = EVOLUTION.NONE;
                particleStop = false;
            }
        }
    }

    override public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;
        base.MeteoEvolution();
     
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

        particle.gameObject.SetActive(true);

        Invoke("ParticleStop",1.0f);
        coolTimer = status_.coolTimeMeteo;
    }

    private void ParticleStop()
    {
        particleStop = true;
    }

    override public void EarthquakeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;

        if (!earthquakeEvolution && !meteoEvolution)
        {
            HealOne();
        }
    }

    public override void ThunderstormEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;

        base.ThunderstormEvolution();

        foreach(Animal animal in animalList)
        {
            if(animal.tag == "Enemy")
            {
                animal.status.speed *= 1.0f + status_.thunderSpeedUP * 0.01f;
                break;
            }
        }

        activeTimer = status_.activeTimeThunder;
        coolTimer = status_.coolTimeThunder;
    }

    public override void TsunamiEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;

        base.TsunamiEvolution();

        if (!costDown) baseStatus.cost = (int)(baseStatus.cost * (1.0f - status_.costDownMag));
        costDown = true;
    }

    public override void EruptionEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;

        base.EruptionEvolution();

        List<Animal> playerAnimal = new List<Animal>();
        
        foreach(Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;
            playerAnimal.Add(animal);
        }

        int r = Random.Range(0, playerAnimal.Count - 1);

        playerAnimal[r].status.speed *= 1.2f;
    }

    public override void PlagueEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;

        base.PlagueEvolution();
        status.attackSpeed = 0;
    }

    public override void DesertificationEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;
        base.DesertificationEvolution();

        status.hp -= (int)(status.maxHP * 0.1f);
        if (status.hp <= 0) status.hp = 1;

        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;
            
            animal.status.AllStatusUp(1.03f);
            animal.status.Invoke("ResetAll", 4.0f);
        }
    }

    public override void IceAgeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;
        base.IceAgeEvolution();
    }





    private void AttackUp()
    {
        status_.attack = (int)(status_.attack * (1.0f + status_.meteoAttackUp * 0.01f));
    }

    private void HealRange()
    {
        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;

            float dist = Vector2.Distance(transform.position, animal.transform.position);
            if (dist <= status_.meteoHealDist)
            {
                animal.status.hp += Mathf.RoundToInt(animal.status.maxHP * status_.meteoHPHeal * 0.01f);
                if (animal.status.maxHP < animal.status.hp) animal.status.hp = animal.status.maxHP;
            }
        }
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
            healAnimal.status.hp += (int)(healAnimal.status.maxHP * status_.hpHealOne);
            if (healAnimal.status.maxHP < healAnimal.status.hp) healAnimal.status.hp = healAnimal.status.maxHP;
        }
        earthquakeEvolution = true;
    }

    private void SpeedUp()
    {
        status_.speed *= 1.0f + status_.meteoSpeedUp * 0.01f;
    }
}
